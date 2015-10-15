using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.ApplicationModel.Core;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MbientLab.MetaWear.Test {
    public enum ConsoleEntryType {
        SEVERE,
        INFO,
        COMMAND,
        SENSOR
    }
    public class ConsoleLineColorConverter : IValueConverter {
        public SolidColorBrush SevereColor { get; set; }
        public SolidColorBrush InfoColor { get; set; }
        public SolidColorBrush CommandColor { get; set; }
        public SolidColorBrush SensorColor { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language) {
            switch((ConsoleEntryType) value) {
                case ConsoleEntryType.SEVERE:
                    return SevereColor;
                case ConsoleEntryType.INFO:
                    return InfoColor;
                case ConsoleEntryType.COMMAND:
                    return CommandColor;
                case ConsoleEntryType.SENSOR:
                    return SensorColor;
                default:
                    throw new MissingMemberException("Unrecognized log level: " + value.ToString());
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
    public class ConsoleLine {
        public ConsoleLine(ConsoleEntryType type) {
            this.Type = type;
        }

        public ConsoleLine(ConsoleEntryType type, string value) {
            this.Type = type;
            this.Value = value;
        }

        public ConsoleEntryType Type { get; }
        public string Value { get; set; }
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DeviceInfo : Page {
        private static readonly Guid DEVICE_INFO_SERVICE = new Guid("0000180a-0000-1000-8000-00805f9b34fb"),
            CHARACTERISTIC_MANUFACTURER= new Guid("00002a29-0000-1000-8000-00805f9b34fb"),
            CHARACTERISTIC_MODEL_NUMBER= new Guid("00002a24-0000-1000-8000-00805f9b34fb"),
            CHARACTERISTIC_SERIAL_NUMBER= new Guid("00002a25-0000-1000-8000-00805f9b34fb"),
            CHARACTERISTIC_FIRMWARE_REVISION = new Guid("00002a26-0000-1000-8000-00805f9b34fb"),
            CHARACTERISTIC_HARDWARE_REVISION = new Guid("00002a27-0000-1000-8000-00805f9b34fb");
        private static readonly Dictionary<Guid, String> DEVICE_INFO_NAMES = new Dictionary<Guid, String>();

        static DeviceInfo() {
            DEVICE_INFO_NAMES.Add(CHARACTERISTIC_MANUFACTURER, "Manufacturer");
            DEVICE_INFO_NAMES.Add(CHARACTERISTIC_MODEL_NUMBER, "Model Number");
            DEVICE_INFO_NAMES.Add(CHARACTERISTIC_SERIAL_NUMBER, "Serial Number");
            DEVICE_INFO_NAMES.Add(CHARACTERISTIC_FIRMWARE_REVISION, "Firmware Revision");
            DEVICE_INFO_NAMES.Add(CHARACTERISTIC_HARDWARE_REVISION, "Hardware Revision");
        }

        private enum Signal {
            SWITCH
        }

        private Dictionary<Signal, IntPtr> signals = new Dictionary<Signal, IntPtr>();
        private BluetoothLEDevice selectedBtleDevice;
        private GattDeviceService mwGattService;
        private GattCharacteristic mwNotifyChar;
        private IntPtr mwBoard;

        private SendCommand sendCmdDelegate;
        private ReceivedSensorData receivedSensorDataDelegate;
        private Connection conn;

        public DeviceInfo() {
            this.InitializeComponent();

            mwBoard = MetaWearBoard.Create();

            sendCmdDelegate = new SendCommand(sendMetaWearCommand);
            receivedSensorDataDelegate = new ReceivedSensorData(receivedSensorData);

            conn = new Connection();
            conn.sendCommandDelegate = Marshal.GetFunctionPointerForDelegate<SendCommand>(sendCmdDelegate);
            conn.receivedSensorDataDelegate = Marshal.GetFunctionPointerForDelegate<ReceivedSensorData>(receivedSensorDataDelegate);
            Connection.Init(ref conn);
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e) {
            selectedBtleDevice = (BluetoothLEDevice) e.Parameter;
            mwGattService = selectedBtleDevice.GetGattService(Gatt.METAWEAR_SERVICE);

            foreach(var characteristic in selectedBtleDevice.GetGattService(DEVICE_INFO_SERVICE).GetAllCharacteristics()) {
                var result = await characteristic.ReadValueAsync();
                string value = result.Status == GattCommunicationStatus.Success ?
                    System.Text.Encoding.UTF8.GetString(result.Value.ToArray(), 0, (int)result.Value.Length) :
                    "N/A";
                outputListView.Items.Add(new ConsoleLine(ConsoleEntryType.INFO, DEVICE_INFO_NAMES[characteristic.Uuid] + ": " + value));
            }

            mwNotifyChar = mwGattService.GetCharacteristics(Gatt.METAWEAR_NOTIFY_CHARACTERISTIC).First();
            await mwNotifyChar.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.Notify);
            mwNotifyChar.ValueChanged += new TypedEventHandler<GattCharacteristic, GattValueChangedEventArgs>((GattCharacteristic sender, GattValueChangedEventArgs obj) => {
                byte[] response = obj.CharacteristicValue.ToArray();
                MetaWearBoard.HandleResponse(mwBoard, response, (byte) response.Length);
            });
        }

        private string byteArrayToHex(byte[] array) {
            var builder = new StringBuilder();

            builder.Append(string.Format("[0x{0:X2}", array[0]));
            for (int i = 1; i < array.Length; i++) {
                builder.Append(string.Format(", 0x{0:X2}", array[i]));
            }
            builder.Append("]");
            return builder.ToString();
        }

        private async void sendMetaWearCommand(IntPtr board, IntPtr command, byte len) {
            byte[] managedArray = new byte[len];
            Marshal.Copy(command, managedArray, 0, len);
            outputListView.Items.Add(new ConsoleLine(ConsoleEntryType.COMMAND, "Command: " + byteArrayToHex(managedArray)));

            try {
                GattCharacteristic mwCommandChar = mwGattService.GetCharacteristics(Gatt.METAWEAR_COMMAND_CHARACTERISTIC).FirstOrDefault();
                GattCommunicationStatus status = await mwCommandChar.WriteValueAsync(managedArray.AsBuffer(), GattWriteOption.WriteWithoutResponse);

                if (status != GattCommunicationStatus.Success) {
                    outputListView.Items.Add(new ConsoleLine(ConsoleEntryType.SEVERE, "Error writing command, GattCommunicationStatus= " + status));
                }
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void receivedSensorData(IntPtr signal, ref Data data) {
            object managedValue = null;

            switch(data.typeId) {
                case DataTypeId.UINT32:
                    managedValue = Marshal.PtrToStructure<uint>(data.value);
                    break;
                case DataTypeId.FLOAT:
                    managedValue= Marshal.PtrToStructure<float>(data.value);
                    break;
                case DataTypeId.CARTESIAN_FLOAT:
                    managedValue = Marshal.PtrToStructure<CartesianFloat>(data.value);
                    break;
            }

            ConsoleLine newLine = new ConsoleLine(ConsoleEntryType.SENSOR);

            if (managedValue != null) {
                if (signals[Signal.SWITCH] == signal) {
                    newLine.Value = "Switch ";
                    newLine.Value += ((uint)managedValue) == 0 ? "Released" : "Pressed";
                }

                if (CoreApplication.MainView.CoreWindow.Dispatcher.HasThreadAccess) {
                    outputListView.Items.Add(newLine);
                } else {
                    CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                        CoreDispatcherPriority.Normal,
                        () => outputListView.Items.Add(newLine)
                    );
                }
            }
        }

        private void startMotor(object sender, RoutedEventArgs e) {
            Haptic.StartMotor(mwBoard, (float) 100, 5000);
        }

        private void startBuzzer(object sender, RoutedEventArgs e) {
            Haptic.StartBuzzer(mwBoard, 5000);
        }

        private void toggleSwitchSampling(object sender, RoutedEventArgs e) {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;

            if (!signals.ContainsKey(Signal.SWITCH)) {
                signals[Signal.SWITCH] = Switch.GetStateDataSignal(mwBoard);
            }

            if (toggleSwitch != null) {
                if (toggleSwitch.IsOn) {
                    DataSignal.Subscribe(signals[Signal.SWITCH]);
                } else {
                    DataSignal.Unsubscribe(signals[Signal.SWITCH]);
                }
            }
        }

        private void ledRedOn(object sender, RoutedEventArgs e) {
            Led.Pattern pattern = new Led.Pattern();
            Led.LoadPresetPattern(ref pattern, Led.PatternPreset.SOLID);
            Led.WritePattern(mwBoard, ref pattern, Led.Color.RED);
            Led.Play(mwBoard);
        }

        private void ledGreenOn(object sender, RoutedEventArgs e) {
            Led.Pattern pattern = new Led.Pattern();
            Led.LoadPresetPattern(ref pattern, Led.PatternPreset.BLINK);
            Led.WritePattern(mwBoard, ref pattern, Led.Color.GREEN);
            Led.Play(mwBoard);
        }

        private void ledBlueOn(object sender, RoutedEventArgs e) {
            Led.Pattern pattern = new Led.Pattern();
            Led.LoadPresetPattern(ref pattern, Led.PatternPreset.PULSE);
            Led.WritePattern(mwBoard, ref pattern, Led.Color.BLUE);
            Led.Play(mwBoard);
        }

        private void ledOff(object sender, RoutedEventArgs e) {
            Led.StopAndClear(mwBoard);
        }
    }
}

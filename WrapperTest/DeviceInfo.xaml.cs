using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MbientLab.MetaWear.Test {
    public enum LogLevel {
        SEVERE,
        INFO
    }
    public class ConsoleLineColorConverter : IValueConverter {
        public SolidColorBrush SevereColor { get; set; }
        public SolidColorBrush InfoColor { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language) {
            switch((LogLevel) value) {
                case LogLevel.SEVERE:
                    return SevereColor;
                case LogLevel.INFO:
                    return InfoColor;
                default:
                    throw new MissingMemberException("Unrecognized log level: " + value.ToString());
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }

    public class ConsoleLine {
        public ConsoleLine(LogLevel level, string value) {
            this.Level = level;
            this.Value = value;
        }

        public LogLevel Level { get; }
        public string Value { get; }
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

        private BluetoothLEDevice mwBoard;

        public DeviceInfo() {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e) {
            mwBoard = (BluetoothLEDevice) e.Parameter;

            foreach(var characteristic in mwBoard.GetGattService(DEVICE_INFO_SERVICE).GetAllCharacteristics()) {
                var result = await characteristic.ReadValueAsync();
                string value = result.Status == GattCommunicationStatus.Success ?
                    System.Text.Encoding.UTF8.GetString(result.Value.ToArray(), 0, (int)result.Value.Length) :
                    "N/A";
                outputListView.Items.Add(new ConsoleLine(LogLevel.INFO, DEVICE_INFO_NAMES[characteristic.Uuid] + ": " + value));
            }

            /*
            mwGattService = ((BluetoothLEDevice) e.Parameter).GetGattService(Gatt.METAWEAR_SERVICE);

            mwNotifyChar = mwGattService.GetCharacteristics(Constant.METAWEAR_NOTIFY_UUID).First();
            await mwNotifyChar.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.Notify);
            mwNotifyChar.ValueChanged += new TypedEventHandler<GattCharacteristic, GattValueChangedEventArgs>(metawearNotificationHandler);
            */
        }
    }
}

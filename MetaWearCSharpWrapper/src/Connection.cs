using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MbientLab.MetaWear {
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SendCommand(IntPtr board, IntPtr command, byte length);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ReceivedSensorData(IntPtr signal, ref Data sensorData);

    [StructLayout(LayoutKind.Sequential)]
    public struct Connection {
        /// <summary>
        /// Pointer to the function for the SendCommand delegate 
        /// </summary>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public SendCommand sendCommandDelegate;
        /// <summary>
        /// Pointer to the function for the ReceivedSensorData delegate
        /// </summary>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public ReceivedSensorData receivedSensorDataDelegate;

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_connection_init", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Init(ref Connection conn);
    }

    public sealed class Gatt {
        public static readonly Guid METAWEAR_SERVICE = new Guid("326A9000-85CB-9195-D9DD-464CFBBAE75A");
        public static readonly Guid METAWEAR_COMMAND_CHARACTERISTIC = new Guid("326A9001-85CB-9195-D9DD-464CFBBAE75A");
        public static readonly Guid METAWEAR_NOTIFY_CHARACTERISTIC = new Guid("326A9006-85CB-9195-D9DD-464CFBBAE75A");
    }
}

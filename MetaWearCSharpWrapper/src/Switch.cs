using System;
using System.Runtime.InteropServices;

namespace MbientLab.MetaWear {
    public sealed class Switch {
        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_switch_get_state_data_signal", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetStateDataSignal(IntPtr board);
    }
}

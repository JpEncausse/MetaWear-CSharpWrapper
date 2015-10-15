using System;
using System.Runtime.InteropServices;

namespace MbientLab.MetaWear {
    class Switch {
        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_switch_get_state_data_signal")]
        public static extern IntPtr GetStateDataSignal(IntPtr board);
    }
}

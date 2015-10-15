using System;
using System.Runtime.InteropServices;

namespace MbientLab.MetaWear {
    public sealed class MetaWearBoard {
        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_create_metawear_board")]
        public static extern IntPtr Create();

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_free_metawear_board")]
        public static extern void Free(IntPtr board);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_metawearboard_handle_response")]
        public static extern int HandleResponse(IntPtr board, IntPtr response, byte len);
    }
}

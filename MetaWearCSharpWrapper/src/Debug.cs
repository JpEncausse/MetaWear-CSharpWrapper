using System;
using System.Runtime.InteropServices;

namespace MbientLab.MetaWear {
    public sealed class Debug {
        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_debug_reset", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Reset(IntPtr board);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_debug_jump_to_bootloader", CallingConvention = CallingConvention.Cdecl)]
        public static extern void JumpToBootloader(IntPtr board);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_debug_disconnect", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Disconnect(IntPtr board);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_debug_reset_after_gc", CallingConvention = CallingConvention.Cdecl)]
        public static extern void ResetAfterGc(IntPtr board);
    }
}

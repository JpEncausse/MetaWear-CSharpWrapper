using System;
using System.Runtime.InteropServices;

namespace MbientLab.MetaWear {
    public sealed class Haptic {
        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_haptic_start_motor", CallingConvention = CallingConvention.Cdecl)]
        public static extern void StartMotor(IntPtr board, float dutyCycle, ushort pulseWidth);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_haptic_start_buzzer", CallingConvention = CallingConvention.Cdecl)]
        public static extern void StartBuzzer(IntPtr board, ushort pulseWidth);
    }
}

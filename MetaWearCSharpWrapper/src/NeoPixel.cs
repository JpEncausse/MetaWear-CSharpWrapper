using System;
using System.Runtime.InteropServices;

namespace MbientLab.MetaWear {
    public sealed class NeoPixel {
       public enum ColorOrdering {
            WS2811_RGB = 0,
            WS2811_RBG,
            WS2811_GRB,
            WS2811_GBR
        };

        public enum RotDirection {
            TOWARDS,
            AWAY
        };

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_neopixel_init_slow_strand", CallingConvention = CallingConvention.Cdecl)]
        public static extern void InitSlowStrand(IntPtr board, byte strand, byte gpioPin, byte nPixels, ColorOrdering ordering);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_neopixel_init_fast_strand", CallingConvention = CallingConvention.Cdecl)]
        public static extern void InitFastStrand(IntPtr board, byte strand, byte gpioPin, byte nPixels, ColorOrdering ordering);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_neopixel_free_strand", CallingConvention = CallingConvention.Cdecl)]
        public static extern void FreeStrand(IntPtr board, byte strand);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_neopixel_enable_hold", CallingConvention = CallingConvention.Cdecl)]
        public static extern void EnableHold(IntPtr board, byte strand);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_neopixel_disable_hold", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DisableHold(IntPtr board, byte strand);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_neopixel_clear", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Clear(IntPtr board, byte strand, byte start, byte end);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_neopixel_set_color", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetColor(IntPtr board, byte strand, byte pixel, byte red, byte green, byte blue);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_neopixel_rotate", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Rotate(IntPtr board, byte strand, byte count, ushort period, RotDirection direction);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_neopixel_rotate_indefinitely", CallingConvention = CallingConvention.Cdecl)]
        public static extern void RotateIndefinitely(IntPtr board, byte strand, ushort period, RotDirection direction);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_neopixel_stop_rotation", CallingConvention = CallingConvention.Cdecl)]
        public static extern void StopRotation(IntPtr board, byte strand);
    }
}

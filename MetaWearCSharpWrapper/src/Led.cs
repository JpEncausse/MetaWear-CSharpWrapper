﻿using System;
using System.Runtime.InteropServices;

namespace MbientLab.MetaWear {
    public sealed class Led {
        public const byte REPEAT_INDEFINITELY = 0xff;

        public enum Color {
            GREEN,
            RED,
            BLUE
        }

        public enum PatternPreset {
            BLINK,
            PULSE,
            SOLID
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Pattern {
            ushort riseTime;
            ushort highTime;
            ushort fallTime;
            ushort pulseDuration;
            byte highIntensity;
            byte lowIntensity;
            byte repeatCount;
        }

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_led_load_preset_pattern")]
        public static extern void LoadPresetPattern(ref Pattern pattern, PatternPreset preset);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_led_write_pattern")]
        public static extern void WritePattern(IntPtr board, ref Pattern pattern, Color color);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_led_autoplay")]
        public static extern void AutoPlay(IntPtr board);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_led_play")]
        public static extern void Play(IntPtr board);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_led_pause")]
        public static extern void Pause(IntPtr board);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_led_stop")]
        public static extern void Stop(IntPtr board);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_led_stop_and_clear")]
        public static extern void StopAndClear(IntPtr board);
    }
}

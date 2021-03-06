﻿using System;
using System.Runtime.InteropServices;

namespace MbientLab.MetaWear {
    public sealed class DataSignal {
        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_datasignal_subscribe", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Subscribe(IntPtr board);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_datasignal_unsubscribe", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Unsubscribe(IntPtr board);
    }
}

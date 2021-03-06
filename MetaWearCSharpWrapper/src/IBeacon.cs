﻿using System;
using System.Runtime.InteropServices;

namespace MbientLab.MetaWear {
    public sealed class IBeacon {
        public static byte[] GuidToByteArray(Guid guid) {
            byte[] guidBytes = guid.ToByteArray();

            // Implementation taken from SO: http://stackoverflow.com/a/16722909
            Array.Reverse(guidBytes, 0, 4);
            Array.Reverse(guidBytes, 4, 2);
            Array.Reverse(guidBytes, 6, 4);
            Array.Reverse(guidBytes);
            return guidBytes;
        }

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_ibeacon_set_major", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetMajor(IntPtr board, ushort major);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_ibeacon_set_minor", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetMinor(IntPtr board, ushort minor);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_ibeacon_set_period", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetPeriod(IntPtr board, ushort period);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_ibeacon_set_tx_power", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetTxPower(IntPtr board, sbyte power);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_ibeacon_set_rx_power", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetRxPower(IntPtr board, sbyte power);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_ibeacon_set_uuid", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetUuid(IntPtr board, byte[] uuid);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_ibeacon_enable", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Enable(IntPtr board, byte[] uuid);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_ibeacon_disable", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Disable(IntPtr board, byte[] uuid);
    }
}

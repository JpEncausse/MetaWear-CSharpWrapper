﻿using System;
using System.Runtime.InteropServices;

namespace MbientLab.MetaWear {
    public enum DataTypeId {
        UINT32 = 0,
        FLOAT,
        CARTESIAN_FLOAT
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Data {
        public IntPtr value;
        public DataTypeId typeId;
    }
}

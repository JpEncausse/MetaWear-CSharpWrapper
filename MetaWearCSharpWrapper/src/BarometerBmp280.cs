using System;
using System.Runtime.InteropServices;

namespace MbientLab.MetaWear {
    public sealed class BarometerBmp280 {
        public enum Oversampling {
            SKIP = 0,
            ULTRA_LOW_POWER,
            LOW_POWER,
            STANDARD,
            HIGH,
            ULTRA_HIGH
        };

        public enum IirFilter {
            OFF = 0,
            AVG_2,
            AVG_4,
            AVG_8,
            AVG_16
        };


        public enum StandBy {
            TIME_0_5MS = 0,
            TIME_62_5MS,
            TIME_125MS,
            TIME_250MS,
            TIME_500MS,
            TIME_1000MS,
            TIME_2000MS,
            TIME_4000MS
        };

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_baro_bmp280_get_pressure_data_signal")]
        public static extern IntPtr GetPressureDataSignal(IntPtr board);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_baro_bmp280_get_altitude_data_signal")]
        public static extern IntPtr GetAltitudeDataSignal(IntPtr board);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_baro_bmp280_set_oversampling")]
        public static extern void SetOversampling(IntPtr board, Oversampling oversampling);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_baro_bmp280_set_iir_filter")]
        public static extern void SetIirFilter(IntPtr board, IirFilter filter);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_baro_bmp280_set_standby_time")]
        public static extern void SetStandbyTime(IntPtr board, StandBy standbyTime);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_baro_bmp280_write_config")]
        public static extern IntPtr WriteConfig(IntPtr board);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_baro_bmp280_start")]
        public static extern IntPtr Start(IntPtr board);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_baro_bmp280_stop")]
        public static extern IntPtr Stop(IntPtr board);
    }
}

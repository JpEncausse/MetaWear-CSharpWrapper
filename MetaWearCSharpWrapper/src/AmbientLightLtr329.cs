using System;
using System.Runtime.InteropServices;

namespace MbientLab.MetaWear {
    public sealed class AmbientLightLtr329 {
        public enum SensorGain {
            GAIN_1X = 0,            // Defaut setting
            GAIN_2X,
            GAIN_4X,
            GAIN_8X,
            GAIN_48X,
            GAIN_96X
        }

        public enum IntegrationTime {
            TIME_100MS = 0,         // Default setting
            TIME_50MS,
            TIME_200MS,
            TIME_400MS,
            TIME_150MS,
            TIME_250MS,
            TIME_300MS,
            TIME_350MS
        }


        public enum MeasurementRate {
            RATE_50MS = 0,
            RATE_100MS,
            RATE_200MS,
            RATE_500MS,             // Default setting
            RATE_1000MS,
            RATE_2000MS
        };

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_als_ltr329_get_illuminance_data_signal", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetIlluminanceDataSignal(IntPtr board);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_als_ltr329_set_gain", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetGain(IntPtr board, SensorGain gain);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_als_ltr329_set_integration_time", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetIntegrationTime(IntPtr board, IntegrationTime time);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_als_ltr329_set_measurement_rate", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetMeasurementRate(IntPtr board, MeasurementRate rate);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_als_ltr329_write_config", CallingConvention = CallingConvention.Cdecl)]
        public static extern void WriteConfig(IntPtr board);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_als_ltr329_start", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Start(IntPtr board);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_als_ltr329_stop", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Stop(IntPtr board);
    }
}

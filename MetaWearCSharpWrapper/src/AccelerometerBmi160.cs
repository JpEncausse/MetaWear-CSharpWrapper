using System;
using System.Runtime.InteropServices;

namespace MbientLab.MetaWear {
    public sealed class AccelerometerBmi160 {
        public enum FullScaleRange {
            FSR_2G = 0,
            FSR_4G,
            FSR_8G,
            FSR_16G
        };

        public enum OutputDataRate {
            ODR_0_78125HZ = 0,
            ODR_1_5625HZ,
            ODR_3_125HZ,
            ODR_6_25HZ,
            ODR_12_5HZ,
            ODR_25HZ,
            ODR_50HZ,
            ODR_100HZ,
            ODR_200HZ,
            ODR_400HZ,
            ODR_800HZ,
            ODR_1600HZ
        };

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_acc_bmi160_get_acceleration_data_signal", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetAccelerationDataSignal(IntPtr board);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_acc_bmi160_set_odr", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetOutputDataRate(IntPtr board, OutputDataRate odr);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_acc_bmi160_set_range", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetFullScaleRange(IntPtr board, FullScaleRange range);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_acc_bmi160_write_acceleration_config", CallingConvention = CallingConvention.Cdecl)]
        public static extern void WriteAccelerationConfig(IntPtr board);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_acc_bmi160_start", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Start(IntPtr board);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_acc_bmi160_stop", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Stop(IntPtr board);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_acc_bmi160_enable_acceleration_sampling", CallingConvention = CallingConvention.Cdecl)]
        public static extern void EnableAccelerationSampling(IntPtr board);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_acc_bmi160_disable_acceleration_sampling", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DisableAccelerationSampling(IntPtr board);
    }
}

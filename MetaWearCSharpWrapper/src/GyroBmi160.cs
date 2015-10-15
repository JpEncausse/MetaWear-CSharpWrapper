using System;
using System.Runtime.InteropServices;

namespace MbientLab.MetaWear {
    public sealed class GyroBmi160 {
        public enum OutputDataRate {
            ODR_25HZ = 6,
            ODR_50HZ,
            ODR_100HZ,
            ODR_200HZ,
            ODR_400HZ,
            ODR_800HZ,
            ODR_1600HZ,
            ODR_3200HZ
        };


        public enum FullScaleRange {
            FSR_2000DPS = 0,
            FSR_1000DPS,
            FSR_500DPS,
            FSR_250DPS,
            FSR_125DPS
        };

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_gyro_bmi160_get_rotation_data_signal")]
        public static extern IntPtr GetRotationDataSignal(IntPtr board);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_gyro_bmi160_set_odr")]
        public static extern void SetOutputDataRate(IntPtr board, OutputDataRate odr);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_gyro_bmi160_set_range")]
        public static extern void SetFullScaleRange(IntPtr board, FullScaleRange range);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_gyro_bmi160_write_config")]
        public static extern void WriteConfig(IntPtr board);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_gyro_bmi160_start")]
        public static extern void Start(IntPtr board);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_gyro_bmi160_stop")]
        public static extern void Stop(IntPtr board);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_gyro_bmi160_enable_rotation_sampling")]
        public static extern void EnableRotationSampling(IntPtr board);

        [DllImport(Constant.METAWEAR_DLL, EntryPoint = "mbl_mw_gyro_bmi160_disable_rotation_sampling")]
        public static extern void DisableRotationSampling(IntPtr board);
    }
}

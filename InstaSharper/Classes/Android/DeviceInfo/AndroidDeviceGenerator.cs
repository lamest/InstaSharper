using System;
using System.Collections.Generic;
using System.Linq;

namespace InstaSharper.Classes.Android.DeviceInfo
{
    public class AndroidDeviceGenerator
    {
        private static readonly List<string> DevicesNames = new List<string>
        {
#if DEBUG
            AndroidDevices.Samsung_Galaxy_J1,
#endif
            AndroidDevices.LG_K10
        };

        public static Dictionary<string, AndroidDevice> AndroidAndroidDeviceSets = new Dictionary<string, AndroidDevice>
        {
#if DEBUG
            {
                AndroidDevices.Samsung_Galaxy_J1,
                new AndroidDevice
                {
                    AndroidBoardName = "none",
                    AndroidBootloader = "none",
                    DeviceBrand = "none",
                    DeviceModel = "none",
                    DeviceModelBoot = "none",
                    DeviceModelIdentifier = "none",
                    FirmwareBrand = "none",
                    FirmwareFingerprint = "none/none/none:4.4/none/none:none/release-keys",
                    FirmwareTags = "none",
                    FirmwareType = "none",
                    HardwareManufacturer = "samsung",
                    HardwareModel = "SM-J120F",
                    DeviceGuid = new Guid("3380f887-306f-4f6b-a589-5611aad57b3b"),
                    PhoneGuid = new Guid("90c38fda-2def-46d2-a066-2e90d138576e")
                }
            },
#endif
                        {
                AndroidDevices.LG_K10,
                new AndroidDevice
                {
                    AndroidBoardName = "none",
                    AndroidBootloader = "none",
                    DeviceBrand = "none",
                    DeviceModel = "none",
                    DeviceModelBoot = "none",
                    DeviceModelIdentifier = "none",
                    FirmwareBrand = "none",
                    FirmwareFingerprint = "none/none/none:6.0/none/none:none/release-keys",
                    FirmwareTags = "none",
                    FirmwareType = "none",
                    HardwareManufacturer = "LG",
                    HardwareModel = "LG-K430ds",
                    DeviceGuid = new Guid("6296e334-5f98-49ca-8ed4-a14185cf2168"),
                    PhoneGuid = new Guid("6c065a93-ce69-4d13-a50e-3f102ee84358")
                }
            },
        };

        public static AndroidDevice GetRandomAndroidDevice()
        {
            var random = new Random(DateTime.Now.Millisecond);
            var randmonDeviceIndex = random.Next(0, DevicesNames.Count);
            var randomDeviceName = DevicesNames[randmonDeviceIndex];
            return AndroidAndroidDeviceSets[randomDeviceName];
        }

        public static AndroidDevice GetByName(string name)
        {
            return AndroidAndroidDeviceSets[name];
        }

        public static AndroidDevice GetById(string deviceId)
        {
            return (from androidAndroidDeviceSet in AndroidAndroidDeviceSets
                    where androidAndroidDeviceSet.Value.DeviceId == deviceId
                    select androidAndroidDeviceSet.Value).FirstOrDefault();
        }
    }
}
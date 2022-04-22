using Assets.Scripts.Networks;
using Assets.Scripts.Objects.Electrical;
using Assets.Scripts.Objects.Pipes;
using Core.Shared;
using HarmonyLib;
using Objects.Pipes;

namespace EfficientDevices.Patches
{
    [HarmonyPatch(typeof(PowerTick), "ConsumePower")]
    public class PowerTick_ConsumePower
    {
        /// <summary>
        /// Patches PowerTick.ConsumePower to change the powerRequired parameter without modifying the original method (it causes bugs)
        /// </summary>
        /// <param name="device"></param>
        /// <param name="cableNetwork"></param>
        /// <param name="powerRequired"></param>
        static void Prefix(Device device, CableNetwork cableNetwork, ref float powerRequired)
        {
            if (device is TurboVolumePump)
            {
                powerRequired.AssignConfigValue(Mod.TurboPumpGas_MinMax);
            }

            if (device is AirConditioner)
            {
                powerRequired.AssignConfigValue(Mod.AirConditioner_MinMax);
            }
		}
    }
}

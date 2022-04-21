using Assets.Scripts.Objects.Electrical;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfficientDevices.Patches
{
    [HarmonyPatch(typeof(Cable), MethodType.Constructor)]
    public class Cable_Constructor
    {
        static float voltage => Mod.Cable_MaxVoltage.Value > 10 ? Mod.Cable_MaxVoltage.Value : 10;
        /// <summary>
        /// Patches Cable constructor to set the Cable.MaxVoltage to 10000
        /// </summary>
        /// <param name="__instance"></param>
        static void Postfix(Cable __instance)
        {
            __instance.MaxVoltage = voltage;
        }
    }
}

using HarmonyLib;
using Objects.Pipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfficientDevices
{
    [HarmonyPatch(typeof(TurboVolumePump), nameof(TurboVolumePump.GetUsedPower))]
    public class TurboVolumePump_GetUsedPower
    {
        static void Postfix(float __result)
        {
            //__result = __result / 10;
            //Mod.Log.LogInfo("result: " + __result);
        }
    }
}

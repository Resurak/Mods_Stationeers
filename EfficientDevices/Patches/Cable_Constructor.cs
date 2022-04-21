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
        static void Postfix(Cable __instance)
        {
            __instance.MaxVoltage = 10000f;
        }
    }
}

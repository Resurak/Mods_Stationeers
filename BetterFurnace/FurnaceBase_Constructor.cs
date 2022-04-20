using Assets.Scripts.Objects.Pipes;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterFurnace
{
    [HarmonyPatch(typeof(FurnaceBase), MethodType.Constructor)]
    public class FurnaceBase_Constructor
    {
        static void Postfix(FurnaceBase __instance)
        {
            __instance.OutputSetting = 0;
        }
    }
}

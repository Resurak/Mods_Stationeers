﻿using Assets.Scripts.Networks;
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
        static void Postfix(CableNetwork cableNetwork, ref float __result)
        {
            if (__result > Mod.TurboVolumePump_MaxPower.Value)
            {
                __result = Mod.TurboVolumePump_MaxPower.Value;
            }
        }
    }
}

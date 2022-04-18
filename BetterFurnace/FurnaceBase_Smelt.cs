using Assets.Scripts.Atmospherics;
using Assets.Scripts.Objects;
using Assets.Scripts.Objects.Items;
using Assets.Scripts.Objects.Pipes;
using HarmonyLib;
using Reagents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterFurnace
{
    [HarmonyPatch(typeof(FurnaceBase), nameof(FurnaceBase.Smelt))]
    public class FurnaceBase_Smelt
    {
        static int MinSetting => Mod.Furnace_MinSetting.Value;
        static int MaxSetting => Mod.Furnace_MaxSetting.Value;

        /// <summary>
        /// Patches the FurnaceBase.Smelt method to read the Furnace data Setting and using it to speed up smelting time
        /// </summary>
        /// <param name="dynamicThing">The mix inside the furnace</param>
        /// <param name="__instance">The FurnaceBase instance</param>
        /// <param name="___InternalAtmosphere">The FurnaceBase private field InternalAtmosphere</param>
        /// <param name="___ReagentMixture">The FurnaceBase private field InternalAtmosphere</param>
        /// <param name="__result">The result to give to the original method</param>
        /// <returns>Skips the original method if false</returns>
        static bool Prefix(DynamicThing dynamicThing, FurnaceBase __instance, Atmosphere ___InternalAtmosphere, ReagentMixture ___ReagentMixture, ref bool __result)
        {
            if (MinSetting > MaxSetting)
            {
                Mod.Log.LogFatal($"{nameof(Mod.Furnace_MinSetting)} is greater than {nameof(Mod.Furnace_MaxSetting)}, running default");
                return true;
            }

            // Getting the Setting output from the Furnace
            int setting = (int)Math.Round(__instance.OutputSetting);

            // Checking if Setting is valid
            if (setting < MinSetting)
            {
                setting = MinSetting;
            }
            else if (setting >= MaxSetting)
            {
                setting = MaxSetting;
            }

            // Starting loop
            for (int i = 0; i < setting; i++)
            {
                // Getting mix quantity
                IQuantity size = dynamicThing as IQuantity;

                // Checking if quantity is null or zero
                if (size == null)
                {
                    #if DEBUG
                    Mod.Log.LogError("DynamicThing as IQuantity returned null. Probably a bug");
                    #endif

                    __result = false;
                    break;
                }
                if (size.GetQuantity == 0)
                {
                    #if DEBUG
                    Mod.Log.LogWarning("DynamicThing as IQuantity equal to 0, breaking loop");
                    #endif

                    __result = false;
                    break;
                }

                // Calling original DynamicThing.Smelt method
                dynamicThing.Smelt(___InternalAtmosphere, ___ReagentMixture);
            }
            __result = true;
            return false;
        }
    }
}

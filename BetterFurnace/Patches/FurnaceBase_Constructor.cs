using Assets.Scripts.Objects.Pipes;
using HarmonyLib;

namespace BetterFurnace.Patches
{
    [HarmonyPatch(typeof(FurnaceBase), MethodType.Constructor)]
    public class FurnaceBase_Constructor
    {
        /// <summary>
        /// Patches the FurnaceBase constructor to set the Furnace Setting to 0 (to avoid problems)
        /// </summary>
        /// <param name="__instance"></param>
        static void Postfix(FurnaceBase __instance)
        {
            __instance.OutputSetting = 0;
        }
    }
}

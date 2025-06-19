using HarmonyLib;

namespace CapuTags.Patches
{

    [HarmonyPatch(typeof(FusionHub), "Leave")]
    public class OnLeavePatch
    {
        private static void Postfix(FusionHub __instance)
        {
            Stuff.values.Clear();
        }
    }
}

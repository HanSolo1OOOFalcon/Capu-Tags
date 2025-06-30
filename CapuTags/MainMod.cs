using CapuTags;
using HarmonyLib;
using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using MelonLoader;

[assembly: MelonInfo(typeof(MainMod), ModInfo.NAME, ModInfo.VERSION, ModInfo.AUTHOR)]

namespace CapuTags
{
    public class MainMod : MelonMod
    {
        public override void OnInitializeMelon() => ClassInjector.RegisterTypeInIl2Cpp<NametagComponent>();
    }

    [HarmonyPatch(typeof(FusionPlayer), "Spawned")]
    public class FusionPlayer_Spawned_Patch
    {
        private static void Postfix(FusionPlayer __instance)
        {
            if (__instance.IsLocalPlayer)
                return;
            
            __instance.transform.Find("Head").Find("headTarget").Find("PlayerModel").Find("CapuchinRemade").Find("Capuchin").Find("torso").Find("head").gameObject.AddComponent<NametagComponent>().player = __instance;
        }
    }
}
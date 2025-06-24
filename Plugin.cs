using BepInEx;
using BepInEx.Unity.IL2CPP;
using CapuTags.Patches;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;

namespace CapuTags
{
    [BepInPlugin(ModInfo.GUID, ModInfo.Name, ModInfo.Version)]
    public class Init : BasePlugin
    {
        private Harmony _harmonyInstance;

        public override void Load()
        {
            _harmonyInstance = HarmonyPatcher.Patch(ModInfo.GUID);
            ClassInjector.RegisterTypeInIl2Cpp<NametagComponent>();
        }

        public override bool Unload()
        {
            if (_harmonyInstance != null)
                HarmonyPatcher.Unpatch(_harmonyInstance);

            return true;
        }
    }

    [HarmonyPatch(typeof(FusionPlayer), "Spawned")]
    public class FusionPlayerSpawnedPatch
    {
        private static void Postfix(FusionPlayer __instance)
        {
            if (__instance.IsLocalPlayer || __instance == null)
                return;
            
            __instance.transform.Find("Head").gameObject.AddComponent<NametagComponent>().player = __instance;
        }
    }
}
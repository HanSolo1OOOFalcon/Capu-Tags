using BepInEx;
using BepInEx.Unity.IL2CPP;
using CapuTags.Patches;
using Fusion;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace CapuTags
{
    [BepInPlugin(ModInfo.GUID, ModInfo.Name, ModInfo.Version)]
    public class Init : BasePlugin
    {
        public static Init instance;
        public Harmony harmonyInstance;

        public override void Load()
        {
            harmonyInstance = HarmonyPatcher.Patch(ModInfo.GUID);
            instance = this;

            ClassInjector.RegisterTypeInIl2Cpp<NametagComponent>();

            AddComponent<Stuff>();
        }

        public override bool Unload()
        {
            if (harmonyInstance != null)
                HarmonyPatcher.Unpatch(harmonyInstance);

            return true;
        }
    }

    public class Stuff : MonoBehaviour
    {
        public static List<Il2CppSystem.ValueTuple<FusionPlayer, PlayerRef>> values = new List<Il2CppSystem.ValueTuple<FusionPlayer, PlayerRef>>();

        void Update()
        {
            if (!FusionHub.InRoom || FusionHub.Instance.SpawnedPlayers == null)
                return;

            if (values.Count != FusionHub.Instance.SpawnedPlayers.Count)
            {
                values.Clear();
                foreach (var player in FusionHub.Instance.SpawnedPlayers)
                {
                    values.Add(player);
                    var playerThing = player.Item1;
                    if (playerThing.IsLocalPlayer)
                        continue;

                    if (playerThing.transform.Find("Head").GetComponent<NametagComponent>() == null)
                    {
                        var thing = playerThing.transform.Find("Head").gameObject.AddComponent<NametagComponent>();
                        thing.playerName = playerThing.Username;
                        thing.playerColor = playerThing.__Color;
                    }
                }
            }
        }
    }
}

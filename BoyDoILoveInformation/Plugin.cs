using System.Collections.Generic;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace BoyDoILoveInformation;

[BepInPlugin(Constants.PluginGuid, Constants.PluginName, Constants.PluginVersion)]
public class Plugin : BaseUnityPlugin
{
    private const string GorillaInfoEndPointURL =
            "https://raw.githubusercontent.com/HanSolo1000Falcon/GorillaInfo/main/";

    public static Dictionary<string, string> KnownCheats;
    public static Dictionary<string, string> KnownMods;

    public static AssetBundle BDILIBundle;
    
    private void Start()
    {
        new Harmony(Constants.PluginGuid).PatchAll();
        GorillaTagger.OnPlayerSpawned(OnGameInitialized);
    }

    private void OnGameInitialized() { }
}
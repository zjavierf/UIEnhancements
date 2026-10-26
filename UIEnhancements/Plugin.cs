using HarmonyLib;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using JetBrains.Annotations;
using UIEnhancements.Configuration;
using UIEnhancements.Installers;
using SiraUtil.Zenject;
using IPALogger = IPA.Logging.Logger;

namespace UIEnhancements;

[Plugin(RuntimeOptions.DynamicInit)]
[UsedImplicitly]
internal class Plugin
{
    internal static IPALogger Log { get; private set; } = null!;
    private static readonly Harmony HarmonyInstance = new(nameof(UIEnhancements));

    [Init]
    public Plugin(IPALogger logger, Zenjector zenjector, Config conf)
    {
        Log = logger;
        PluginConfig.Instance = conf.Generated<PluginConfig>();
        
        zenjector.Install<MenuInstaller>(Location.Menu);
        Log.Info("UIEnhancements loaded");
    }

    [OnEnable]
    public void OnEnable() => HarmonyInstance.PatchAll();

    [OnDisable]
    public void OnDisable() => HarmonyInstance.UnpatchSelf();
}
#pragma warning disable CS0649
#nullable enable
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using HMUI;
using JetBrains.Annotations;
using SiraUtil.Zenject;
using UIEnhancements.Configuration;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UIEnhancements.UI;

[UsedImplicitly]
internal class LeaderboardBackground : IAsyncInitializable
{
    internal static LeaderboardBackground? Instance { get; private set; }

    private GameObject? _container;
    
    [UIValue("bg-alpha")]
    private float BgAlpha => PluginConfig.Instance.UseSelectiveLeaderboard ? PluginConfig.Instance.LeaderboardBackgroundAlpha : PluginConfig.Instance.BackgroundAlpha;

    [UIValue("bg-color")]
    private string BgColor => "#" + ColorUtility.ToHtmlStringRGB(PluginConfig.Instance.UseSelectiveLeaderboard 
        ? new Color(PluginConfig.Instance.LeaderboardBgColorR, PluginConfig.Instance.LeaderboardBgColorG, PluginConfig.Instance.LeaderboardBgColorB)
        : new Color(PluginConfig.Instance.BgColorR, PluginConfig.Instance.BgColorG, PluginConfig.Instance.BgColorB));

    [UIValue("border-alpha")]
    private float BorderAlpha => (PluginConfig.Instance.UseSelectiveLeaderboard ? PluginConfig.Instance.LeaderboardEnableBorder : PluginConfig.Instance.EnableBorder) 
        ? (PluginConfig.Instance.UseSelectiveLeaderboard ? PluginConfig.Instance.LeaderboardBorderAlpha : PluginConfig.Instance.BorderAlpha) 
        : 0f;

    [UIValue("border-color")]
    private string BorderColor => "#" + ColorUtility.ToHtmlStringRGB(PluginConfig.Instance.UseSelectiveLeaderboard 
        ? new Color(PluginConfig.Instance.LeaderboardBorderColorR, PluginConfig.Instance.LeaderboardBorderColorG, PluginConfig.Instance.LeaderboardBorderColorB)
        : new Color(PluginConfig.Instance.BorderColorR, PluginConfig.Instance.BorderColorG, PluginConfig.Instance.BorderColorB));
    
    public LeaderboardBackground()
    {
        Instance = this;
    }

    public async Task InitializeAsync(CancellationToken token)
    {
        await BuildOrReloadBsml();
    }

    internal async Task BuildOrReloadBsml()
    {
        Object.Destroy(_container);

        while (_container != null)
        {
            await Task.Yield();
        }

        if (!PluginConfig.Instance.EnableLeaderboardBackground)
        {
            return;
        }

        var platformVc = Resources.FindObjectsOfTypeAll<ViewController>()
            .FirstOrDefault(vc => vc.name == "PlatformLeaderboardViewController");

        Transform? targetParent = platformVc != null ? platformVc.transform : null;

        if (targetParent == null)
        {
            Plugin.Log.Warn("[UIEnhancements] PlatformLeaderboardViewController not found yet.");
            return;
        }

        _container = new GameObject("LeaderboardBackgroundContainer");
        _container.transform.SetParent(targetParent, false);
        
        ResetTransform();

        string bsmlContent = Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), "UIEnhancements.UI.BSML.LeaderboardBackground.bsml");

        BSMLParser.Instance.Parse(bsmlContent, _container, this);
        Plugin.Log.Info("[UIEnhancements] Successfully built LeaderboardBackground.");
    }

    private void ResetTransform()
    {
        if (_container == null) return;

        RectTransform rect = _container.GetComponent<RectTransform>() ?? _container.AddComponent<RectTransform>();
        rect.localPosition = new Vector3(0f, 0f, -5f);
        rect.localRotation = Quaternion.identity;
        rect.localScale = Vector3.one;
        
        _container.transform.SetAsFirstSibling();
    }
}
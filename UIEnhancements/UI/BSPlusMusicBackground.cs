#pragma warning disable CS0649
#nullable enable
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using JetBrains.Annotations;
using SiraUtil.Zenject;
using UIEnhancements.Configuration;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace UIEnhancements.UI;

[UsedImplicitly]
internal class BSPlusMusicBackground : IAsyncInitializable
{
    internal static BSPlusMusicBackground? Instance { get; private set; }
    private GameObject? _container;

    [UIComponent("BSPlusMusicBackgroundPanel")]
    private readonly HorizontalLayoutGroup? _backgroundPanel = null!;
    
    [UIValue("bg-alpha")]
    private float BgAlpha => PluginConfig.Instance.UseSelectiveBSPlusMusic ? PluginConfig.Instance.BSPlusMusicBackgroundAlpha : PluginConfig.Instance.BackgroundAlpha;

    [UIValue("bg-color")]
    private string BgColor => "#" + ColorUtility.ToHtmlStringRGB(PluginConfig.Instance.UseSelectiveBSPlusMusic 
        ? new Color(PluginConfig.Instance.BSPlusMusicBgColorR, PluginConfig.Instance.BSPlusMusicBgColorG, PluginConfig.Instance.BSPlusMusicBgColorB)
        : new Color(PluginConfig.Instance.BgColorR, PluginConfig.Instance.BgColorG, PluginConfig.Instance.BgColorB));

    [UIValue("border-alpha")]
    private float BorderAlpha => (PluginConfig.Instance.UseSelectiveBSPlusMusic ? PluginConfig.Instance.BSPlusMusicEnableBorder : PluginConfig.Instance.EnableBorder) 
        ? (PluginConfig.Instance.UseSelectiveBSPlusMusic ? PluginConfig.Instance.BSPlusMusicBorderAlpha : PluginConfig.Instance.BorderAlpha) 
        : 0f;

    [UIValue("border-color")]
    private string BorderColor => "#" + ColorUtility.ToHtmlStringRGB(PluginConfig.Instance.UseSelectiveBSPlusMusic 
        ? new Color(PluginConfig.Instance.BSPlusMusicBorderColorR, PluginConfig.Instance.BSPlusMusicBorderColorG, PluginConfig.Instance.BSPlusMusicBorderColorB)
        : new Color(PluginConfig.Instance.BorderColorR, PluginConfig.Instance.BorderColorG, PluginConfig.Instance.BorderColorB));

    public BSPlusMusicBackground() => Instance = this;

    public async Task InitializeAsync(CancellationToken token) => await BuildOrReloadBsml();

    internal async Task BuildOrReloadBsml()
    {
        Object.Destroy(_container);
        while (_container != null) await Task.Yield();

        if (!PluginConfig.Instance.EnableBSPlusMusicBackground) return;

        var canvas = Resources.FindObjectsOfTypeAll<Canvas>()
            .FirstOrDefault(c => c.name == "ChatPlexMod_MenuMusic");

        if (canvas == null) return;

        _container = new GameObject("BSPlusMusicBackgroundContainer");
        _container.transform.SetParent(canvas.transform, false);
        ResetTransform();

        string bsmlContent = Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), "UIEnhancements.UI.BSML.BSPlusMusicBackground.bsml");

        BSMLParser.Instance.Parse(bsmlContent, _container, this);
        Plugin.Log.Info("[UIEnhancements] Successfully built BSPlusMusicBackground.");
    }

    private void ResetTransform()
    {
        if (_container == null) return;
        RectTransform rect = _container.GetComponent<RectTransform>() ?? _container.AddComponent<RectTransform>();
        rect.localPosition = Vector3.zero;
        rect.localRotation = Quaternion.identity;
        rect.localScale = Vector3.one;
        _container.transform.SetAsFirstSibling();
    }
}
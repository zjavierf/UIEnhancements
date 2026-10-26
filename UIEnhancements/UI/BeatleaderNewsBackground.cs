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
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace UIEnhancements.UI;

[UsedImplicitly]
internal class BeatleaderNewsBackground : IAsyncInitializable
{
    internal static BeatleaderNewsBackground? Instance { get; private set; }
    private GameObject? _container;

    [UIComponent("BeatleaderNewsBackgroundPanel")]
    private readonly HorizontalLayoutGroup? _backgroundPanel = null!;
    
    [UIValue("bg-alpha")]
    private float BgAlpha => PluginConfig.Instance.UseSelectiveBeatleaderNews ? PluginConfig.Instance.BeatleaderNewsBackgroundAlpha : PluginConfig.Instance.BackgroundAlpha;

    [UIValue("bg-color")]
    private string BgColor => "#" + ColorUtility.ToHtmlStringRGB(PluginConfig.Instance.UseSelectiveBeatleaderNews 
        ? new Color(PluginConfig.Instance.BeatleaderNewsBgColorR, PluginConfig.Instance.BeatleaderNewsBgColorG, PluginConfig.Instance.BeatleaderNewsBgColorB)
        : new Color(PluginConfig.Instance.BgColorR, PluginConfig.Instance.BgColorG, PluginConfig.Instance.BgColorB));

    [UIValue("border-alpha")]
    private float BorderAlpha => (PluginConfig.Instance.UseSelectiveBeatleaderNews ? PluginConfig.Instance.BeatleaderNewsEnableBorder : PluginConfig.Instance.EnableBorder) 
        ? (PluginConfig.Instance.UseSelectiveBeatleaderNews ? PluginConfig.Instance.BeatleaderNewsBorderAlpha : PluginConfig.Instance.BorderAlpha) 
        : 0f;

    [UIValue("border-color")]
    private string BorderColor => "#" + ColorUtility.ToHtmlStringRGB(PluginConfig.Instance.UseSelectiveBeatleaderNews 
        ? new Color(PluginConfig.Instance.BeatleaderNewsBorderColorR, PluginConfig.Instance.BeatleaderNewsBorderColorG, PluginConfig.Instance.BeatleaderNewsBorderColorB)
        : new Color(PluginConfig.Instance.BorderColorR, PluginConfig.Instance.BorderColorG, PluginConfig.Instance.BorderColorB));

    public BeatleaderNewsBackground() => Instance = this;

    public async Task InitializeAsync(CancellationToken token) => await BuildOrReloadBsml();

    internal async Task BuildOrReloadBsml()
    {
        Object.Destroy(_container);
        while (_container != null) await Task.Yield();

        if (!PluginConfig.Instance.EnableBeatleaderNewsBackground) return;

        var vc = Resources.FindObjectsOfTypeAll<ViewController>()
            .FirstOrDefault(v => v.GetType().FullName == "BeatLeader.UI.MainMenu.BeatLeaderNewsViewController");

        if (vc == null) return;

        _container = new GameObject("BeatleaderNewsBackgroundContainer");
        _container.transform.SetParent(vc.transform, false);
        ResetTransform();

        string bsmlContent = Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), "UIEnhancements.UI.BSML.BeatleaderNewsBackground.bsml");

        BSMLParser.Instance.Parse(bsmlContent, _container, this);
        Plugin.Log.Info("[UIEnhancements] Successfully built BeatleaderNewsBackground.");
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
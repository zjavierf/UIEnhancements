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
internal class ModsBackground : IAsyncInitializable
{
    internal static ModsBackground? Instance { get; private set; }

    private GameObject? _container;
    
    [UIValue("bg-alpha")]
    private float BgAlpha => PluginConfig.Instance.UseSelectiveMods ? PluginConfig.Instance.ModsBackgroundAlpha : PluginConfig.Instance.BackgroundAlpha;

    [UIValue("bg-color")]
    private string BgColor => "#" + ColorUtility.ToHtmlStringRGB(PluginConfig.Instance.UseSelectiveMods 
        ? new Color(PluginConfig.Instance.ModsBgColorR, PluginConfig.Instance.ModsBgColorG, PluginConfig.Instance.ModsBgColorB)
        : new Color(PluginConfig.Instance.BgColorR, PluginConfig.Instance.BgColorG, PluginConfig.Instance.BgColorB));

    [UIValue("border-alpha")]
    private float BorderAlpha => (PluginConfig.Instance.UseSelectiveMods ? PluginConfig.Instance.ModsEnableBorder : PluginConfig.Instance.EnableBorder) 
        ? (PluginConfig.Instance.UseSelectiveMods ? PluginConfig.Instance.ModsBorderAlpha : PluginConfig.Instance.BorderAlpha) 
        : 0f;

    [UIValue("border-color")]
    private string BorderColor => "#" + ColorUtility.ToHtmlStringRGB(PluginConfig.Instance.UseSelectiveMods 
        ? new Color(PluginConfig.Instance.ModsBorderColorR, PluginConfig.Instance.ModsBorderColorG, PluginConfig.Instance.ModsBorderColorB)
        : new Color(PluginConfig.Instance.BorderColorR, PluginConfig.Instance.BorderColorG, PluginConfig.Instance.BorderColorB));
    
    public ModsBackground()
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

        if (!PluginConfig.Instance.EnableModsBackground)
        {
            return;
        }

        var menuButtonsVc = Resources.FindObjectsOfTypeAll<ViewController>()
            .FirstOrDefault(vc => vc.name == "MenuButtonsViewController");

        Transform? targetParent = menuButtonsVc != null ? menuButtonsVc.transform : null;

        if (targetParent == null)
        {
            Plugin.Log.Warn("[UIEnhancements] MenuButtonsViewController not found yet.");
            return;
        }

        _container = new GameObject("ModsBackgroundContainer");
        _container.transform.SetParent(targetParent, false);
        
        ResetTransform();

        string bsmlContent = Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), "UIEnhancements.UI.BSML.ModsBackground.bsml");

        BSMLParser.Instance.Parse(bsmlContent, _container, this);
        Plugin.Log.Info("[UIEnhancements] Successfully built ModsBackground.");
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
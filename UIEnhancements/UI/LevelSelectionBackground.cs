#pragma warning disable CS0649
#nullable enable
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using JetBrains.Annotations;
using SiraUtil.Zenject;
using UIEnhancements.Configuration;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace UIEnhancements.UI;

[UsedImplicitly]
internal class LevelSelectionBackground : IAsyncInitializable
{
    internal static LevelSelectionBackground? Instance { get; private set; }

    [Inject] private readonly LevelSelectionNavigationController _levelSelectionNavigationController = null!;

    private GameObject? _container;
    
    [UIValue("bg-alpha")]
    private float BgAlpha => PluginConfig.Instance.UseSelectiveLevelSelection ? PluginConfig.Instance.LevelSelectionBackgroundAlpha : PluginConfig.Instance.BackgroundAlpha;

    [UIValue("bg-color")]
    private string BgColor => "#" + ColorUtility.ToHtmlStringRGB(PluginConfig.Instance.UseSelectiveLevelSelection 
        ? new Color(PluginConfig.Instance.LevelSelectionBgColorR, PluginConfig.Instance.LevelSelectionBgColorG, PluginConfig.Instance.LevelSelectionBgColorB)
        : new Color(PluginConfig.Instance.BgColorR, PluginConfig.Instance.BgColorG, PluginConfig.Instance.BgColorB));
    
    [UIValue("border-alpha")]
    private float BorderAlpha => (PluginConfig.Instance.UseSelectiveLevelSelection ? PluginConfig.Instance.LevelSelectionEnableBorder : PluginConfig.Instance.EnableBorder) 
        ? (PluginConfig.Instance.UseSelectiveLevelSelection ? PluginConfig.Instance.LevelSelectionBorderAlpha : PluginConfig.Instance.BorderAlpha) 
        : 0f;

    [UIValue("border-color")]
    private string BorderColor => "#" + ColorUtility.ToHtmlStringRGB(PluginConfig.Instance.UseSelectiveLevelSelection 
        ? new Color(PluginConfig.Instance.LevelSelectionBorderColorR, PluginConfig.Instance.LevelSelectionBorderColorG, PluginConfig.Instance.LevelSelectionBorderColorB)
        : new Color(PluginConfig.Instance.BorderColorR, PluginConfig.Instance.BorderColorG, PluginConfig.Instance.BorderColorB));

    public LevelSelectionBackground()
    {
        Instance = this;
    }

    public async Task InitializeAsync(CancellationToken token)
    {
        _levelSelectionNavigationController.didActivateEvent += OnViewActivated;
        await BuildOrReloadBsml();
    }

    private void OnViewActivated(bool firstActivation, bool addedToHierarchy, bool screenSystemIsEnabled)
    {
        if (!firstActivation && _container != null)
        {
            ResetTransform();
        }
    }

    internal async Task BuildOrReloadBsml()
    {
        Object.Destroy(_container);

        while (_container != null)
        {
            await Task.Yield();
        }

        if (!PluginConfig.Instance.EnableLevelSelectionBackground)
        {
            return;
        }

        Transform parentTransform = _levelSelectionNavigationController.transform;

        _container = new GameObject("LevelSelectionBackgroundContainer");
        _container.transform.SetParent(parentTransform, false);
        
        ResetTransform();

        string bsmlContent = Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), "UIEnhancements.UI.BSML.LevelSelectionBackground.bsml");

        BSMLParser.Instance.Parse(bsmlContent, _container, this);
        Plugin.Log.Info("[UIEnhancements] Successfully built LevelSelectionBackground.");
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
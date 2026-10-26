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
internal class GameplaySettingsBackground : IAsyncInitializable
{
    internal static GameplaySettingsBackground? Instance { get; private set; }

    [Inject] private readonly GameplaySetupViewController _gameplaySetupViewController = null!;

    private GameObject? _container;
    
    [UIValue("bg-alpha")]
    private float BgAlpha => PluginConfig.Instance.UseSelectiveGameplaySettings ? PluginConfig.Instance.GameplaySettingsBackgroundAlpha : PluginConfig.Instance.BackgroundAlpha;

    [UIValue("bg-color")]
    private string BgColor => "#" + ColorUtility.ToHtmlStringRGB(PluginConfig.Instance.UseSelectiveGameplaySettings 
        ? new Color(PluginConfig.Instance.GameplaySettingsBgColorR, PluginConfig.Instance.GameplaySettingsBgColorG, PluginConfig.Instance.GameplaySettingsBgColorB)
        : new Color(PluginConfig.Instance.BgColorR, PluginConfig.Instance.BgColorG, PluginConfig.Instance.BgColorB));

    [UIValue("border-alpha")]
    private float BorderAlpha => (PluginConfig.Instance.UseSelectiveGameplaySettings ? PluginConfig.Instance.GameplaySettingsEnableBorder : PluginConfig.Instance.EnableBorder) 
        ? (PluginConfig.Instance.UseSelectiveGameplaySettings ? PluginConfig.Instance.GameplaySettingsBorderAlpha : PluginConfig.Instance.BorderAlpha) 
        : 0f;

    [UIValue("border-color")]
    private string BorderColor => "#" + ColorUtility.ToHtmlStringRGB(PluginConfig.Instance.UseSelectiveGameplaySettings 
        ? new Color(PluginConfig.Instance.GameplaySettingsBorderColorR, PluginConfig.Instance.GameplaySettingsBorderColorG, PluginConfig.Instance.GameplaySettingsBorderColorB)
        : new Color(PluginConfig.Instance.BorderColorR, PluginConfig.Instance.BorderColorG, PluginConfig.Instance.BorderColorB));
    
    public GameplaySettingsBackground()
    {
        Instance = this;
    }

    public async Task InitializeAsync(CancellationToken token)
    {
        _gameplaySetupViewController.didActivateEvent += OnViewActivated;
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

        if (!PluginConfig.Instance.EnableGameplaySettingsBackground)
        {
            return;
        }

        Transform parentTransform = _gameplaySetupViewController.transform;

        _container = new GameObject("GameplaySettingsBackgroundContainer");
        _container.transform.SetParent(parentTransform, false);
        
        ResetTransform();

        string bsmlContent = Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), "UIEnhancements.UI.BSML.GameplaySettingsBackground.bsml");

        BSMLParser.Instance.Parse(bsmlContent, _container, this);
        Plugin.Log.Info("[UIEnhancements] Successfully built GameplaySettingsBackground.");
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
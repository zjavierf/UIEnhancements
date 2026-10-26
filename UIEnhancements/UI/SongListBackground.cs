#pragma warning disable CS0649
#nullable enable
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using JetBrains.Annotations;
using UIEnhancements.Configuration;
using SiraUtil.Zenject;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace UIEnhancements.UI;

[UsedImplicitly]
internal class SongListBackground : IAsyncInitializable
{
    internal static SongListBackground? Instance { get; private set; }

    [Inject] private readonly LevelCollectionViewController _levelCollectionViewController = null!;

    private GameObject? _container;
    
    [UIValue("bg-alpha")]
    private float BgAlpha => PluginConfig.Instance.UseSelectiveSongList ? PluginConfig.Instance.SongListBackgroundAlpha : PluginConfig.Instance.BackgroundAlpha;

    [UIValue("bg-color")]
    private string BgColor => "#" + ColorUtility.ToHtmlStringRGB(PluginConfig.Instance.UseSelectiveSongList 
        ? new Color(PluginConfig.Instance.SongListBgColorR, PluginConfig.Instance.SongListBgColorG, PluginConfig.Instance.SongListBgColorB)
        : new Color(PluginConfig.Instance.BgColorR, PluginConfig.Instance.BgColorG, PluginConfig.Instance.BgColorB));

    [UIValue("border-alpha")]
    private float BorderAlpha => (PluginConfig.Instance.UseSelectiveSongList ? PluginConfig.Instance.SongListEnableBorder : PluginConfig.Instance.EnableBorder) 
        ? (PluginConfig.Instance.UseSelectiveSongList ? PluginConfig.Instance.SongListBorderAlpha : PluginConfig.Instance.BorderAlpha) 
        : 0f;

    [UIValue("border-color")]
    private string BorderColor => "#" + ColorUtility.ToHtmlStringRGB(PluginConfig.Instance.UseSelectiveSongList 
        ? new Color(PluginConfig.Instance.SongListBorderColorR, PluginConfig.Instance.SongListBorderColorG, PluginConfig.Instance.SongListBorderColorB)
        : new Color(PluginConfig.Instance.BorderColorR, PluginConfig.Instance.BorderColorG, PluginConfig.Instance.BorderColorB));
    
    public SongListBackground()
    {
        Instance = this;
    }

    public async Task InitializeAsync(CancellationToken token)
    {
        _levelCollectionViewController.didActivateEvent += OnViewActivated;
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

        if (!PluginConfig.Instance.EnableSongListBackground)
        {
            return;
        }
        
        int retries = 0;
        while (_levelCollectionViewController == null || _levelCollectionViewController.transform == null)
        {
            if (retries++ > 50) return;
            await Task.Delay(100);
        }

        Transform parentTransform = _levelCollectionViewController.transform.Find("LevelCollectionTableView") ?? _levelCollectionViewController.transform;

        _container = new GameObject("SongListBackgroundContainer");
        _container.transform.SetParent(parentTransform, false);
        
        ResetTransform();

        string bsmlContent = Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), "UIEnhancements.UI.BSML.SongListBackground.bsml");

        BSMLParser.Instance.Parse(bsmlContent, _container, this);
        Plugin.Log.Info("[UIEnhancements] Successfully built SongListBackground.");
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
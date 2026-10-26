#nullable enable
using System.ComponentModel;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using JetBrains.Annotations;
using UIEnhancements.Configuration;
using UnityEngine;
using Zenject;

namespace UIEnhancements.UI.SettingsFlow;

[HotReload(RelativePathToLayout = @"./ModSettingsView.bsml")]
[ViewDefinition("UIEnhancements.UI.SettingsFlow.ModSettingsView.bsml")]
[UsedImplicitly]
public class UIEnhancementsSettingsViewController : BSMLAutomaticViewController, INotifyPropertyChanged
{
    public new event PropertyChangedEventHandler? PropertyChanged;
    private UIEnhancementsFlowCoordinator? _flowCoordinator;

    [Inject]
    public void Construct(UIEnhancementsFlowCoordinator flowCoordinator)
    {
        _flowCoordinator = flowCoordinator;
    }

    private void RefreshAllBackgrounds()
    {
        _ = SongListBackground.Instance?.BuildOrReloadBsml();
        _ = GameplaySettingsBackground.Instance?.BuildOrReloadBsml();
        _ = LevelSelectionBackground.Instance?.BuildOrReloadBsml();
        _ = LeaderboardBackground.Instance?.BuildOrReloadBsml();
        _ = ModsBackground.Instance?.BuildOrReloadBsml();
        _ = BeatleaderNewsBackground.Instance?.BuildOrReloadBsml();
        _ = BSPlusMusicBackground.Instance?.BuildOrReloadBsml();
    }

    // --- Toggles ---
    [UIValue("song-list-bg")]
    public bool EnableSongListBg
    {
        get => PluginConfig.Instance.EnableSongListBackground;
        set { PluginConfig.Instance.EnableSongListBackground = value; _ = SongListBackground.Instance?.BuildOrReloadBsml(); }
    }

    [UIValue("settings-bg")]
    public bool EnableSettingsBg
    {
        get => PluginConfig.Instance.EnableGameplaySettingsBackground;
        set { PluginConfig.Instance.EnableGameplaySettingsBackground = value; _ = GameplaySettingsBackground.Instance?.BuildOrReloadBsml(); }
    }

    [UIValue("main-bg")]
    public bool EnableMainBg
    {
        get => PluginConfig.Instance.EnableLevelSelectionBackground;
        set { PluginConfig.Instance.EnableLevelSelectionBackground = value; _ = LevelSelectionBackground.Instance?.BuildOrReloadBsml(); }
    }

    [UIValue("leaderboard-bg")]
    public bool EnableLeaderboardBg
    {
        get => PluginConfig.Instance.EnableLeaderboardBackground;
        set { PluginConfig.Instance.EnableLeaderboardBackground = value; _ = LeaderboardBackground.Instance?.BuildOrReloadBsml(); }
    }
    
    [UIValue("beatleader-news-bg")]
    public bool EnableBeatleaderNewsBg
    {
        get => PluginConfig.Instance.EnableBeatleaderNewsBackground;
        set { PluginConfig.Instance.EnableBeatleaderNewsBackground = value; _ = BeatleaderNewsBackground.Instance?.BuildOrReloadBsml(); }
    }

    [UIValue("bsplus-music-bg")]
    public bool EnableBSPlusMusicBg
    {
        get => PluginConfig.Instance.EnableBSPlusMusicBackground;
        set { PluginConfig.Instance.EnableBSPlusMusicBackground = value; _ = BSPlusMusicBackground.Instance?.BuildOrReloadBsml(); }
    }

    [UIValue("mods-bg")]
    public bool EnableModsBg
    {
        get => PluginConfig.Instance.EnableModsBackground;
        set { PluginConfig.Instance.EnableModsBackground = value; _ = ModsBackground.Instance?.BuildOrReloadBsml(); }
    }

    // --- Global Customization ---
    [UIValue("bg-alpha")]
    public float BackgroundAlpha
    {
        get => PluginConfig.Instance.BackgroundAlpha;
        set { PluginConfig.Instance.BackgroundAlpha = value; RefreshAllBackgrounds(); }
    }

    [UIValue("bg-color")]
    public Color BgColor
    {
        get => new Color(PluginConfig.Instance.BgColorR, PluginConfig.Instance.BgColorG, PluginConfig.Instance.BgColorB);
        set { PluginConfig.Instance.BgColorR = value.r; PluginConfig.Instance.BgColorG = value.g; PluginConfig.Instance.BgColorB = value.b; RefreshAllBackgrounds(); }
    }
    
    [UIValue("enable-border")]
    public bool EnableBorder
    {
        get => PluginConfig.Instance.EnableBorder;
        set { PluginConfig.Instance.EnableBorder = value; RefreshAllBackgrounds(); }
    }

    [UIValue("border-color")]
    public Color BorderColor
    {
        get => new Color(PluginConfig.Instance.BorderColorR, PluginConfig.Instance.BorderColorG, PluginConfig.Instance.BorderColorB);
        set { PluginConfig.Instance.BorderColorR = value.r; PluginConfig.Instance.BorderColorG = value.g; PluginConfig.Instance.BorderColorB = value.b; RefreshAllBackgrounds(); }
    }
    
    [UIValue("border-alpha")]
    public float BorderAlpha
    {
        get => PluginConfig.Instance.BorderAlpha;
        set { PluginConfig.Instance.BorderAlpha = value; RefreshAllBackgrounds(); }
    }

    // --- Song List Selective ---
    [UIValue("selective-song-list")]
    public bool UseSelectiveSongList
    {
        get => PluginConfig.Instance.UseSelectiveSongList;
        set { PluginConfig.Instance.UseSelectiveSongList = value; _ = SongListBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("song-list-alpha")]
    public float SongListAlpha
    {
        get => PluginConfig.Instance.SongListBackgroundAlpha;
        set { PluginConfig.Instance.SongListBackgroundAlpha = value; _ = SongListBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("song-list-color")]
    public Color SongListColor
    {
        get => new Color(PluginConfig.Instance.SongListBgColorR, PluginConfig.Instance.SongListBgColorG, PluginConfig.Instance.SongListBgColorB);
        set { PluginConfig.Instance.SongListBgColorR = value.r; PluginConfig.Instance.SongListBgColorG = value.g; PluginConfig.Instance.SongListBgColorB = value.b; _ = SongListBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("song-list-enable-border")]
    public bool SongListEnableBorder
    {
        get => PluginConfig.Instance.SongListEnableBorder;
        set { PluginConfig.Instance.SongListEnableBorder = value; _ = SongListBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("song-list-border-color")]
    public Color SongListBorderColor
    {
        get => new Color(PluginConfig.Instance.SongListBorderColorR, PluginConfig.Instance.SongListBorderColorG, PluginConfig.Instance.SongListBorderColorB);
        set { PluginConfig.Instance.SongListBorderColorR = value.r; PluginConfig.Instance.SongListBorderColorG = value.g; PluginConfig.Instance.SongListBorderColorB = value.b; _ = SongListBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("song-list-border-alpha")]
    public float SongListBorderAlpha
    {
        get => PluginConfig.Instance.SongListBorderAlpha;
        set { PluginConfig.Instance.SongListBorderAlpha = value; _ = SongListBackground.Instance?.BuildOrReloadBsml(); }
    }

    // --- Gameplay Settings Selective ---
    [UIValue("selective-settings")]
    public bool UseSelectiveSettings
    {
        get => PluginConfig.Instance.UseSelectiveGameplaySettings;
        set { PluginConfig.Instance.UseSelectiveGameplaySettings = value; _ = GameplaySettingsBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("settings-alpha")]
    public float SettingsAlpha
    {
        get => PluginConfig.Instance.GameplaySettingsBackgroundAlpha;
        set { PluginConfig.Instance.GameplaySettingsBackgroundAlpha = value; _ = GameplaySettingsBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("settings-color")]
    public Color SettingsColor
    {
        get => new Color(PluginConfig.Instance.GameplaySettingsBgColorR, PluginConfig.Instance.GameplaySettingsBgColorG, PluginConfig.Instance.GameplaySettingsBgColorB);
        set { PluginConfig.Instance.GameplaySettingsBgColorR = value.r; PluginConfig.Instance.GameplaySettingsBgColorG = value.g; PluginConfig.Instance.GameplaySettingsBgColorB = value.b; _ = GameplaySettingsBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("settings-enable-border")]
    public bool SettingsEnableBorder
    {
        get => PluginConfig.Instance.GameplaySettingsEnableBorder;
        set { PluginConfig.Instance.GameplaySettingsEnableBorder = value; _ = GameplaySettingsBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("settings-border-color")]
    public Color SettingsBorderColor
    {
        get => new Color(PluginConfig.Instance.GameplaySettingsBorderColorR, PluginConfig.Instance.GameplaySettingsBorderColorG, PluginConfig.Instance.GameplaySettingsBorderColorB);
        set { PluginConfig.Instance.GameplaySettingsBorderColorR = value.r; PluginConfig.Instance.GameplaySettingsBorderColorG = value.g; PluginConfig.Instance.GameplaySettingsBorderColorB = value.b; _ = GameplaySettingsBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("settings-border-alpha")]
    public float SettingsBorderAlpha
    {
        get => PluginConfig.Instance.GameplaySettingsBorderAlpha;
        set { PluginConfig.Instance.GameplaySettingsBorderAlpha = value; _ = GameplaySettingsBackground.Instance?.BuildOrReloadBsml(); }
    }

    // --- Level Selection Selective ---
    [UIValue("selective-main")]
    public bool UseSelectiveMain
    {
        get => PluginConfig.Instance.UseSelectiveLevelSelection;
        set { PluginConfig.Instance.UseSelectiveLevelSelection = value; _ = LevelSelectionBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("main-alpha")]
    public float MainAlpha
    {
        get => PluginConfig.Instance.LevelSelectionBackgroundAlpha;
        set { PluginConfig.Instance.LevelSelectionBackgroundAlpha = value; _ = LevelSelectionBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("main-color")]
    public Color MainColor
    {
        get => new Color(PluginConfig.Instance.LevelSelectionBgColorR, PluginConfig.Instance.LevelSelectionBgColorG, PluginConfig.Instance.LevelSelectionBgColorB);
        set { PluginConfig.Instance.LevelSelectionBgColorR = value.r; PluginConfig.Instance.LevelSelectionBgColorG = value.g; PluginConfig.Instance.LevelSelectionBgColorB = value.b; _ = LevelSelectionBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("main-enable-border")]
    public bool MainEnableBorder
    {
        get => PluginConfig.Instance.LevelSelectionEnableBorder;
        set { PluginConfig.Instance.LevelSelectionEnableBorder = value; _ = LevelSelectionBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("main-border-color")]
    public Color MainBorderColor
    {
        get => new Color(PluginConfig.Instance.LevelSelectionBorderColorR, PluginConfig.Instance.LevelSelectionBorderColorG, PluginConfig.Instance.LevelSelectionBorderColorB);
        set { PluginConfig.Instance.LevelSelectionBorderColorR = value.r; PluginConfig.Instance.LevelSelectionBorderColorG = value.g; PluginConfig.Instance.LevelSelectionBorderColorB = value.b; _ = LevelSelectionBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("main-border-alpha")]
    public float MainBorderAlpha
    {
        get => PluginConfig.Instance.LevelSelectionBorderAlpha;
        set { PluginConfig.Instance.LevelSelectionBorderAlpha = value; _ = LevelSelectionBackground.Instance?.BuildOrReloadBsml(); }
    }

    // --- Leaderboard Selective ---
    [UIValue("selective-leaderboard")]
    public bool UseSelectiveLeaderboard
    {
        get => PluginConfig.Instance.UseSelectiveLeaderboard;
        set { PluginConfig.Instance.UseSelectiveLeaderboard = value; _ = LeaderboardBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("leaderboard-alpha")]
    public float LeaderboardAlpha
    {
        get => PluginConfig.Instance.LeaderboardBackgroundAlpha;
        set { PluginConfig.Instance.LeaderboardBackgroundAlpha = value; _ = LeaderboardBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("leaderboard-color")]
    public Color LeaderboardColor
    {
        get => new Color(PluginConfig.Instance.LeaderboardBgColorR, PluginConfig.Instance.LeaderboardBgColorG, PluginConfig.Instance.LeaderboardBgColorB);
        set { PluginConfig.Instance.LeaderboardBgColorR = value.r; PluginConfig.Instance.LeaderboardBgColorG = value.g; PluginConfig.Instance.LeaderboardBgColorB = value.b; _ = LeaderboardBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("leaderboard-enable-border")]
    public bool LeaderboardEnableBorder
    {
        get => PluginConfig.Instance.LeaderboardEnableBorder;
        set { PluginConfig.Instance.LeaderboardEnableBorder = value; _ = LeaderboardBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("leaderboard-border-color")]
    public Color LeaderboardBorderColor
    {
        get => new Color(PluginConfig.Instance.LeaderboardBorderColorR, PluginConfig.Instance.LeaderboardBorderColorG, PluginConfig.Instance.LeaderboardBorderColorB);
        set { PluginConfig.Instance.LeaderboardBorderColorR = value.r; PluginConfig.Instance.LeaderboardBorderColorG = value.g; PluginConfig.Instance.LeaderboardBorderColorB = value.b; _ = LeaderboardBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("leaderboard-border-alpha")]
    public float LeaderboardBorderAlpha
    {
        get => PluginConfig.Instance.LeaderboardBorderAlpha;
        set { PluginConfig.Instance.LeaderboardBorderAlpha = value; _ = LeaderboardBackground.Instance?.BuildOrReloadBsml(); }
    }

    // --- Mods Selective ---
    [UIValue("selective-mods")]
    public bool UseSelectiveMods
    {
        get => PluginConfig.Instance.UseSelectiveMods;
        set { PluginConfig.Instance.UseSelectiveMods = value; _ = ModsBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("mods-alpha")]
    public float ModsAlpha
    {
        get => PluginConfig.Instance.ModsBackgroundAlpha;
        set { PluginConfig.Instance.ModsBackgroundAlpha = value; _ = ModsBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("mods-color")]
    public Color ModsColor
    {
        get => new Color(PluginConfig.Instance.ModsBgColorR, PluginConfig.Instance.ModsBgColorG, PluginConfig.Instance.ModsBgColorB);
        set { PluginConfig.Instance.ModsBgColorR = value.r; PluginConfig.Instance.ModsBgColorG = value.g; PluginConfig.Instance.ModsBgColorB = value.b; _ = ModsBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("mods-enable-border")]
    public bool ModsEnableBorder
    {
        get => PluginConfig.Instance.ModsEnableBorder;
        set { PluginConfig.Instance.ModsEnableBorder = value; _ = ModsBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("mods-border-color")]
    public Color ModsBorderColor
    {
        get => new Color(PluginConfig.Instance.ModsBorderColorR, PluginConfig.Instance.ModsBorderColorG, PluginConfig.Instance.ModsBorderColorB);
        set { PluginConfig.Instance.ModsBorderColorR = value.r; PluginConfig.Instance.ModsBorderColorG = value.g; PluginConfig.Instance.ModsBorderColorB = value.b; _ = ModsBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("mods-border-alpha")]
    public float ModsBorderAlpha
    {
        get => PluginConfig.Instance.ModsBorderAlpha;
        set { PluginConfig.Instance.ModsBorderAlpha = value; _ = ModsBackground.Instance?.BuildOrReloadBsml(); }
    }

    // --- Beatleader News Selective ---
    [UIValue("selective-beatleader-news")]
    public bool UseSelectiveBeatleaderNews
    {
        get => PluginConfig.Instance.UseSelectiveBeatleaderNews;
        set { PluginConfig.Instance.UseSelectiveBeatleaderNews = value; _ = BeatleaderNewsBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("beatleader-news-alpha")]
    public float BeatleaderNewsAlpha
    {
        get => PluginConfig.Instance.BeatleaderNewsBackgroundAlpha;
        set { PluginConfig.Instance.BeatleaderNewsBackgroundAlpha = value; _ = BeatleaderNewsBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("beatleader-news-color")]
    public Color BeatleaderNewsColor
    {
        get => new Color(PluginConfig.Instance.BeatleaderNewsBgColorR, PluginConfig.Instance.BeatleaderNewsBgColorG, PluginConfig.Instance.BeatleaderNewsBgColorB);
        set { PluginConfig.Instance.BeatleaderNewsBgColorR = value.r; PluginConfig.Instance.BeatleaderNewsBgColorG = value.g; PluginConfig.Instance.BeatleaderNewsBgColorB = value.b; _ = BeatleaderNewsBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("beatleader-news-enable-border")]
    public bool BeatleaderNewsEnableBorder
    {
        get => PluginConfig.Instance.BeatleaderNewsEnableBorder;
        set { PluginConfig.Instance.BeatleaderNewsEnableBorder = value; _ = BeatleaderNewsBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("beatleader-news-border-color")]
    public Color BeatleaderNewsBorderColor
    {
        get => new Color(PluginConfig.Instance.BeatleaderNewsBorderColorR, PluginConfig.Instance.BeatleaderNewsBorderColorG, PluginConfig.Instance.BeatleaderNewsBorderColorB);
        set { PluginConfig.Instance.BeatleaderNewsBorderColorR = value.r; PluginConfig.Instance.BeatleaderNewsBorderColorG = value.g; PluginConfig.Instance.BeatleaderNewsBorderColorB = value.b; _ = BeatleaderNewsBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("beatleader-news-border-alpha")]
    public float BeatleaderNewsBorderAlpha
    {
        get => PluginConfig.Instance.BeatleaderNewsBorderAlpha;
        set { PluginConfig.Instance.BeatleaderNewsBorderAlpha = value; _ = BeatleaderNewsBackground.Instance?.BuildOrReloadBsml(); }
    }

    // --- BS+ Music Selective ---
    [UIValue("selective-bsplus-music")]
    public bool UseSelectiveBSPlusMusic
    {
        get => PluginConfig.Instance.UseSelectiveBSPlusMusic;
        set { PluginConfig.Instance.UseSelectiveBSPlusMusic = value; _ = BSPlusMusicBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("bsplus-music-alpha")]
    public float BSPlusMusicAlpha
    {
        get => PluginConfig.Instance.BSPlusMusicBackgroundAlpha;
        set { PluginConfig.Instance.BSPlusMusicBackgroundAlpha = value; _ = BSPlusMusicBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("bsplus-music-color")]
    public Color BSPlusMusicColor
    {
        get => new Color(PluginConfig.Instance.BSPlusMusicBgColorR, PluginConfig.Instance.BSPlusMusicBgColorG, PluginConfig.Instance.BSPlusMusicBgColorB);
        set { PluginConfig.Instance.BSPlusMusicBgColorR = value.r; PluginConfig.Instance.BSPlusMusicBgColorG = value.g; PluginConfig.Instance.BSPlusMusicBgColorB = value.b; _ = BSPlusMusicBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("bsplus-music-enable-border")]
    public bool BSPlusMusicEnableBorder
    {
        get => PluginConfig.Instance.BSPlusMusicEnableBorder;
        set { PluginConfig.Instance.BSPlusMusicEnableBorder = value; _ = BSPlusMusicBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("bsplus-music-border-color")]
    public Color BSPlusMusicBorderColor
    {
        get => new Color(PluginConfig.Instance.BSPlusMusicBorderColorR, PluginConfig.Instance.BSPlusMusicBorderColorG, PluginConfig.Instance.BSPlusMusicBorderColorB);
        set { PluginConfig.Instance.BSPlusMusicBorderColorR = value.r; PluginConfig.Instance.BSPlusMusicBorderColorG = value.g; PluginConfig.Instance.BSPlusMusicBorderColorB = value.b; _ = BSPlusMusicBackground.Instance?.BuildOrReloadBsml(); }
    }
    [UIValue("bsplus-music-border-alpha")]
    public float BSPlusMusicBorderAlpha
    {
        get => PluginConfig.Instance.BSPlusMusicBorderAlpha;
        set { PluginConfig.Instance.BSPlusMusicBorderAlpha = value; _ = BSPlusMusicBackground.Instance?.BuildOrReloadBsml(); }
    }

    [UIAction("BackButton")]
    void OnBackPressed() => _flowCoordinator?.DismissSelf();
}
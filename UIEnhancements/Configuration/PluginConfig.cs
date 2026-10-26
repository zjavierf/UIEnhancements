using System.Runtime.CompilerServices;
using IPA.Config.Stores;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace UIEnhancements.Configuration;

internal class PluginConfig
{
    public static PluginConfig Instance { get; set; } = null!;

    public virtual bool EnableSongListBackground { get; set; } = false;
    public virtual bool EnableGameplaySettingsBackground { get; set; } = true;
    public virtual bool EnableLevelSelectionBackground { get; set; } = true;
    public virtual bool EnableLeaderboardBackground { get; set; } = true;
    public virtual bool EnableModsBackground { get; set; } = true;
    public virtual bool EnableBeatleaderNewsBackground { get; set; } = true;
    public virtual bool EnableBSPlusMusicBackground { get; set; } = true;

    // Global Customization
    public virtual float BackgroundAlpha { get; set; } = 0.95f;
    public virtual float BgColorR { get; set; } = 0f;
    public virtual float BgColorG { get; set; } = 0f;
    public virtual float BgColorB { get; set; } = 0f;
    
    public virtual bool EnableBorder { get; set; } = true;
    public virtual float BorderAlpha { get; set; } = 1f;
    public virtual float BorderColorR { get; set; } = 0.65f;
    public virtual float BorderColorG { get; set; } = 0f;
    public virtual float BorderColorB { get; set; } = 0.95f;

    // --- Song List Selective ---
    public virtual bool UseSelectiveSongList { get; set; } = false;
    public virtual float SongListBackgroundAlpha { get; set; } = 0.95f;
    public virtual float SongListBgColorR { get; set; } = 0f;
    public virtual float SongListBgColorG { get; set; } = 0f;
    public virtual float SongListBgColorB { get; set; } = 0f;
    public virtual bool SongListEnableBorder { get; set; } = false;
    public virtual float SongListBorderAlpha { get; set; } = 1f;
    public virtual float SongListBorderColorR { get; set; } = 1f;
    public virtual float SongListBorderColorG { get; set; } = 0.5f;
    public virtual float SongListBorderColorB { get; set; } = 0f;

    // --- Gameplay Settings Selective ---
    public virtual bool UseSelectiveGameplaySettings { get; set; } = false;
    public virtual float GameplaySettingsBackgroundAlpha { get; set; } = 0.95f;
    public virtual float GameplaySettingsBgColorR { get; set; } = 0f;
    public virtual float GameplaySettingsBgColorG { get; set; } = 0f;
    public virtual float GameplaySettingsBgColorB { get; set; } = 0f;
    public virtual bool GameplaySettingsEnableBorder { get; set; } = false;
    public virtual float GameplaySettingsBorderAlpha { get; set; } = 1f;
    public virtual float GameplaySettingsBorderColorR { get; set; } = 1f;
    public virtual float GameplaySettingsBorderColorG { get; set; } = 0.5f;
    public virtual float GameplaySettingsBorderColorB { get; set; } = 0f;

    // --- Level Selection Selective ---
    public virtual bool UseSelectiveLevelSelection { get; set; } = false;
    public virtual float LevelSelectionBackgroundAlpha { get; set; } = 0.95f;
    public virtual float LevelSelectionBgColorR { get; set; } = 0f;
    public virtual float LevelSelectionBgColorG { get; set; } = 0f;
    public virtual float LevelSelectionBgColorB { get; set; } = 0f;
    public virtual bool LevelSelectionEnableBorder { get; set; } = false;
    public virtual float LevelSelectionBorderAlpha { get; set; } = 1f;
    public virtual float LevelSelectionBorderColorR { get; set; } = 1f;
    public virtual float LevelSelectionBorderColorG { get; set; } = 0.5f;
    public virtual float LevelSelectionBorderColorB { get; set; } = 0f;

    // --- Leaderboard Selective ---
    public virtual bool UseSelectiveLeaderboard { get; set; } = false;
    public virtual float LeaderboardBackgroundAlpha { get; set; } = 0.95f;
    public virtual float LeaderboardBgColorR { get; set; } = 0f;
    public virtual float LeaderboardBgColorG { get; set; } = 0f;
    public virtual float LeaderboardBgColorB { get; set; } = 0f;
    public virtual bool LeaderboardEnableBorder { get; set; } = false;
    public virtual float LeaderboardBorderAlpha { get; set; } = 1f;
    public virtual float LeaderboardBorderColorR { get; set; } = 1f;
    public virtual float LeaderboardBorderColorG { get; set; } = 0.5f;
    public virtual float LeaderboardBorderColorB { get; set; } = 0f;

    // --- Mods Selective ---
    public virtual bool UseSelectiveMods { get; set; } = false;
    public virtual float ModsBackgroundAlpha { get; set; } = 0.95f;
    public virtual float ModsBgColorR { get; set; } = 0f;
    public virtual float ModsBgColorG { get; set; } = 0f;
    public virtual float ModsBgColorB { get; set; } = 0f;
    public virtual bool ModsEnableBorder { get; set; } = false;
    public virtual float ModsBorderAlpha { get; set; } = 1f;
    public virtual float ModsBorderColorR { get; set; } = 1f;
    public virtual float ModsBorderColorG { get; set; } = 0.5f;
    public virtual float ModsBorderColorB { get; set; } = 0f;

    // --- Beatleader News Selective ---
    public virtual bool UseSelectiveBeatleaderNews { get; set; } = false;
    public virtual float BeatleaderNewsBackgroundAlpha { get; set; } = 0.95f;
    public virtual float BeatleaderNewsBgColorR { get; set; } = 0f;
    public virtual float BeatleaderNewsBgColorG { get; set; } = 0f;
    public virtual float BeatleaderNewsBgColorB { get; set; } = 0f;
    public virtual bool BeatleaderNewsEnableBorder { get; set; } = false;
    public virtual float BeatleaderNewsBorderAlpha { get; set; } = 1f;
    public virtual float BeatleaderNewsBorderColorR { get; set; } = 1f;
    public virtual float BeatleaderNewsBorderColorG { get; set; } = 0.5f;
    public virtual float BeatleaderNewsBorderColorB { get; set; } = 0f;

    // --- BS+ Music Selective ---
    public virtual bool UseSelectiveBSPlusMusic { get; set; } = false;
    public virtual float BSPlusMusicBackgroundAlpha { get; set; } = 0.95f;
    public virtual float BSPlusMusicBgColorR { get; set; } = 0f;
    public virtual float BSPlusMusicBgColorG { get; set; } = 0f;
    public virtual float BSPlusMusicBgColorB { get; set; } = 0f;
    public virtual bool BSPlusMusicEnableBorder { get; set; } = false;
    public virtual float BSPlusMusicBorderAlpha { get; set; } = 1f;
    public virtual float BSPlusMusicBorderColorR { get; set; } = 1f;
    public virtual float BSPlusMusicBorderColorG { get; set; } = 0.5f;
    public virtual float BSPlusMusicBorderColorB { get; set; } = 0f;

    public virtual void OnChanged()
    {
    }
}
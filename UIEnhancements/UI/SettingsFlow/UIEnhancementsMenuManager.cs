#nullable enable
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;
using JetBrains.Annotations;
using Zenject;

namespace UIEnhancements.UI.SettingsFlow;

[UsedImplicitly]
public class UIEnhancementsMenuManager : IInitializable, System.IDisposable
{
    [Inject] private readonly UIEnhancementsFlowCoordinator _flowCoordinator = null!;
    private MenuButton? _menuButton;

    public void Initialize()
    {
        _menuButton = new MenuButton("UI Enhancements", "Customize your UI", ShowFlowCoordinator);
        MenuButtons.Instance.RegisterButton(_menuButton);
    }

    public void Dispose()
    {
        if (_menuButton != null && MenuButtons.Instance != null)
        {
            MenuButtons.Instance.UnregisterButton(_menuButton);
        }
    }

    private void ShowFlowCoordinator()
    {
        if (_flowCoordinator == null || BeatSaberUI.MainFlowCoordinator == null) return;
        BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinator(_flowCoordinator);
    }
}
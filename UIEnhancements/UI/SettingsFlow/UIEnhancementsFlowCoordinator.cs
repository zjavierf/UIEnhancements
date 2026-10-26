using BeatSaberMarkupLanguage;
using HMUI;
using Zenject;

namespace UIEnhancements.UI.SettingsFlow;

public class UIEnhancementsFlowCoordinator : FlowCoordinator
{
    private UIEnhancementsSettingsViewController _settingsViewController = null!;

    [Inject]
    public void Construct(UIEnhancementsSettingsViewController settingsViewController)
    {
        _settingsViewController = settingsViewController;
    }

    protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
    {
        if (firstActivation)
        {
            showBackButton = false;
            ProvideInitialViewControllers(_settingsViewController);
        }
    }

    public void DismissSelf()
    {
        BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator(this);
    }

    protected override void BackButtonWasPressed(ViewController topViewController)
    {
        BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator(this);
    }
}
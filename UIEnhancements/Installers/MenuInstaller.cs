using JetBrains.Annotations;
using UIEnhancements.UI;
using UIEnhancements.UI.SettingsFlow;
using Zenject;

namespace UIEnhancements.Installers;

[UsedImplicitly]
internal class MenuInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<SongListBackground>().AsSingle();
        Container.BindInterfacesTo<GameplaySettingsBackground>().AsSingle();
        Container.BindInterfacesTo<LevelSelectionBackground>().AsSingle();
        Container.BindInterfacesTo<LeaderboardBackground>().AsSingle();
        Container.BindInterfacesTo<ModsBackground>().AsSingle();
        Container.BindInterfacesTo<BeatleaderNewsBackground>().AsSingle();
        Container.BindInterfacesTo<BSPlusMusicBackground>().AsSingle();
        
        Container.BindInterfacesAndSelfTo<UIEnhancementsMenuManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<UIEnhancementsSettingsViewController>().FromNewComponentAsViewController().AsSingle();
        Container.Bind<UIEnhancementsFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle();
    }
}
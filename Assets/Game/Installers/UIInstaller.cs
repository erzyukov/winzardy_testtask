namespace Game.Installers
{
	using Game.UI;
	using Zenject;


	public class UIInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			// UIModel
			Container
				.Bind<UIModel>()
				.AsSingle();

			// UINavigator
			Container
				.BindInterfacesTo<UINavigator>()
				.AsSingle();

			// MainMenu

			Container
				.BindInterfacesTo<UIMainMenuScreenView>()
				.FromComponentInHierarchy()
				.AsSingle();

			Container
				.BindInterfacesTo<UIMainMenuScreenPresenter>()
				.AsSingle();

			//CharactersSelect

			Container
				.BindInterfacesTo<UICharactersSelectScreenView>()
				.FromComponentInHierarchy()
				.AsSingle();

			Container
				.BindInterfacesTo<UICharactersSelectScreenPresenter>()
				.AsSingle();
		}
	}
}
namespace Game.Installers
{
	using Game.UI;
	using Zenject;


	public class UIInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Install_Core();
			Install_Screens();
			Install_Misc();
		}

		private void Install_Core()
		{
			// UIModel
			Container
				.Bind<UIModel>()
				.AsSingle();

			// UINavigator
			Container
				.BindInterfacesTo<UINavigator>()
				.AsSingle();
		}

		private void Install_Screens()
		{
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

		private void Install_Misc()
		{
			//UISelectCharacterAnimator
			Container
				.BindInterfacesTo<UISelectCharacterAnimator>()
				.FromComponentInHierarchy()
				.AsSingle();
		}
	}
}
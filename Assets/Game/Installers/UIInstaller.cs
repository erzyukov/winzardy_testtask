namespace Game.Installers
{
	using Game.UI;
	using Zenject;


	public class UIInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			// UIModel
			Container.Bind<UIModel>()
				.AsSingle();
		}
	}
}
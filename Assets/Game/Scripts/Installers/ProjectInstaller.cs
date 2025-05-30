namespace Game.Installers
{
	using Game.Core;
	using Zenject;


	public class ProjectInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<GameModel>()
				.AsSingle();
		}
	}
}
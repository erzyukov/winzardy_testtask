namespace Game.Installers
{
	using Game.Configs;
	using UnityEngine;
	using Zenject;


	public class ConfigInstaller : MonoInstaller
	{
		[SerializeField] RootConfig			_rootConfig;

		public override void InstallBindings()
		{
			var rc		= _rootConfig;

			// Gameplay
			Container.BindInstance( rc.CharactersConfig );
		}
	}
}
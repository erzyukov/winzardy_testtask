namespace Game.Configs
{
	using UnityEngine;

	[CreateAssetMenu( fileName = "RootConfig", menuName = "Configs/RootConfig", order = (int)EConfig.Root )]
	public class RootConfig : ScriptableObject
	{
		public CharactersConfig CharactersConfig;
	}
}

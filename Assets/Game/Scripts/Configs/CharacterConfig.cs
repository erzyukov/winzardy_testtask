namespace Game.Configs
{
	using Game.Utilities;
	using UnityEngine;

	[CreateAssetMenu( fileName = "CharacterConfig", menuName = "Configs/Characters/CharacterConfig", order = (int)EConfig.Character )]
	public class CharacterConfig : AutoIdConfig
	{
		public string		Name;
		public int			ExperienceToNextLevel;
		public int			CurrentExperience;
		public Sprite		Icon;
	}
}

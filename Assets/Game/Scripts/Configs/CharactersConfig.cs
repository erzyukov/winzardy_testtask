namespace Game.Configs
{
	using Game.UI;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;

	[CreateAssetMenu( fileName = "CharactersConfig", menuName = "Configs/Characters/CharactersConfig", order = (int)EConfig.Characters )]
	public class CharactersConfig : ScriptableObject
	{
		public UICharacterElement CharacterElementPrefab;
		
		public List<CharacterConfig> Characters;

		public CharacterConfig GetCharacter( uint id )
		{
			var characters			= Characters.AsEnumerable();
			CharacterConfig config	= characters.First( u => u.Id == id );

			if (config == null)
				throw new Exception( $"Config not found for character with ID = {id}." );

			return config;
		}
	}
}
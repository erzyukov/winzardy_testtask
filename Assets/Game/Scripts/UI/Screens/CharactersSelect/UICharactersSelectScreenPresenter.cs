namespace Game.UI
{
	using Game.Configs;
	using Game.Core;
	using Game.Utilities;
	using R3;
	using System.Collections.Generic;
	using UnityEngine;


	public class UICharactersSelectScreenPresenter : UiScreenPresenterBase<IUICharactersSelectScreenView>
	{
		protected override EScreen Screen => EScreen.CharacterSelect;

		private readonly IUISelectCharacterAnimator		_animator;
		private readonly CharactersConfig				_config;
		private readonly GameModel						_gameModel;

		private Dictionary<uint, IUICharacterElement>		_characters = new();

#region Constructor

		public UICharactersSelectScreenPresenter( 
			IUICharactersSelectScreenView view, 
			UIModel model, 
			IUINavigator navigator,
			IUISelectCharacterAnimator animator,
			CharactersConfig config,
			GameModel gameModel
		) 
			: base( view, model, navigator )
		{
			_animator	= animator;
			_config		= config;
			_gameModel	= gameModel;
		}

#endregion

#region UiScreenPresenterBase

		public override void Initialize()
		{
			FillCharacterList();
			_animator.ResetAnimation();

			base.Initialize();

			View.SelectButtonClicked
				.Subscribe( _ => OnSelectButtonClicked() )
				.AddTo( Disposables );

			_gameModel.SelectedCharacterId
				.Select( v => v != 0 )
				.Subscribe( OnChangedSelectedCharacter )
				.AddTo( Disposables );
		}

		protected override void OnOpened()
		{
			base.OnOpened();

			if (_gameModel.SelectedCharacterId.Value == 0)
			{
				_animator.StartOpenScreenInfoAnimation();
				_animator.StartOpenScreenListAnimation();
			}
			else
				RunCharacterSelectedAnimation();
		}

		protected override void OnClosed()
		{
			base.OnClosed();
			View.BaseButtons.ForEach( b => b.ResetAnimation() );
			_animator.ResetAnimation();
		}

#endregion

		private void OnSelectButtonClicked()
		{
			OpenScreen( EScreen.MainMenu );
		}

		private void FillCharacterList()
		{
			View.CharacterListParent.DestroyChildren();
			_config.Characters.ForEach( c =>
			{
				IUICharacterElement character = GameObject.Instantiate( _config.CharacterElementPrefab, View.CharacterListParent );
				character.SetIcon( c.Icon );
				character.SetExperience( (float) c.CurrentExperience / c.ExperienceToNextLevel );
				character.SelectButtonClicked
					.Subscribe( _ => OnSelectButtonClicked( c.Id ) )
					.AddTo( Disposables );
			
				_characters.Add( c.Id, character );
			} );
		}

		private void OnSelectButtonClicked( uint id )
		{
            foreach (var item in _characters)
				item.Value.SetSelected( item.Key == id );

			ShowSelectedCharacterInfo( id );
		}

		private void OnChangedSelectedCharacter( bool isSelected )
		{
			View.SetSelectButtonInteractive( isSelected );
			View.SetExperienceActive( isSelected );
		}

		private void ShowSelectedCharacterInfo( uint id )
		{
			if (_gameModel.SelectedCharacterId.Value == id)
				return;

			_gameModel.SelectedCharacterId.Value = id;

			var config = _config.GetCharacter( id );

			var experienceRatio = (float) config.CurrentExperience / config.ExperienceToNextLevel;
			_animator.RunChangeCharacterAnimation( experienceRatio, config.CurrentExperience, () =>
			{
				View.SetName( config.Name );
				View.SetIcon( config.Icon );
				View.SetExperienceMax( config.ExperienceToNextLevel );
			} );
		}

		private void RunCharacterSelectedAnimation()
		{
			var config = _config.GetCharacter( _gameModel.SelectedCharacterId.Value );

			var experienceRatio = (float) config.CurrentExperience / config.ExperienceToNextLevel;
			_animator.RunCharacterSelectedAnimation( experienceRatio, config.CurrentExperience, true );

			_animator.StartOpenScreenListAnimation();
		}
	}
}

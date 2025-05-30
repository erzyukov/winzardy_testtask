namespace Game.UI
{
	using Game.Configs;
	using Game.Utilities;
	using R3;
	using System.Collections.Generic;
	using UnityEngine;


	public class UICharactersSelectScreenPresenter : UiScreenPresenterBase<IUICharactersSelectScreenView>
	{
		protected override EScreen Screen => EScreen.CharacterSelect;

		private readonly IUISelectCharacterAnimator		_animator;
		private readonly CharactersConfig				_config;

		private Dictionary<uint, IUICharacterElement>		_characters = new();

#region Constructor

		public UICharactersSelectScreenPresenter( 
			IUICharactersSelectScreenView view, 
			UIModel model, 
			IUINavigator navigator,
			IUISelectCharacterAnimator animator,
			CharactersConfig config
		) 
			: base( view, model, navigator )
		{
			_animator	= animator;
			_config		= config;
		}

#endregion

		public override void Initialize()
		{
			FillCharacterList();
			_animator.ResetAnimation();

			base.Initialize();

			View.SelectButtonClicked
				.Subscribe( _ => OnSelectButtonClicked() )
				.AddTo( Disposables );
		}

		protected override void OnOpened()
		{
			base.OnOpened();
			_animator.StartOpenScreenAnimation( null );
		}

		protected override void OnClosed()
		{
			base.OnClosed();
			View.BaseButtons.ForEach( b => b.ResetAnimation() );
			_animator.ResetAnimation();
		}

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

		private void ShowSelectedCharacterInfo( uint id )
		{
			
		}
	}
}

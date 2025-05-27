namespace Game.UI
{
	using R3;
	using UnityEngine.UIElements;


	public class UIMainMenuScreenPresenter : UiScreenPresenterBase<IUIMainMenuScreenView>
	{
		public UIMainMenuScreenPresenter( IUIMainMenuScreenView view, UIModel model, IUINavigator navigator ) 
			: base( view, model, navigator ) { }

		protected override EScreen Screen => EScreen.MainMenu;

		public override void Initialize()
		{
			base.Initialize();

			View.SelectCharacterButtonClicked
				.Subscribe( _ => OnSelectCharacterButtonClicked() )
				.AddTo( Disposables );
		}

		private void OnSelectCharacterButtonClicked()
		{
			OpenScreen( EScreen.CharacterSelect );
		}
	}
}

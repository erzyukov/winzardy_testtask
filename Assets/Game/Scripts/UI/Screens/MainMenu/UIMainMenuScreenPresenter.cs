namespace Game.UI
{
	using R3;


	public class UIMainMenuScreenPresenter : UiScreenPresenterBase<IUIMainMenuScreenView>
	{
		protected override EScreen Screen => EScreen.MainMenu;

		public UIMainMenuScreenPresenter( IUIMainMenuScreenView view, UIModel model, IUINavigator navigator ) 
			: base( view, model, navigator ) { }

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

		protected override void OnClosed()
		{
			base.OnClosed();
			View.BaseButtons.ForEach( b => b.ResetAnimation() );
		}
	}
}

namespace Game.UI
{
	public class UICharactersSelectScreenPresenter : UiScreenPresenterBase<IUICharactersSelectScreenView>
	{
		public UICharactersSelectScreenPresenter( IUICharactersSelectScreenView view, UIModel model, IUINavigator navigator ) 
			: base( view, model, navigator ) { }

		protected override EScreen Screen => EScreen.CharacterSelect;
	}
}

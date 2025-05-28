namespace Game.UI
{
	using R3;


	public class UICharactersSelectScreenPresenter : UiScreenPresenterBase<IUICharactersSelectScreenView>
	{
		protected override EScreen Screen => EScreen.CharacterSelect;

		private readonly IUISelectCharacterAnimator _animator;

#region Constructor

		public UICharactersSelectScreenPresenter( 
			IUICharactersSelectScreenView view, 
			UIModel model, 
			IUINavigator navigator,
			IUISelectCharacterAnimator animator
		) 
			: base( view, model, navigator )
		{
			_animator = animator;
		}

#endregion

		public override void Initialize()
		{
			base.Initialize();

			View.SelectButtonClicked
				.Subscribe( _ => OnSelectButtonClicked() )
				.AddTo( Disposables );

			_animator.ResetAnimation();
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
	}
}

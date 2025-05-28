namespace Game.UI
{
	using R3;
	using System;
	using Zenject;


	public abstract class UiScreenPresenterBase<TView> : IInitializable, IDisposable
		where TView : IUiScreenViewBase
	{
		protected readonly TView		View;
		
		private readonly UIModel		_model;
		private readonly IUINavigator	_navigator;

		public UiScreenPresenterBase( TView view, UIModel model, IUINavigator navigator )
		{
			View		= view;
			_model		= model;
			_navigator	= navigator;
		}

		protected abstract EScreen		Screen			{ get; }

		protected CompositeDisposable	Disposables		= new();

		public virtual void Initialize()
		{
			_model.Screen
				.Select( s => s.Equals( Screen ) )
				.Subscribe( OnScreenActiveChange )
				.AddTo( Disposables );
		}

		private void OnScreenActiveChange( bool isActive )
		{
			View.SetActive( isActive );

			if (isActive) 
				OnOpened();
			else
				OnClosed();
		}

		public virtual void Dispose() => Disposables.Dispose();

		protected void OpenScreen( EScreen screen ) => _navigator.Open( screen );

		protected virtual void OnOpened() { }

		protected virtual void OnClosed() { }
	}
}

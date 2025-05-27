namespace Game.UI
{
	using R3;
	using System;
	using Zenject;


	public abstract class UiScreenPresenterBase : IInitializable, IDisposable
	{
		[Inject] protected IUiScreenViewBase		View;
		
		[Inject] private UIModel					_model;

		protected abstract EScreen		Screen			{ get; }

		protected CompositeDisposable	Disposables		= new();

		public virtual void Initialize()
		{
			_model.Screen
				.Select( s => s.Equals( Screen ) )
				.Subscribe( v => View.SetActive( v ) )
				.AddTo( Disposables );
		}

		public virtual void Dispose() => Disposables.Dispose();
	}
}

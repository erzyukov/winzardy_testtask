namespace Game.UI
{
	public interface IUINavigator
	{
		void Open( EScreen screen );
	}

	public class UINavigator : IUINavigator
	{
		private readonly UIModel		_model;

		public UINavigator( UIModel model )
		{
			_model = model;
		}

		public void Open( EScreen screen )
		{
			_model.Screen.Value = screen;
		}
	}
}

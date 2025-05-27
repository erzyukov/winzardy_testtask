namespace Game.UI
{
	using R3;

	public class UIModel
	{
		public ReactiveProperty<EScreen> Screen = new( EScreen.MainMenu );
	}
}

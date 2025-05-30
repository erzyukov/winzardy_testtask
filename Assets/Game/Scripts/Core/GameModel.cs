namespace Game.Core
{
	using R3;

	public class GameModel
	{
		public ReactiveProperty<uint> SelectedCharacterId { get; } = new();
	}
}

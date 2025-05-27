namespace Game.UI
{
	using R3;
	using UnityEngine;
	using UnityEngine.UI;

	public interface IUIMainMenuScreenView : IUiScreenViewBase
	{
		Observable<Unit> SelectCharacterButtonClicked { get; }
	}

	public class UIMainMenuScreenView : UiScreenViewBase, IUIMainMenuScreenView
	{
		[SerializeField] private Button _selectCharacterButton;

		public Observable<Unit> SelectCharacterButtonClicked => _selectCharacterButton.OnClickAsObservable();
	}
}

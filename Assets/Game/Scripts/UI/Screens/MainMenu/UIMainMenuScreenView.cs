namespace Game.UI
{
	using R3;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;

	public interface IUIMainMenuScreenView : IUiScreenViewBase
	{
		Observable<Unit> SelectCharacterButtonClicked { get; }
		List<BaseButtonAnimation> BaseButtons { get; }
	}

	public class UIMainMenuScreenView : UiScreenViewBase, IUIMainMenuScreenView
	{
		[SerializeField] private Button _selectCharacterButton;
		
		[SerializeField] private List<BaseButtonAnimation> _baseButtons;

		public Observable<Unit> SelectCharacterButtonClicked	=> _selectCharacterButton.OnClickAsObservable();
		public List<BaseButtonAnimation> BaseButtons			=> _baseButtons;
	}
}

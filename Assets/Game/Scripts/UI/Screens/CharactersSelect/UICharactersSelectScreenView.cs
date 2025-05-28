namespace Game.UI
{
	using R3;
	using System.Collections.Generic;
	using TMPro;
	using UnityEngine;
	using UnityEngine.UI;

	public interface IUICharactersSelectScreenView : IUiScreenViewBase
	{
		Observable<Unit> SelectButtonClicked { get; }
		List<BaseButtonAnimation> BaseButtons { get; }
	}

	public class UICharactersSelectScreenView : UiScreenViewBase, IUICharactersSelectScreenView
	{
		[SerializeField] private TextMeshProUGUI	_name;
		[SerializeField] private TextMeshProUGUI	_maxExperience;
		[SerializeField] private TextMeshProUGUI	_curExperience;
		[SerializeField] private Image				_icon;
		[SerializeField] private Button				_selectButton;
		[SerializeField] private Transform			_characterListParent;
		
		[SerializeField] private List<BaseButtonAnimation>		_baseButtons;

		public Observable<Unit> SelectButtonClicked => _selectButton.OnClickAsObservable();
		public List<BaseButtonAnimation> BaseButtons => _baseButtons;
	}
}

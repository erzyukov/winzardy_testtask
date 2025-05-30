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

		Transform CharacterListParent { get; }

		void SetSelectButtonInteractive( bool value );
		void SetName( string value );
		void SetIcon( Sprite value );
		void SetExperienceMax( int value );
		void SetExperienceActive( bool value );
	}

	public class UICharactersSelectScreenView : UiScreenViewBase, IUICharactersSelectScreenView
	{
		[SerializeField] private TextMeshProUGUI	_name;
		[SerializeField] private TextMeshProUGUI	_maxExperience;
		[SerializeField] private TextMeshProUGUI	_curExperience;
		[SerializeField] private Image				_icon;
		[SerializeField] private Button				_selectButton;
		[SerializeField] private Transform			_characterListParent;
		[SerializeField] private GameObject			_experienceBlock;
		
		[SerializeField] private List<BaseButtonAnimation>		_baseButtons;

		public Observable<Unit> SelectButtonClicked		=> _selectButton.OnClickAsObservable();
		public List<BaseButtonAnimation> BaseButtons	=> _baseButtons;
		public Transform CharacterListParent			=> _characterListParent;

		public void SetSelectButtonInteractive( bool value )
		=> 
			_selectButton.interactable = value;

		public void SetName( string value )		=> _name.text = value;

		public void SetIcon( Sprite value )		=> _icon.sprite = value;

		public void SetExperienceActive( bool value )	=> _experienceBlock.SetActive( value );
		public void SetExperienceMax( int value )		=> _maxExperience.text = value.ToString();
	}
}

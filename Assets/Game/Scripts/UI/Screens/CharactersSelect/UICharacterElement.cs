namespace Game.UI
{
	using PrimeTween;
	using R3;
	using UnityEngine;
	using UnityEngine.UI;


	public interface IUICharacterElement
	{
		Observable<Unit> SelectButtonClicked { get; }
		void SetIcon( Sprite value );
		void SetExperience( float value );
		void SetSelected( bool value );
	}

	public class UICharacterElement : MonoBehaviour, IUICharacterElement
	{
		[SerializeField] private Image		_icon;
		[SerializeField] private Slider		_experience;
		[SerializeField] private Button		_selectButton;
		[SerializeField] private Image		_selection;

		[Header("Parameters")]
		[SerializeField] private Color		_selectedBorderColor;
		
		private Color		_defaultBorderColor;
		private bool		_selected;

		private void Awake()
		{
			_defaultBorderColor = _selection.color;
		}

		public Observable<Unit> SelectButtonClicked		=> _selectButton.OnClickAsObservable();

		public void SetIcon( Sprite value )			=> _icon.sprite = value;

		public void SetExperience( float value )	=> _experience.value = value;

		public void SetSelected( bool value )
		{
			if (value && !_selected)
				Tween.Color( _selection, _selectedBorderColor, 0.3f, Ease.OutSine );
			else if (!value && _selected)
				Tween.Color( _selection, _defaultBorderColor, 0.3f, Ease.InSine );
			
			_selected = value;
		}
	}
}

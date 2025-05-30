namespace Game.UI
{
	using Game.Utilities;
	using PrimeTween;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using TMPro;
	using UnityEngine;
	using UnityEngine.UI;


	public interface IUISelectCharacterAnimator
	{
		void StartOpenScreenInfoAnimation();
		void StartOpenScreenListAnimation();
		void RunChangeCharacterAnimation( float experienceRatio, int currentExperience, Action onFade );
		void RunCharacterSelectedAnimation( float experienceRatio, int currentExperience, bool clearExperienceSlider = false );
		void ResetAnimation();
	}

	public class UISelectCharacterAnimator : MonoBehaviour, IUISelectCharacterAnimator
	{
		[SerializeField] private RectTransform		_title;
		[SerializeField] private Image				_icon;
		[SerializeField] private Transform			_iconDummyText;
		[SerializeField] private RectTransform		_selectButton;
		[SerializeField] private Transform			_charactersParent;
		[SerializeField] private Slider				_experienceSlider;
		[SerializeField] private TextMeshProUGUI	_experienceText;

		[Header("Parameters")]
		[SerializeField] private float _charactersStartAnimationDelay;
		[SerializeField] private float _charactersStartAnimationDuration;
		[SerializeField] private float _emptyIconAlpha;
		[SerializeField] private float _emptyIconAlphaAnimationDuration;
		[SerializeField] private float _titleStartAnimationDuration;
		[SerializeField] private float _dummyTextStartAnimationDuration;
		[SerializeField] private float _selectButtonStartAnimationDuration;
		[SerializeField] private float _characterFadeAnimationDuration;
		[SerializeField] private float _characterSelectAnimationDuration;
		[SerializeField] private float _characterExperienceSelectAnimationDuration;


		private List<Transform> _characterTransforms = new();

		private float	_defaultTitlePosition;
		private float	_defaultSelectButtonPosition;
		private bool	_isReadyForAnimation;

#region IUISelectCharacterAnimator

		public void StartOpenScreenInfoAnimation()
		{
			_isReadyForAnimation	= false;
			var sequence			= Sequence.Create();

			AddStartAlphaAnimation( sequence );
			AddStartTitleAnimation( sequence );
			AddStartDummyTextAnimation( sequence );
		}

		public void StartOpenScreenListAnimation()
		{
			_isReadyForAnimation	= false;
			var sequence			= Sequence.Create();

			AddStartSelectButtonAnimation( sequence );
			AddStartCharactersAnimation( sequence );
		}

		public void RunChangeCharacterAnimation( float experienceRatio, int currentExperience, Action onFade )
		{
			var sequence			= Sequence.Create();

			AddCharacterFadeAnimation( sequence ); 

			sequence.ChainCallback( onFade );

			AddCharacterSelectedAnimation( sequence, experienceRatio, currentExperience );
		}

		public void RunCharacterSelectedAnimation( float experienceRatio, int currentExperience, bool clearExperienceSlider = false )
		{
			var sequence			= Sequence.Create();
			AddCharacterSelectedAnimation( sequence, experienceRatio, currentExperience, clearExperienceSlider );
		}

		public void ResetAnimation()
		{
			PrepareAnimation();
		}

#endregion

		private void PrepareAnimation()
		{
			if (_isReadyForAnimation)
				return;

			_isReadyForAnimation		= true;
			_icon.color					= new Color( 1, 1, 1, 0 );
			_defaultTitlePosition		= _title.anchoredPosition.y;
			_title.anchoredPosition		= _title.anchoredPosition.WithY( 200 );
			
			_defaultSelectButtonPosition	= _selectButton.anchoredPosition.y;
			_selectButton.anchoredPosition	= _selectButton.anchoredPosition.WithY( -200 );

			_iconDummyText.localRotation	= Quaternion.Euler( 0, 0, 40 );

			_characterTransforms.Clear();
			foreach(Transform child in _charactersParent)
			{
				_characterTransforms.Add( child );
				child.localScale = Vector3.zero;
			}
		}

#region Animations

		private void AddStartAlphaAnimation( Sequence sequence )
		{
			sequence.Group( 
				Tween.Alpha( 
					_icon, 
					_emptyIconAlpha, 
					_emptyIconAlphaAnimationDuration, 
					Ease.InOutSine 
				)
			);
		}

		private void AddStartTitleAnimation( Sequence sequence )
		{
			sequence.Group( 
				Tween.UIAnchoredPositionY( 
					_title, 
					_defaultTitlePosition, 
					_titleStartAnimationDuration, 
					Ease.OutBounce 
				) 
			);
		}

		private void AddStartDummyTextAnimation( Sequence sequence )
		{
			if (!_iconDummyText.gameObject.activeSelf)
				return;

			sequence.Group( 
				Tween.LocalRotation( 
					_iconDummyText, 
					Quaternion.identity, 
					_dummyTextStartAnimationDuration, 
					Ease.OutElastic
				)
			);
		}

		private void AddStartSelectButtonAnimation( Sequence sequence )
		{
			sequence.Group( 
				Tween.UIAnchoredPositionY( 
					_selectButton, 
					_defaultSelectButtonPosition, 
					_selectButtonStartAnimationDuration, 
					Ease.OutSine
				)
			);
		}

		private void AddStartCharactersAnimation( Sequence sequence )
		{
			var insertDelay			= 0f;

			_characterTransforms.Where( b => b.localScale != Vector3.one ).ToList().ForEach( b =>
			{
				sequence.Insert( 
					insertDelay, 
					Tween.Scale( b, Vector3.one, _charactersStartAnimationDuration, Ease.OutElastic ) 
				);
				insertDelay += _charactersStartAnimationDelay;
			} );
		}

		private void AddCharacterFadeAnimation( Sequence sequence )
		{
			sequence.Group( 
				Tween.UIAnchoredPositionY( 
					_title, 
					200, 
					_characterFadeAnimationDuration, 
					Ease.InSine
				) 
			);
			sequence.Group( 
				Tween.Alpha( 
					_icon, 
					0, 
					_characterFadeAnimationDuration, 
					Ease.InSine 
				)
			);

			if (_experienceSlider.value != 0)
			{
				sequence.Group(
					Tween.UISliderValue(
						_experienceSlider,
						0,
						_characterFadeAnimationDuration,
						Ease.InSine
					)
				);
				sequence.Group(
					Tween.Custom(
						int.Parse( _experienceText.text ),
						0,
						_characterFadeAnimationDuration,
						ease: Ease.InSine,
						onValueChange: v => _experienceText.text = ((int)v).ToString()
					)
				);
			}
		}

		private void AddCharacterSelectedAnimation( Sequence sequence, float experienceRatio, int currentExperience, bool clearExperienceSlider = false )
		{
			_iconDummyText.gameObject.SetActive( false );

			if (clearExperienceSlider)
				_experienceSlider.value = 0;

			sequence.Chain( 
				Tween.UIAnchoredPositionY( 
					_title, 
					_defaultTitlePosition, 
					_characterSelectAnimationDuration, 
					Ease.OutBounce 
				) 
			);
			sequence.Group( 
				Tween.Alpha( 
					_icon, 
					1, 
					_characterSelectAnimationDuration, 
					Ease.OutSine 
				)
			);

			if (experienceRatio != 0)
			{
				sequence.Group( 
					Tween.UISliderValue( 
						_experienceSlider, 
						experienceRatio, 
						_characterExperienceSelectAnimationDuration, 
						Ease.OutCubic 
					)
				);
				sequence.Group(
					Tween.Custom(
						0,
						currentExperience,
						_characterExperienceSelectAnimationDuration,
						ease: Ease.OutSine,
						onValueChange: v => _experienceText.text = ((int)v).ToString()
					)
				);
			}
		}

#endregion
	}
}

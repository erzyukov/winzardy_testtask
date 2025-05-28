namespace Game.UI
{
	using Game.Utilities;
	using PrimeTween;
	using System;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;


	public interface IUISelectCharacterAnimator
	{
		void StartOpenScreenAnimation( Action onComplete );
		void ResetAnimation();
	}

	public class UISelectCharacterAnimator : MonoBehaviour, IUISelectCharacterAnimator
	{
		[SerializeField] private RectTransform		_title;
		[SerializeField] private Image				_icon;
		[SerializeField] private Transform			_iconDummyText;
		[SerializeField] private RectTransform		_selectButton;
		[SerializeField] private Transform			_charactersParent;

		[Header("Parameters")]
		[SerializeField] private float _charactersStartAnimationDelay;
		[SerializeField] private float _charactersStartAnimationDuration;
		[SerializeField] private float _emptyIconAlpha;
		[SerializeField] private float _emptyIconAlphaAnimationDuration;
		[SerializeField] private float _titleStartAnimationDuration;
		[SerializeField] private float _dummyTextStartAnimationDuration;
		[SerializeField] private float _selectButtonStartAnimationDuration;


		private List<Transform> _characterTransforms = new();

		private float	_defaultTitlePosition;
		private float	_defaultSelectButtonPosition;
		private bool	_isReadyForAnimation;

#region IUISelectCharacterAnimator

		public void StartOpenScreenAnimation( Action onComplete )
		{
			_isReadyForAnimation	= false;

			var sequence			= Sequence.Create();

			AddStartAlphaAnimation( sequence );
			AddStartTitleAnimation( sequence );
			AddStartDummyTextAnimation( sequence );
			AddStartSelectButtonAnimation( sequence );
			AddStartCharactersAnimation( sequence );

			sequence.OnComplete( () => onComplete?.Invoke() );
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

			_characterTransforms.ForEach( b =>
			{
				sequence.Insert( 
					insertDelay, 
					Tween.Scale( b, Vector3.one, _charactersStartAnimationDuration, Ease.OutElastic ) 
				);
				insertDelay += _charactersStartAnimationDelay;
			} );
		}

#endregion
	}
}

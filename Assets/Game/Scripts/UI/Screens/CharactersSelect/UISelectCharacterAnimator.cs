namespace Game.UI
{
	using Game.Utilities;
	using PrimeTween;
	using System;
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

		private float	_defaultTitlePosition;
		private float	_defaultSelectButtonPosition;
		private bool	_isReadyForAnimation;

#region IUISelectCharacterAnimator

		public void StartOpenScreenAnimation( Action onComplete )
		{
			_isReadyForAnimation = false;

			Sequence.Create()
				.Group( Tween.Alpha( _icon, 0.2f, 0.8f, Ease.OutSine ) )
				.Group( Tween.UIAnchoredPositionY( _title, _defaultTitlePosition, 1f, Ease.OutBounce ) )
				.Group( Tween.LocalRotation( _iconDummyText, Quaternion.identity, 1f, Ease.OutElastic ) )
				.Group( Tween.UIAnchoredPositionY( _selectButton, _defaultSelectButtonPosition, 0.5f, Ease.OutSine ) )
				.OnComplete( () => onComplete?.Invoke() );
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
		}
	}
}

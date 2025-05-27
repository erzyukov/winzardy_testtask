namespace Game.UI
{
	using PrimeTween;
	using UnityEngine;
	using UnityEngine.EventSystems;


	public class BaseButtonAnimation : MonoBehaviour, 
		IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
	{
		[SerializeField] private Transform		_iconTransform;
		[SerializeField] private float			_buttonOverScale;
		[SerializeField] private float			_buttonOverDuration;
		[SerializeField] private float			_buttonDownScale;
		[SerializeField] private float			_iconDeviationAngle;

		private float		_halfButtonOverDuration;
		
		private Sequence	_overSequence;
		private Sequence	_downSequence;

		private bool		_isPointerOver;
		private bool		_isPointerDown;
		private bool		_isOverSequenceComplete = true;

		private void Awake()
		{
			_halfButtonOverDuration = _buttonOverDuration * 0.5f;
		}

		public void OnPointerEnter( PointerEventData eventData )
		{
			_isPointerOver = true;

			if (_isPointerDown)
			{
				OnPointerDown( null );
				return;
			}

			TweenPointerEnter();
		}

		public void OnPointerExit( PointerEventData eventData )
		{
			_isPointerOver = false;

			TweenPointerExit();

			if (_isPointerDown )
				_downSequence.Stop();
		}

		public void OnPointerDown( PointerEventData eventData )
		{
			_isPointerDown = true;

			TweenPointerDown();
		}

		public void OnPointerUp( PointerEventData eventData )
		{
			// Don't need animation if pointer not over button or over tween complete
			if ((!_isPointerOver || _isOverSequenceComplete) && !_isPointerDown)
				return;

			_isPointerDown = false;

			if (_isPointerOver)
				TweenPointerUp();
		}

#region Tweens

		private void TweenPointerEnter()
		{
			_isOverSequenceComplete = false;

			_overSequence.Stop();
			_overSequence = Sequence.Create()
				.Group( Tween.Scale( transform, _buttonOverScale, _buttonOverDuration, Ease.OutSine ) )
				.Group( Tween.LocalRotation( _iconTransform, Quaternion.Euler(0, 0, -_iconDeviationAngle), 
					_buttonOverDuration, Ease.OutSine ) )
				.OnComplete( () => _isOverSequenceComplete = true );
		}

		private void TweenPointerExit()
		{
			_isOverSequenceComplete = false;

			_overSequence.Stop();
			_overSequence = Sequence.Create()
				.Group( Tween.Scale( transform, 1f, _halfButtonOverDuration, Ease.OutSine ) )
				.Group( Tween.LocalRotation( _iconTransform, Quaternion.identity, 
					_buttonOverDuration, Ease.OutBounce ) )
				.OnComplete( () => _isOverSequenceComplete = true );
		}

		private void TweenPointerDown()
		{
			_overSequence.Stop();
			_downSequence = Sequence.Create()
				.Group( Tween.Scale( transform, _buttonDownScale, _buttonOverDuration, Ease.OutSine ) )
				.Group( Tween.LocalRotation( _iconTransform, Quaternion.Euler(0, 0, _iconDeviationAngle), 
					_buttonOverDuration, Ease.OutSine ) );
		}

		private void TweenPointerUp()
		{
			_downSequence.Stop();
			_downSequence = Sequence.Create()
				.Group( Tween.Scale( transform, _buttonOverScale, _halfButtonOverDuration, Ease.OutSine ) )
				.Group( Tween.LocalRotation( _iconTransform, Quaternion.Euler(0, 0, -_iconDeviationAngle), 
					_halfButtonOverDuration, Ease.OutSine ) );
		}

#endregion
	}
}

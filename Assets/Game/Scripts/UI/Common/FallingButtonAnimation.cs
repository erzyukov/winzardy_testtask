namespace Game.UI
{
	using Game.Utilities;
	using PrimeTween;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;


	public class FallingButtonAnimation : MonoBehaviour
	{
		[SerializeField] private List<RectTransform>	_buttonTransforms;

		[SerializeField] private float		_buttonHiddenPosition;
		[SerializeField] private float		_startAnimationDelay;
		[SerializeField] private float		_buttonAnimationDelay;
		[SerializeField] private float		_buttonAnimationDuration;
		[SerializeField] private Ease		_defaultEase;
		[SerializeField] private Ease		_lastElementEase;

		private List<float>		_targetButtonPositions = new();
		private List<Button>	_buttons = new();

		private void Awake()
		{
			_buttonTransforms.ForEach( b =>
			{
				_targetButtonPositions.Add( b.anchoredPosition.y );
				b.anchoredPosition = b.anchoredPosition.WithY( _buttonHiddenPosition );

				var button = b.GetComponent<Button>();
				_buttons.Add( button );
				button.interactable = false;
			} );
		}

		private void Start()
		{
			var sequence = Sequence.Create();
			sequence.ChainDelay( _startAnimationDelay );

			var insertDelay		= 0f;
			var ease			= Ease.OutCubic;

			_buttonTransforms.ForEach( (i, b) =>
			{
				if (i == _buttons.Count - 1)
					ease = Ease.OutBounce;

				sequence.Insert( 
					insertDelay, 
					Tween.UIAnchoredPositionY( b, _targetButtonPositions[ i ], _buttonAnimationDuration, ease ) 
				);
				
				insertDelay += _buttonAnimationDelay;
			} );

			sequence.OnComplete( () => _buttons.ForEach( b => b.interactable = true ) );
		}
	}
}

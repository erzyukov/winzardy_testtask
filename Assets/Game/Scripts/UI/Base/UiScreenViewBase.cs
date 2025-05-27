namespace Game.UI
{
	using UnityEngine;


	public interface IUiScreenViewBase
	{
		void SetActive( bool value );
	}

	public abstract class UiScreenViewBase : MonoBehaviour, IUiScreenViewBase
	{
		public void SetActive( bool value ) => gameObject.SetActive( value );
	}
}

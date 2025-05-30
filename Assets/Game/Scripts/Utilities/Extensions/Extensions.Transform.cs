namespace Game.Utilities
{
	using UnityEngine;


	public static class Extensions_Transform
	{
		public static void DestroyChildren( this Transform transform )
		{
			for (var i = transform.childCount - 1; i >= 0; i--)
			{
				var child			= transform.GetChild( 0 );
				
				child.SetParent( null );
				GameObject.Destroy( child.gameObject );
			}
		}
	}
}

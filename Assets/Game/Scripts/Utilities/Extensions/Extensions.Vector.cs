namespace Game.Utilities
{
	using UnityEngine;


	public static class Extensions_Vector
	{
		public static Vector2 WithX( this Vector2 v, float x )			{ v.x = x; return v; }
		public static Vector2 WithY( this Vector2 v, float y )			{ v.y = y; return v; }	
	}
}

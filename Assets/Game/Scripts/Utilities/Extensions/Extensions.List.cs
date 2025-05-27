namespace Game.Utilities
{
	using System.Collections.Generic;
	using System;


	public static class Extensions_List
	{
		public static void ForEach<T>(this IEnumerable<T> sequence, Action<int, T> action)
		{
			int i = 0;
			foreach (T item in sequence)
			{
				action(i, item);
				i++;
			}
		}
	}
}

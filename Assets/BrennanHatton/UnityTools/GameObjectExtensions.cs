using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.UnityTools
{

	public static class GameObjectExtensions {
		
		// <summary>
		/// 
		/// </summary>
		public static void SetActive(this GameObject[] source, bool active) {
			for(int i = 0;i < source.Length; i++)
			{
				source[i].SetActive(active);
			}
		}
	}
}

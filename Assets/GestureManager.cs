using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Logging
{

	public class GestureManager : MonoBehaviour
	{
		public GestureLogger left, right;
		
	   
		public static GestureManager Instance { get; private set; }
		private void Awake() 
		{ 
			// If there is an instance, and it's not me, delete myself.
    
			if (Instance != null && Instance != this) 
			{ 
				Destroy(this); 
			} 
			else 
			{ 
				Instance = this; 
			} 
		}
		
		public ThumbState GetState()
		{
			if(left.state == right.state)
				return left.state;
				
			if(left.state == ThumbState.Side)
				return right.state;
				
			if(right.state == ThumbState.Side)
				return left.state;
				
			return ThumbState.Side;
		}
	}

}
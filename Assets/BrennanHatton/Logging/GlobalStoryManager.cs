using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Logging
{

	public class GlobalStoryManager : MonoBehaviour
	{
		public StoryMaker storyMaker;
		
		void Reset()
		{
			storyMaker = this.GetComponent<StoryMaker>();
		}
		
		//Singlton
		public static GlobalStoryManager Instance { get; private set; }
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
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

namespace BrennanHatton.Logging
{

	public class SetupBNGRig : MonoBehaviour
	{
		
		public GrabberLogger[] grabberLoggers;
		public PlayerMovementLogger movementLogger;
		
		void Reset()
		{
			Grabber[] grabbers = GameObject.FindObjectsOfType<Grabber>();
			grabberLoggers = new GrabberLogger[grabbers.Length];
			for(int i = 0 ;i <grabbers.Length; i++)
			{
				grabberLoggers[i] = grabbers[i].gameObject.GetComponent<GrabberLogger>(); 
				
				if(grabberLoggers[i] == null)
					grabberLoggers[i] = grabbers[i].gameObject.AddComponent<GrabberLogger>(); 
			}
			
			BNGPlayerController player = GameObject.FindObjectOfType<BNGPlayerController>();
			
			if(player != null)
			{
				movementLogger = player.gameObject.GetComponent<PlayerMovementLogger>(); 
				
				if(movementLogger == null)
					movementLogger = player.gameObject.AddComponent<PlayerMovementLogger>(); 
			}
			
		}
	}

}
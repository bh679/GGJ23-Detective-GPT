using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

namespace BrennanHatton.Logging
{

	public class SetupBNGRig : MonoBehaviour
	{
		
		public GrabberLogger[] grabberLoggers;
		
		void Reset()
		{
			Grabber[] grabbers = GameObject.FindObjectsOfType<Grabber>();
			grabberLoggers = new GrabberLogger[grabbers.Length];
			for(int i = 0 ;i <grabbers.Length; i++)
			{
				grabberLoggers[i] = grabbers[i].gameObject.AddComponent<GrabberLogger>(); 
			}
		}
	}

}
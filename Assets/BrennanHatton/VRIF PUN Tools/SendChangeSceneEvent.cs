using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Networking.Events
{
	public class SendChangeSceneEvent : MonoBehaviour
	{
		public int sceneId = 0, delay = 2;
		
		public void SendChangeScene()
		{
			SendChangeScene(sceneId);
		}
		
		public void SendChangeScene(int _sceneId)
		{
			SendChangeScene(_sceneId, delay);
		}
		
		public void SendChangeScene(int _sceneId, int _delay)
		{
			sceneId = _sceneId;
			delay = _delay;
			
			SendEventManager.SendChangeScene(sceneId, delay);
		}
	}
}
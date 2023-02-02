using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.SceneManagement;

namespace BrennanHatton.Networking.Events
{
	
	public class ReceiveChangeSceneEvent : MonoBehaviour, IOnEventCallback
	{
		public UnityEvent onReceiveChange = new UnityEvent();
		public bool changeScene = true;
		//public NetworkPlacerSpawner PlayerManager;
		
		private void OnEnable()
		{
			PhotonNetwork.AddCallbackTarget(this);
		}
	
		private void OnDisable()
		{
			PhotonNetwork.RemoveCallbackTarget(this);
		}
	
		public void OnEvent(EventData photonEvent)
		{
			byte eventCode = photonEvent.Code;
			
			if(eventCode == SendEventManager.ChangeSceneEventCode)
			{
				object[] data = (object[])photonEvent.CustomData;
				int id = (int)data[0];
				int sceneBuildId = (int)data[1];
				int delay = (int)data[2];
				
				onReceiveChange.Invoke();
				
				if(changeScene)
					StartCoroutine(changeSceneAfterTime(sceneBuildId,delay));
			}
		}
		
		IEnumerator changeSceneAfterTime(int bid, int delay)
		{
			yield return new WaitForSeconds(delay);
			
			SceneManager.LoadScene(bid);
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;

namespace BrennanHatton.Networking.Events
{
	
	public class ReceiveNarrationEvent : MonoBehaviour, IOnEventCallback
	{
		public SpeechManager speech;
		
		public UnityEvent onReceive; 
		
		void Reset()
		{
			speech = GameObject.FindObjectOfType<SpeechManager>();
		}
		
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
			
			if(eventCode == SendNarrationEventManager.NarrationTextEventCode)
			{
				object[] data = (object[])photonEvent.CustomData;
				int id = (int)data[0];
				string narration = (string)data[1];
				Debug.Log("narration: " + narration);
				speech.SpeakWithSDKPlugin(narration);
				
				onReceive.Invoke();
			}
		}
	}

}

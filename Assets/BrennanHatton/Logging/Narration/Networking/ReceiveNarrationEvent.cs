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
		public AudioSource[] waitTillStopSource;
		public int[] idsToPlay;
		public float delay = 0;
		
		public UnityEvent onReceive; 
		public UnityEvent onPlay; 
		
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
				int genId = (int)data[2];
				Debug.Log("narration: " + narration);
				
				if(idsToPlay.Length == 0 || playThisId(genId))
				{
					if(speech != null)
					StartCoroutine(playSpeechWhenAudioStops(narration));
					
					onReceive.Invoke();
				}
				
			}
		}
		
		bool playThisId(int id)
		{
			for(int i = 0; i < idsToPlay.Length; i++)
			{
				if(id == idsToPlay[i])
					return true;
			}
			
			return false;
			
		}
		
		IEnumerator playSpeechWhenAudioStops(string narration)
		{
			yield return new WaitForSeconds(delay);
			
			if(waitTillStopSource != null)
			{
				for(int i = 0; i < waitTillStopSource.Length; i++)
				{
					while(waitTillStopSource[i].isPlaying)
					{
						yield return new WaitForEndOfFrame();
	
					}
				}
			}
			
			speech.SpeakWithSDKPlugin(narration);
			onPlay.Invoke();
		}
	}

}

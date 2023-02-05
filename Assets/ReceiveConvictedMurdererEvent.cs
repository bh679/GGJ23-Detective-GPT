using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using BrennanHatton.Networking.Events;
using TMPro;

namespace DetectiveGPT
{
	
	public class ReceiveConvictedMurdererEvent : MonoBehaviour, IOnEventCallback
	{
		public int idsToPlay = 1;
		
		
		public GameStateManager gameManager; 
		public UnityEvent onReceive; 
		
		void Reset()
		{
			
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
				Debug.Log("Murderer Color: " + narration);
				
				if(idsToPlay == genId)
				{
					gameManager.convictedId = calcId(narration);
					onReceive.Invoke();
				}
			}
		}
		
		int calcId(string color)
		{
			color = color.Replace(" ", "");
			color = color.Replace("\n", "");
			Debug.Log(color);
			
			for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
			{
				if(PhotonNetwork.PlayerList[i].NickName.Substring(0,PhotonNetwork.PlayerList[i].NickName.IndexOf(" ")).Contains(color))
					return i;
			}
			
			return -1;
		}
	}

}

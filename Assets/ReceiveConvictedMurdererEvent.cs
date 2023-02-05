using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using BrennanHatton.Networking.Events;
using TMPro;
using BrennanHatton.Networking;

namespace DetectiveGPT
{
	
	public class ReceiveConvictedMurdererEvent : MonoBehaviour, IOnEventCallback
	{
		public int idsToPlay = 1;
		
		
		public GameStateManager gameManager; 
		public UnityEvent onReceive; 
		public MaterialToNickName colorNames;
		public Cage cage;
		
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
				Debug.Log("<color=red>Murderer Color: " + narration+"</color>");
				
				if(idsToPlay == genId)
				{
					cage.response = narration;
					
					gameManager.convictedId = calcId(GetColor(narration),narration);
					Debug.Log(gameManager.convictedId);
					onReceive.Invoke();
				}
			}
		}
		
		string GetColor(string response)
		{
			Debug.Log(response);
			for(int i = 0; i < colorNames.colorNames.Length; i++)
			{
				if(response.ToLower().Contains(colorNames.colorNames[i].ToLower()))
				{
					return colorNames.colorNames[i];
				}
			}
			
			return "";
		}
		
		int calcId(string color, string repsonse)
		{
			
			if(color == "")
			{
				color = repsonse;
				color = color.Replace(" ", "");
				color = color.Replace("\n", "");
			}
			
			if(color.ToLower() == "gray")
				color = "grey";
			Debug.Log(color);
				
			for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
			{
				if(PhotonNetwork.PlayerList[i].NickName.ToLower().Contains(color.ToLower()))
					return i;
					
				Debug.Log(PhotonNetwork.PlayerList[i].NickName.ToLower() + "does not contain " + color);
			}
			
			return -1;
		}
	}

}

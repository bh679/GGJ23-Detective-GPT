using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using BrennanHatton.Logging;

namespace BrennanHatton.Networking.Events
{
	
	public class ReceiveFullLogEvent : MonoBehaviour, IOnEventCallback
	{
		public StoryMaker story;
		public int narratorId = 0;
		string output;
		
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
			
			if(eventCode == SendLoggingEventManager.SendFullLogEventCode && PhotonNetwork.IsMasterClient)
			{
				object[] data = (object[])photonEvent.CustomData;
				int id = (int)data[0];
				string log = (string)data[1];
				
				string playerName = "";
				for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
				{
					if(PhotonNetwork.PlayerList[i].ActorNumber == id)
					{
						output += PhotonNetwork.PlayerList[i].NickName;
					}
				}
				
				output += "}"+story.narrators[narratorId].preName +playerName+story.narrators[narratorId].preActions+ "{"+log;
			}
		}
		
		public void ExecuteAfterTime(float time)
		{
			StartCoroutine(output);
		}
		
		IEnumerator _ExeceuteAfterTime(float time)
		{
			yield return new WaitForSeconds(time);
			
			story.RunActions(narratorId,output);
		}
	}
}
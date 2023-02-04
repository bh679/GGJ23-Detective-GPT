using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using BNG;
using BrennanHatton.Networking.Events;
using BrennanHatton.Networking;

namespace BrennanHatton.Logging
{
	
	public class ReceiveDamageEventLog : MonoBehaviour, IOnEventCallback
	{
		public UnityEvent onReceive; 
		public Importance importance = Importance.Critical;
		
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
			
			if(eventCode == SendPVPEventManager.PlayerTakeDamage)
			{
				object[] data = (object[])photonEvent.CustomData;
				int id = (int)data[0];
				int target = (int)data[1];
				int damage = (int)data[2];
				string item= (string)data[3];
				
				Debug.Log("RecieveDamageEvent id:" + id + " targetPlayerId:" + target+" damage:" +damage);
				if(target == PhotonNetwork.LocalPlayer.ActorNumber)
				{
					Player who = NetworkManager.GetActorPlayer(id);
					LogAction log = new LogAction(true);
					if(who == null)
						log.who = "Disconnected Player";
					else
						log.who = who.NickName;
					log.did = "delt " + damage + " damage to " + PhotonNetwork.LocalPlayer.NickName;
					log.with = item;
					log.importance = importance;
					ActionLogger.Instance.Add(log);
				}
				
				onReceive.Invoke();
			}
		}
		
		
	}

}

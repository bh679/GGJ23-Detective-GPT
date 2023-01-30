using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using BNG;
using BrennanHatton.Networking;
using BrennanHatton.Networking.Events;
using ExitGames.Client.Photon;

namespace DetectiveGPT
{

	public enum GameState
	{
		PreMurder,
		WelcomeGPT,
		Investigation,
		Story,
		End
	}

	public class GameStateManager : MonoBehaviour, IOnEventCallback
	{
		public PlayerSpawnPosition spawner;
		public int purpetatorId;
		
		public GameState gameState = GameState.PreMurder;
		
		void Reset()
		{
			spawner = GameObject.FindObjectOfType<PlayerSpawnPosition>();
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
			
			if(eventCode == SendPVPEventManager.PlayerResetEventCode)
			{
				object[] data = (object[])photonEvent.CustomData;
				int id = (int)data[0];
				
				if(gameState == GameState.PreMurder)
				{
					PlayerKilled(id);
				}
				
			}
		}
	    
		public void PlayerKilled(int id)
		{
			purpetatorId = id;
			gameState = GameState.WelcomeGPT;
			
			Damageable[] damagables;
			
			for(int i = 0; i < NetworkPlayerSpawner.spawnedPlayerPrefabs.Count; i++)
			{
				damagables = NetworkPlayerSpawner.spawnedPlayerPrefabs[i].GetComponents<Damageable>();
				for(int d =0 ; d < damagables.Length; d++)
				{
					damagables[d].enabled = false;
				}
			}
		}
	}
		
}


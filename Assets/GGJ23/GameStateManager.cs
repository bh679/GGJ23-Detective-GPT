using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using BNG;
using BrennanHatton.Networking;
using BrennanHatton.Networking.Events;
using ExitGames.Client.Photon;
using UnityEngine.Events;

namespace DetectiveGPT
{

	public enum GameState
	{
		PreMurder = 0,
		Murder = 1,
		Investigation = 2,
		Story = 3,
		End = 4
	}

	public class GameStateManager : MonoBehaviourPunCallbacks, IOnEventCallback
	{
		public PlayerSpawnPosition spawner;
		public int purpetatorId;
		const string state_PlayerProp = "GameState";
		
		public UnityEvent onPreMuder, onMurder, onInvestigate, onStory, onEnd;
		
		public GameState gameState = GameState.PreMurder;
		
		void Reset()
		{
			spawner = GameObject.FindObjectOfType<PlayerSpawnPosition>();
		}
		
		
		public override void OnJoinedRoom()
		{
			
			base.OnJoinedRoom();
			
			//check state of other players
			if(PhotonNetwork.PlayerList.Length >1 && (int)PhotonNetwork.PlayerList[0].CustomProperties[state_PlayerProp] >= 1)
				SetState((GameState)((int)PhotonNetwork.PlayerList[0].CustomProperties[state_PlayerProp]));
			else
				SetState(GameState.PreMurder);
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
		
		public void SetState(GameState newState)
		{
			gameState = newState;
			Debug.LogError(gameState);
			PlayerCustomProperties.SetCustomProp<int>(state_PlayerProp,(int)gameState);
			
			switch(newState)
			{
			case GameState.PreMurder:
				onPreMuder.Invoke();
				break;
			case GameState.Murder:
				onMurder.Invoke();
				break;
			case GameState.Story:
				onStory.Invoke();
				break;
			case GameState.End:
				onEnd.Invoke();
				break;
			}
		}
	    
		public void PlayerKilled(int id)
		{
			purpetatorId = id;
			SetState(GameState.Murder);
			
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


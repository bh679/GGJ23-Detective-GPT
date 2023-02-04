using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using BNG;
using BrennanHatton.UnityTools;
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
		DrawConclusion = 3,
		End = 4
	}

	public class GameStateManager : MonoBehaviourPunCallbacks, IOnEventCallback
	{
		public PlayerSpawnPosition spawner;
		public int purpetatorId, victimId;
		const string state_PlayerProp = "GameState";
		public SetMaterial corpse;
		
		public UnityEvent onPreMuder, onMurder, onInvestigate, onDrawConclusion, onEnd;
		
		public GameState gameState = GameState.PreMurder;
		
		//Singlton
		public static GameStateManager Instance { get; private set; }
		private void Awake() 
		{ 
			// If there is an instance, and it's not me, delete myself.
		    
			if (Instance != null && Instance != this) 
			{ 
				Destroy(this); 
			} 
			else 
			{ 
				Instance = this; 
			} 
		}
		
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
					corpse.gameObject.SetActive(true);
					corpse.SetMaterialPlz(id);
				}
				
			}
		}
		
		public void SetState(GameState newState)
		{
			gameState = newState;
			Debug.Log("<color=red>"+gameState+"</color>");
			PlayerCustomProperties.SetCustomProp<int>(state_PlayerProp,(int)gameState);
			
			switch((int)newState)
			{
			case (int)GameState.PreMurder:
				onPreMuder.Invoke();
				break;
			case (int)GameState.Murder:
				onMurder.Invoke();
				break;
			case (int)GameState.Investigation:
				onInvestigate.Invoke();
				break;
			case (int)GameState.DrawConclusion:
				onDrawConclusion.Invoke();
				break;
			case (int)GameState.End:
				onEnd.Invoke();
				break;
			}
		}
	    
		public void PlayerKilled(int victum)
		{
			//purpetatorId = murderer;
			victimId = victum;
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
		
		public void StartInvestigation()
		{
			SetState(GameState.Investigation);
		}
		
		public void DrawConclusion()
		{
			SetState(GameState.DrawConclusion);
		}
		
		public void DoneReadingStory()
		{
			SetState(GameState.End);
		}
		
		public void ResetPlayerProp()
		{
			PlayerCustomProperties.SetCustomProp<int>(state_PlayerProp,(int)GameState.PreMurder);
		}
	}
		
}


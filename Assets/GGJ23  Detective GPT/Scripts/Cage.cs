using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using BrennanHatton.Networking;

namespace DetectiveGPT
{
	public class Cage : MonoBehaviour
	{
		public GameStateManager gameManager;
		public Transform groundTarget;
		public GameObject Detective;
		public string response;
		public DetectiveGPTNarrator DGPT;
		public AudioSource boomSource;
		public float playerOffset = -1.793f;
		float offset = 0;
		
		void Reset()
		{
			gameManager = GameObject.FindObjectOfType<GameStateManager>();
		}
		
		void OnEnable()
		{
			MoveOverPlayer();
		}
		
		public void MoveOverPlayer()
		{
			
			Transform conviceted;
			if(gameManager.convictedId >= 0)
			{
				Debug.Log(gameManager.convictedId);
				PhotonView playerPhotonView = NetworkPlayerSpawner.GetPlayerByActor(gameManager.convictedId);
				Debug.Log(playerPhotonView);
				conviceted = playerPhotonView.GetComponent<BrennanHatton.Networking.NetworkPlayer>().head;
				
				offset = playerOffset;
			}else if(response.Contains(DGPT.GetNarratorName()))
				conviceted = Detective.transform;
			else
			{
				ObjectCage();
				
				return;
			}
			
			boomSource.Play();
			this.transform.position = new Vector3(
				conviceted.position.x, 
				transform.position.y + offset,
				conviceted.position.z
			);
		
			groundTarget.position = new Vector3(groundTarget.position.x, conviceted.position.y, groundTarget.position.z);
		}
		
		void ObjectCage()
		{
			
		}
	}
}
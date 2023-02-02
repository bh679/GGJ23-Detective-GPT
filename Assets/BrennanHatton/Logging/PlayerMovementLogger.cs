using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace BrennanHatton.Logging
{
	public enum MovementState
	{
		Standing = 0,
		Crouching = 1,
		WalkingSlowly = 2,
		Walking = 2,
		Running = 3,
		Teleporting = 4,
		Turned = 5
		
	}
	
	public class PlayerMovementLogger : MonoBehaviour
	{
		
		//public CharacterController characterController;
		public float checkSeconds = 2f;
		public float runSpeed = 0.000003f;
		MovementState lastMovementStat;
		public MovementState movementState;
		public Importance importance = Importance.Unimportant;
		public bool usePhotonName = true;
		
		void Reset()
		{
			//characterController = this.GetComponent<CharacterController>();
		}
		
	    // Start is called before the first frame update
	    void Start()
		{
			lastMovementStat = MovementState.Standing;
			movementState = MovementState.Standing;
			
			StartCoroutine(CheckMovement());
		}
	    
		void Update()
		{
			
			
		}
		
		Vector3 lastPos;
	
		float speed;
		// Update is called once per frame
		IEnumerator CheckMovement()
		{
			while(true)
			{
				yield return new WaitForSeconds(checkSeconds);
				
				speed = distanceMoved()/checkSeconds;
				
				if(speed == 0)
				{
					movementState = MovementState.Standing;
				}else if(speed < runSpeed/2f)
				{
					movementState = MovementState.WalkingSlowly;
				}else if(speed < runSpeed)
				{
					movementState = MovementState.Walking;
				}else 
				{
					movementState = MovementState.Running;
				}
						
				SendStateChange();
			}
		}
		
		float distanceMoved()
		{
			float distance = Vector3.Distance(lastPos, this.transform.position);
			lastPos = this.transform.position;
			return distance;
		}
	    
		void SendStateChange()
		{
			//Debug.Log(movementState);
			if(lastMovementStat != movementState)
			{
				LogAction log = new LogAction();
				if(usePhotonName)
					log.who = PhotonNetwork.LocalPlayer.NickName;
				log.did = "Started";
				log.what = movementState.ToString();
				log.with = "";
				log.importance = importance;
				ActionLogger.Instance.Add(log);
				
				//Debug.Log(log.GetString());
			}
			
			lastMovementStat = movementState;
		}
	}
}
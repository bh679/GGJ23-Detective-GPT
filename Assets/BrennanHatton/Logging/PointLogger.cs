using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Networking;
using Photon.Pun;
using DetectiveGPT;

namespace BrennanHatton.Logging
{

	public class PointLogger : MonoBehaviour
	{

		[Tooltip("If PhysicsCheckType = Trigger and this is true, an additonal raycast check will occur to check for obstacles in the way")]
		public bool TriggerRequiresRaycast = true;

		public float RaycastLength = 20f;

		public float SphereCastLength = 20f;
		public float SphereCastRadius = 0.05f;

		public LayerMask RemoteGrabLayers = ~0;

		private Collider _lastColliderHit = null;
		
		public float resetFrequencey = 5f;
		
		public LogAction log = new LogAction(false);
		public GameObject detective;
		
		
		public bool usePhotonName = true;
		BrennanHatton.Networking.NetworkPlayer lastPlayer;
		
		void Reset()
		{
			log = new LogAction(false);
			log.who = "Player";
			log.did = "pointed at";
			log.what = "";
			log.with = "their finger";
		}

		float timer= 0;
		public virtual void Update() {
				RaycastHit hit;
			if (Physics.SphereCast(transform.position, SphereCastRadius, transform.forward, out hit, SphereCastLength)) {
					ObjectHit(hit.collider);
				}
				else if (_lastColliderHit != null) {
					RemovePreviousHitObject();
				}
				
			if(timer >= resetFrequencey)
			{
				timer = 0;
				lastPlayer = null;
			}
			timer+=Time.deltaTime;
		}

		private void ObjectHit(Collider colliderHit) {
			// Did our last item change?
			if (_lastColliderHit != colliderHit) {
				
				if(colliderHit.gameObject == detective)
				{
					lastPlayer = null;
					if(usePhotonName)
						log.who = PhotonNetwork.LocalPlayer.NickName;
					log.what = DetectiveGPTNarrator.Instance.GetNarratorName();
					log.when = Time.time.ToString();;
					//Debug.Log("pointed at " + log.what);
					ActionLogger.Instance.Add(log);
				}else{
					
				
					BrennanHatton.Networking.NetworkPlayer player = colliderHit.GetComponentInParent<BrennanHatton.Networking.NetworkPlayer>();
				
					if((player != null && lastPlayer != player))
					{
						lastPlayer = player;
						if(usePhotonName)
							log.who = PhotonNetwork.LocalPlayer.NickName;
						log.what = player.PhotonView.Owner.NickName;
						log.when = Time.time.ToString();;
						//Debug.Log("pointed at " + log.what);
						ActionLogger.Instance.Add(log);
					}
				}
				
				
				RemovePreviousHitObject();
			}

			_lastColliderHit = colliderHit;

		}

		public void RemovePreviousHitObject() {
			if (_lastColliderHit == null) return;


			_lastColliderHit = null;
		}
	}
	
}
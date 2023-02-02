using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Networking;
//using BNG;

namespace BrennanHatton.Logging
{

	public class PointLogger : MonoBehaviour
	{
		//public RemoteGrabType PhysicsCheckType = RemoteGrabType.Trigger;

		[Tooltip("If PhysicsCheckType = Trigger and this is true, an additonal raycast check will occur to check for obstacles in the way")]
		public bool TriggerRequiresRaycast = true;

		public float RaycastLength = 20f;

		public float SphereCastLength = 20f;
		public float SphereCastRadius = 0.05f;

		public LayerMask RemoteGrabLayers = ~0;

		// Grabber we can hand objects off to
		//public GrabbablesInTrigger ParentGrabber;

		private Collider _lastColliderHit = null;
		
		
		public LogAction log;
		BrennanHatton.Networking.NetworkPlayer lastPlayer;
		
		void Reset()
		{
			log = new LogAction(false);
			log.who = "Player";
			log.did = "pointed at";
			log.what = "";
			log.with = "their finger";
		}

		/*void Start() {
			if(PhysicsCheckType == RemoteGrabType.Trigger && GetComponent<Collider>() == null) {
				Debug.LogWarning("Remote Grabber set to 'Trigger', but no Trigger Collider was found. You may need to add a collider, or switch to a different physics check type.");
			}

			// Add a raycast check if we're using a trigger type. Trigger types don't check collision.
			if (PhysicsCheckType == RemoteGrabType.Trigger && TriggerRequiresRaycast && ParentGrabber != null) {
				ParentGrabber.RaycastRemoteGrabbables = true;
			}
		}*/

		public virtual void Update() {
			/*/if (PhysicsCheckType == RemoteGrabType.Raycast) {

				RaycastHit hit;
				if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, RaycastLength, RemoteGrabLayers)) {
					ObjectHit(hit.collider);
				}
				else if (_lastColliderHit != null) {
					RemovePreviousHitObject();
				}
			}
			//else if (PhysicsCheckType == RemoteGrabType.Spherecast) {*/
				RaycastHit hit;
			if (Physics.SphereCast(transform.position, SphereCastRadius, transform.forward, out hit, SphereCastLength)) {
					ObjectHit(hit.collider);
				}
				else if (_lastColliderHit != null) {
					RemovePreviousHitObject();
				}
			//}
		}

		private void ObjectHit(Collider colliderHit) {
			// Did our last item change?
			if (_lastColliderHit != colliderHit) {
				
				BrennanHatton.Networking.NetworkPlayer player = colliderHit.GetComponentInParent<BrennanHatton.Networking.NetworkPlayer>();
				
				if(player != null && lastPlayer != player)
				{
					lastPlayer = player;
					log.what = player.PhotonView.Owner.NickName;
					ActionLogger.Instance.Add(log);
				}
				
				
				RemovePreviousHitObject();
			}

			_lastColliderHit = colliderHit;

		}

		public void RemovePreviousHitObject() {
			if (_lastColliderHit == null) return;


			_lastColliderHit = null;
		}/*

		void OnTriggerEnter(Collider other) {
            
			// Skip check for other PhysicsCheckTypes
			if (ParentGrabber == null || PhysicsCheckType != RemoteGrabType.Trigger) {
				return;
			}
            
			// Ignore Raycast Triggers
			if(other.gameObject.layer == 2) {
				return;
			}

			//  We will let this grabber know we have remote objects available           
			Grabbable grabObject = other.GetComponent<Grabbable>();
			if(grabObject != null && ParentGrabber != null) {
				ParentGrabber.AddValidRemoteGrabbable(other, grabObject);
				return;
			}

			// Check for Grabbable Child Object Last
			GrabbableChild gc = other.GetComponent<GrabbableChild>();
			if (gc != null && ParentGrabber != null) {
				ParentGrabber.AddValidRemoteGrabbable(other, gc.ParentGrabbable);
				return;
			}
		}

		void OnTriggerExit(Collider other) {

			// Skip check for other PhysicsCheckTypes
			if (ParentGrabber == null || PhysicsCheckType != RemoteGrabType.Trigger) {
				return;
			}

			Grabbable grabObject = other.GetComponent<Grabbable>();
			if (grabObject != null && ParentGrabber != null) {
				ParentGrabber.RemoveValidRemoteGrabbable(other, grabObject);
				return;
			}

			// Check for Grabbable Child Object Last
			GrabbableChild gc = other.GetComponent<GrabbableChild>();
			if (gc != null && ParentGrabber != null) {
				ParentGrabber.RemoveValidRemoteGrabbable(other, gc.ParentGrabbable);
				return;
			}
		}*/
	}
	
}
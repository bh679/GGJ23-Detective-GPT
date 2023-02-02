using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using BrennanHatton.BNGTools;

namespace BrennanHatton.Logging
{
	public class NetworkedDamageableLogs : MonoBehaviour
	{
		public DamageableEvents damageable;
		public PhotonView player;
		
		public string targetName;
		public Importance importance = Importance.Critical;
		
		void Reset()
		{
			damageable = this.GetComponent<DamageableEvents>();
			player = this.GetComponentInParent<PhotonView>();
			targetName = this.gameObject.name;
		}
		
	    // Start is called before the first frame update
	    void Start()
		{
		    damageable.onDamagedDetails.AddListener(LogAndNetworkDamage);
	    }
	    
		void LogAndNetworkDamage(float damageAmount, GameObject sender)
		{
			LogAction log = new LogAction();
			log.who = PhotonNetwork.LocalPlayer.NickName;;
			log.did = "delt " + damageAmount + " damage to " + (player==null?"":player.Owner.NickName+"'s ") + targetName;
			log.with = sender.name;
			log.importance = importance;
			ActionLogger.Instance.Add(log);
		}
	}
}
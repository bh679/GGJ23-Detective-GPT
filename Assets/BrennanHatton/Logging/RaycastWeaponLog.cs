using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using Photon.Pun;

namespace BrennanHatton.Logging
{
	public class RaycastWeaponLog : MonoBehaviour
	{
		public RaycastWeapon weapon;
		
		public Importance importance = Importance.SlightlyImportant;
		public bool usePhotonName = true;
		bool shotHit = false;
		
		void Reset()
		{
			weapon = this.GetComponent<RaycastWeapon>();
		}
		
	    // Start is called before the first frame update
	    void Start()
	    {
		    weapon.onShootEvent.AddListener(()=>{LogShot();});
		    weapon.onRaycastHitEvent.AddListener(LogShotHit);
		    shotHit = false;
	    }
	    
		public void LogShotHit(RaycastHit hit)
		{
			shotHit = true;
			LogAction log = new LogAction();
			if(usePhotonName)
				log.who = PhotonNetwork.LocalPlayer.NickName;
			log.did = "Shot";
			log.what = hit.transform.gameObject.name;
			log.with = this.gameObject.name;
			log.importance = importance;
			
			ActionLogger.Instance.Add(log);
		}
	    
		public void LogShot()
		{
			if(shotHit)
			{
				shotHit = false;
				return;
			}
			LogAction log = new LogAction();
			if(usePhotonName)
				log.who = PhotonNetwork.LocalPlayer.NickName;
			else
				log.who = "Player";
			log.did = "Shot";
			log.what = "and hit nothing";
			log.with = this.gameObject.name;
			log.importance = importance;
			
			ActionLogger.Instance.Add(log);
		}
		
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace BrennanHatton.Logging
{
	public class LogAnAction : MonoBehaviour
	{
		public LogAction log = new LogAction(false);
		
		
		public bool onStart, usePhotonName = true;
		
	    // Start is called before the first frame update
	    void Start()
		{
				
		    if(onStart)
			    Log();
	    }
	    
		public void Log()
		{
			if(!this.enabled)
				return;
				
			if(usePhotonName)
				log.who = PhotonNetwork.LocalPlayer.NickName;
			ActionLogger.Instance.Add(log);
		}
	}
}
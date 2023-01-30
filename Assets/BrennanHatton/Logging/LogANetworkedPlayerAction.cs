using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace BrennanHatton.Logging
{
	public class LogANetworkedPlayerAction : MonoBehaviour
	{
		public LogAction log = new LogAction(false);
		public bool fromPlayerName = true, targetPlayerName = false, updateTime = true;
		public bool onStart;
		
	    // Start is called before the first frame update
	    void Start()
	    {
		    if(onStart)
			    Log();
	    }
	    
		public void Log()
		{
			if(fromPlayerName)
				log.who = PhotonNetwork.LocalPlayer.NickName;
				
			if(targetPlayerName)
				log.what = PhotonNetwork.LocalPlayer.NickName;
				
			if(updateTime)
			{
				log.when = Time.time.ToString();
				log.when = log.when.Substring(0, log.when.LastIndexOf(".")+2);
			}
			
			ActionLogger.Instance.Add(log);
		}
	}
}
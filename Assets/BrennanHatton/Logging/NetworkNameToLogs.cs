using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace BrennanHatton.Logging
{

	public class NetworkNameToLogs : MonoBehaviour
	{
		int logs = 0;
		
	    // Start is called before the first frame update
	    void Start()
	    {
		    logs = ActionLogger.Instance.actions.Count;
	    }
	
	    // Update is called once per frame
	    void Update()
	    {
		    if(logs != ActionLogger.Instance.actions.Count)
		    {
		    	if(ActionLogger.Instance.actions[ActionLogger.Instance.actions.Count-1].who == LogAction.defaultName)
		    	{
			    	ActionLogger.Instance.actions[ActionLogger.Instance.actions.Count-1].who = "'"+PhotonNetwork.LocalPlayer.NickName+"'";
			    	
			    	ActionLogger.Instance.output = ActionLogger.Instance.output.Replace(LogAction.defaultName,"'"+PhotonNetwork.LocalPlayer.NickName+"'");
		    	}
		    }
	    }
	}

}
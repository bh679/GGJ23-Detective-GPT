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
		    	if(ActionLogger.Instance.actions[ActionLogger.Instance.actions.Count-1].who == "Player")
			    	ActionLogger.Instance.actions[ActionLogger.Instance.actions.Count-1].who = "'"+PhotonNetwork.LocalPlayer.NickName+"'";
		    }
	    }
	}

}
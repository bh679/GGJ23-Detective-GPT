using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace BrennanHatton.Logging
{
	public class RunActionsIfLocalPlayer : MonoBehaviour
	{
		public PhotonView player;
		public bool onStart;
		
		void Reset()
		{
			player = this.GetComponent<PhotonView>();
		}
		
	    // Start is called before the first frame update
	    void Start()
	    {
		    if(onStart)
			    RunActions();
	    }
	
		public void RunActions()
		{
			if(player.Owner.IsLocal)
			{
				
				GlobalStoryManager.Instance.storyMaker.RunActions();
			}
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

namespace BrennanHatton.Networking
{

	public class PhotonChangeServer : MonoBehaviourPunCallbacks
	{
		static bool isDefault = true;
		public string target = "au";
		public UnityEvent onChange;
		public NetworkManager netManager;
		bool defaultAutoReconnect;
		
		void Start()
		{
			defaultAutoReconnect = netManager.autoReconnect;
			
			if(PhotonNetwork.CloudRegion == target)
				target = "usw";
		}
		
		public void ChangeToTarget(string targ = "")
		{
			if (targ != null)
				target = targ;
				
			netManager.autoReconnect = false;
			PhotonNetwork.Disconnect();
			PhotonNetwork.ConnectToRegion(target);
			onChange.Invoke();
		}
		
		public void ChangeToDefault()
		{
			netManager.autoReconnect = true;
			PhotonNetwork.Disconnect();
			onChange.Invoke();
		}
		
		public void SwitchTargetDefault()
		{
			if(isDefault)
				ChangeToTarget();
			else
				ChangeToDefault();
				
			isDefault = !isDefault;
		}
		
		
		public override void OnJoinedRoom()
		{
			netManager.autoReconnect = defaultAutoReconnect;
			base.OnJoinedRoom();
		}
		
	}

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.UnityTools;
using Photon.Pun;

namespace BrennanHatton.Networking
{
	public class MaterialToNickName : MonoBehaviour
	{
		SetMaterial matset;
		public string[] colorNames;
		string OGname;
		public bool expectAvatar = true;
		
		public void SetOGName()
		{
			OGname = PhotonNetwork.NickName;
		}
		
		public void SetNickName()
		{
			SetOGName();
			
			StartCoroutine(setNickNameWhenRead());
			
		}
		
		
		IEnumerator setNickNameWhenRead()
		{
			if(matset == null)
			{
				while(NetworkPlayerSpawner.localPlayer == null && expectAvatar)
					yield return new WaitForSeconds(0.2f);
				
				if( NetworkPlayerSpawner.localPlayer != null)
				{
					matset = NetworkPlayerSpawner.localPlayer.gameObject.GetComponentInChildren<SetMaterial>();
					
					matset.onMatChange.AddListener(SetName);
				}
			}
			
			if(matset != null)
				SetName(matset.MaterialId);
				
		}
		
		public void SetName(int id)
		{
			PhotonNetwork.NickName = colorNames[id] + " " + OGname;
		}
	}
}
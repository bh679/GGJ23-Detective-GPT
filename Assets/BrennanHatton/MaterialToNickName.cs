using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.UnityTools;
using Photon.Pun;

namespace BrennanHatton.Networking
{
	public class MaterialToNickName : MonoBehaviour
	{
		public SetMaterial matset;
		public string[] colorNames;
		
		void Start()
		{
			matset.onMatChange.AddListener(SetName);
		}
		
		public void SetName(int id)
		{
			PhotonNetwork.NickName = colorNames[id] + " " + PhotonNetwork.NickName;
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.UnityTools;
using Photon.Pun;

namespace BrennanHatton.Networking
{
	public class MaterialToNickName : MonoBehaviour
	{
		public string[] colorNames;
		
		public void SetName(int id)
		{
			PhotonNetwork.NickName = colorNames[id] + " " + PhotonNetwork.NickName;
		}
	}
}
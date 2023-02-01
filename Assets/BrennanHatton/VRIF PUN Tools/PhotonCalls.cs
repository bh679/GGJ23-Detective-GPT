using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace BrennanHatton.Networking
{

	public class PhotonCalls : MonoBehaviour
	{
			
		public static string GetPlayerName(int actor)
		{
			for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
			{
				if(PhotonNetwork.PlayerList[i].ActorNumber == actor)
					return PhotonNetwork.PlayerList[i].NickName;
			}
				
			return null;
		}
	}

}
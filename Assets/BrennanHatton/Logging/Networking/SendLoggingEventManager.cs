using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.SceneManagement;

namespace BrennanHatton.Networking.Events
{
	
	public class SendLoggingEventManager : MonoBehaviourPunCallbacks
	{
		// If you have multiple custom events, it is recommended to define them in the used class
		public const byte SendFullLogEventCode = 164;
		
		
		public static void SendFullLogEvent(string log)
		{
			//Debug.Log("SendUpdateHealthEvent id:" + PhotonNetwork.LocalPlayer.ActorNumber + " targetPlayerId:" + targetPlayerId+" damage:" +damage);
			object[] content = new object[] { PhotonNetwork.LocalPlayer.ActorNumber, log }; // Array contains the target position and the IDs of the selected units
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
			PhotonNetwork.RaiseEvent(SendFullLogEventCode, content, raiseEventOptions, SendOptions.SendReliable);
		}
	
	}

}
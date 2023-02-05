using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

namespace BrennanHatton.Networking.Events
{
	
	public class SendNarrationEventManager : MonoBehaviourPunCallbacks
	{
		// If you have multiple custom events, it is recommended to define them in the used class
		public const byte StreamEventCode = 175,
		AudioclipEventCode = 176,
		NarrationTextEventCode = 177;
		
		public static void SendAudioclipEvent(byte[] data)
		{
			Debug.Log("SendUpdateHealthEvent id:" + PhotonNetwork.LocalPlayer.ActorNumber + " data:" + data);
			object[] content = new object[] { PhotonNetwork.LocalPlayer.ActorNumber, data }; // Array contains the target position and the IDs of the selected units
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
			PhotonNetwork.RaiseEvent(AudioclipEventCode, content, raiseEventOptions, SendOptions.SendReliable);
		}
		
		
		public static void SendNarrationTextEvent(string narration, int id)
		{
			Debug.Log("SendNarrationTextEvent id:" + PhotonNetwork.LocalPlayer.ActorNumber + " narration:" + narration);
			object[] content = new object[] { PhotonNetwork.LocalPlayer.ActorNumber, narration, id }; // Array contains the target position and the IDs of the selected units
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
			PhotonNetwork.RaiseEvent(NarrationTextEventCode, content, raiseEventOptions, SendOptions.SendReliable);
		}
	
	}

}
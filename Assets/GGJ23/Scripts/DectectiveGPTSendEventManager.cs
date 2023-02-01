using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.SceneManagement;

namespace DetectiveGPT
{
	
	public class DectectiveGPTSendEventManager : MonoBehaviourPunCallbacks
	{
		// If you have multiple custom events, it is recommended to define them in the used class
		public const byte SendQuestionAnswerStringEventCode = 201,
		SendGPTResponseEventCode = 202;
		
		
		public static void SendQuestionAnswerString(int answerId, string data)
		{
			Debug.Log("SendQuestionAnswerString id:" + PhotonNetwork.LocalPlayer.ActorNumber + " data:" + data);
			
			// Array contains the target position and the IDs of the selected units
			object[] content = new object[] { PhotonNetwork.LocalPlayer.ActorNumber,answerId, data }; 
			
			// You would have to set the Receivers to All in order to receive this event on the local client as well
			RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; 
			
			PhotonNetwork.RaiseEvent(SendQuestionAnswerStringEventCode, content, raiseEventOptions, SendOptions.SendReliable);
		}
		
	
	
	}

}
// sample code by unitycoder.com
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using BrennanHatton.AI;

namespace BrennanHatton.Logging
{
	[System.Serializable]
	public class Narrrator
	{
		public string name;
		public TextAsset personality, actionDataFormat, instructions, preName, preActions;
		public TextAsset initalPrompt, followupPrompts, introduction, questions;
		
		public string GetActionPrompt(string playerName, string actionLog)
		{
			return personality.text + " \n\n" + actionDataFormat.text + " \n\n" + instructions.text + " \n\n"
				+preName.text +playerName + " \n\n" + preActions.text + "{"+actionLog+"}";
		}
	}
	
	public class StoryMaker : MonoBehaviour
	{
		public GPT3 GPTAPI;
		public ActionLogger logger;
		public Narrrator[] narrators;
		public int id;
		
		public void Reset()
		{
			GPTAPI = this.GetComponent<GPT3>();
		}
		
		public void Introduction()
		{
			GPTAPI.Execute(narrators[id].introduction.text);
		}
		
		public void RunActions()
		{
			RunActions(-1);
		}
		public void RunActions(int narratorId = -1, string additoinalPrompts = "")
		{
			if(narratorId >= 0)
				id = narratorId;
				
			GPTAPI.Execute(narrators[id].GetActionPrompt(Photon.Pun.PhotonNetwork.LocalPlayer.NickName, logger.GetString() + additoinalPrompts));//initalPrompt.text +/* GetStorySoFar() + */"{"+logger.GetString()+"}");
			logger.actions = new List<LogAction>();
			logger.output = "";
		}
		
		public void AskQuestion()
		{
			GPTAPI.Execute(narrators[id].questions.text);
		}

		

	}
}

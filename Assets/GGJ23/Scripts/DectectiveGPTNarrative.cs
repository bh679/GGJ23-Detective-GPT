using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.AI;
using BrennanHatton.Networking;
using Photon.Pun;

namespace DetectiveGPT
{

	public class DectectiveGPTNarrative : MonoBehaviour
	{
		public DetectiveGPTQuestioning questions;
		public SpeechManager speechManager;
		public GPT3 gpt;
		public TextAsset PersonalityPrompt, PreNotesPrompt, FixesPrompt, FormatingPrompt, AcionItemPrompt;
		
	    // Start is called before the first frame update
	    void Start()
	    {
		    gpt.onResults.AddListener(
			    (interaction)=>{
			    	speechManager.SpeakWithSDKPlugin(
				    	interaction.GeneratedText
			    	);
			    }
		    );
	    }
	    
		public void CreateNarrative()
		{
			if(PhotonNetwork.PlayerList[0].ActorNumber != PhotonNetwork.LocalPlayer.ActorNumber)
				return;
			
			string promptStr = "";
			
			promptStr += PersonalityPrompt.text + "\n\n";
			promptStr += "The victim is " + Victim() + "\n\n";
			promptStr += "The " + PhotonNetwork.PlayerList.Length + " suspects are:\n"+ListOfSuspects() + "\n\n";
			promptStr += FormatingPrompt.text + "\n\n";
			promptStr += PreNotesPrompt.text + '\n';
			
			for(int i = 0; i < questions.answers.Count; i++)
			{
				promptStr += questions.answers[i].GetPromptData();
			}
			
			promptStr += "\n\n";
			promptStr += FixesPrompt.text + "\n\n";
			promptStr += AcionItemPrompt.text + "\n\n";
			
			
			gpt.Execute(promptStr);
		}
		
		public string Victim()
		{
			return PhotonCalls.GetPlayerName(GameStateManager.Instance.victimId);
		}
		
		public string ListOfSuspects()
		{
			string output = "";
			
			for(int i = 0 ; i < PhotonNetwork.PlayerList.Length; i++)
			{
				if(PhotonNetwork.PlayerList[i].ActorNumber == GameStateManager.Instance.victimId)
					output += "The late ";
				output += PhotonNetwork.PlayerList[i].NickName + "\n";
				
			}
			
			return output;
		}
	}

}
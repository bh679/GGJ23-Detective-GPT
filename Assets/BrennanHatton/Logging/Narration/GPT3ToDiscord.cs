using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.AI;

namespace BrennanHatton.Discord
{
	public class GPT3ToDiscord : MonoBehaviour
	{
		public ClassDiscordConnection discord;
		public GPT3 GPTAPI;
		
		void Reset()
		{
			discord = GameObject.FindObjectOfType<ClassDiscordConnection>();
			GPTAPI = GameObject.FindObjectOfType<GPT3>();
		}
		
		int interactionLength;
		int promptLength;
		
		void Start()
		{
			if(discord != null && GPTAPI != null)
			{
			GPTAPI.onExecute.AddListener(SendPrompt);
				GPTAPI.onResults.AddListener(SendResult);
			}else
				Debug.LogError("GPT3 Discord Connection not setup. Missing References.");
		}
	    
		void SendPrompt(InteractionData interaction)
		{
			discord.SendMessage("```Prompt: "+interaction.requestData.prompt+"```", true);
		}
		
		void SendResult(InteractionData interaction)
		{
			discord.SendMessage("Result: "+interaction.generatedText+"", true);
		}
	}
}

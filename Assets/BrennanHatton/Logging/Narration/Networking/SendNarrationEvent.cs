using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.Logging;
using BrennanHatton.AI;

namespace BrennanHatton.Networking.Events
{
	
	public class SendNarrationEvent : MonoBehaviour
	{	

		public GPT3 gpt;
		public bool runActions = true;
		
		int interactionsNumber;
		
		
		void Reset()
		{
			gpt = this.GetComponentInParent<GPT3>();
		}
		
		void Start()
		{
			interactionsNumber = gpt.interactions.Count;
		}
	
		// Update is called once per frame
		void Update()
		{
			if(runActions)
			{
				//if new story is avalible
				if(gpt.interactions.Count != interactionsNumber)
				{
					Debug.Log("Preparing to send narration");
					SendNarrationEventPlz(gpt.interactions[gpt.interactions.Count-1].generatedText, gpt.interactions.Count-1);
			    	
					interactionsNumber = gpt.interactions.Count;
				}
			}
		}
			
		public void SendNarrationEventPlz(string text, int id)
		{
			SendNarrationEventManager.SendNarrationTextEvent(text, id);
		}
	
	}

}

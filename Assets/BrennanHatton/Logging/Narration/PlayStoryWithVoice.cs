using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BrennanHatton.Logging
{
	public class PlayStoryWithVoice : MonoBehaviour
	{
	    
		public SpeechManager speech;
		public StoryMaker story;
		public bool continuousNarration = false;
		public bool runIntro = true;
		public bool runActions = true;
		
		int interactionsNumber;
		bool executing = false;
		
		
		void Reset()
		{
			speech = GameObject.FindObjectOfType<SpeechManager>();
			story = this.GetComponent<StoryMaker>();
		}
		void Start()
		{
			interactionsNumber = story.GPTAPI.interactions.Count;
			
			executing = true;
			
			if(runIntro)
				StartCoroutine(RunIntro());
		}
	
		// Update is called once per frame
		void Update()
		{
			
			//if its not current making a story
			if(continuousNarration && !executing)
			{
				//if there are actions
				if(story.logger.actions.Count > 0)
				{
					story.RunActions();
					executing = true;
				}else
				{
					story.AskQuestion();
					executing = true;
				}
				
			}
			
			//if not currently playing
			if(runActions && speech.audioSource.isPlaying == false)
			{
				//if new story is avalible
				if((executing || !continuousNarration) && story.GPTAPI.interactions.Count != interactionsNumber)
				{
					speech.SpeakWithSDKPlugin(story.GPTAPI.interactions[story.GPTAPI.interactions.Count-1].generatedText);
					// ```"+story.inputResults.text+"```" + "prompt:```"+story.inputPrompt.text+story.logger.output+"```");
			    	
					interactionsNumber = story.GPTAPI.interactions.Count;
					executing = false;
				}
			}
		}
		
		IEnumerator RunIntro()
		{
			yield return new WaitForSeconds(1f);
			
			story.Introduction();
		}
		
	}
}
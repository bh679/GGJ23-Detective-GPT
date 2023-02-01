using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.AI;

namespace DetectiveGPT
{

	public class DectectiveGPTNarrative : MonoBehaviour
	{
		public DetectiveGPTQuestioning questions;
		public SpeechManager speechManager;
		public GPT3 gpt;
		public TextAsset prompt;
		
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
	
	    // Update is called once per frame
	    void Update()
	    {
	        
	    }
	    
		public void CreateNarrative()
		{
			string promptStr = "";
			
			if(prompt != null)
				promptStr += prompt.text;
			
			for(int i = 0; i < questions.answers.Count; i++)
			{
				promptStr += questions.answers[i].GetData("{","}","");
			}
			
			
			
			gpt.Execute(promptStr);
		}
	}

}
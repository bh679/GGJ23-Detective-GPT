using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DetectiveGPT
{

	[System.Serializable]
	public class QuestionAudioData
	{
		public QuestionIDs questionId;
		public AudioClip Ask, Conclude;
	}
	
	[System.Serializable]
	public class Narrator
	{
		public string name;
		public AudioClip Intro;
		public QuestionAudioData[] Questions;
		public AudioClip Conclusion, AIWin, AILose;
		
		public Narrator()
		{
			Questions = new QuestionAudioData[3];
		}
		
		public QuestionAudioData GetQuestion(QuestionIDs questionId)
		{
			for(int i =0 ; i < Questions.Length; i++)
			{
				if(Questions[i].questionId == questionId)
				{
					return Questions[i];
				}
			}
			
			return null;
		}
	}
	
	public class DetectiveGPTNarrator : MonoBehaviour
	{
		public Narrator[] narrators;
		public int NarratorID;
		Narrator narrator;
		public AudioSource source;
		
		public static DetectiveGPTNarrator Instance { get; private set; }
		private void Awake() 
		{ 
			// If there is an instance, and it's not me, delete myself.
    
			if (Instance != null && Instance != this) 
			{ 
				Destroy(this); 
			} 
			else 
			{ 
				Instance = this; 
			} 
		}
		
		void Reset()
		{
			source = this.GetComponent<AudioSource>();
		}
		
		public void PlayIntro()
		{
			source.clip = narrator.Intro;
			source.Play();
		}
		
		public void PlayConclusion()
		{
			source.clip = narrator.Conclusion;
			source.Play();
		}
		
		public void PlayWinner()
		{
			source.clip = narrator.AILose;
			source.Play();
		}
		
		public string GetNarratorName()
		{
			return narrator.name;
		}
		
		bool PlayQuestion(QuestionIDs questionId, bool ask)
		{
			
			QuestionAudioData qad = narrator.GetQuestion(questionId);
			
			if(qad != null)
			{
				if(ask)
					source.clip = qad.Ask;
				else
					source.clip = qad.Conclude;
				source.Play();
				return true;
			}
			
			return false;
		}
		
		public bool AskQuestion(QuestionIDs questionId)
		{
			return PlayQuestion(questionId, true);
		}
		
		public bool ConcludeQuestion(QuestionIDs questionId)
		{
			return PlayQuestion(questionId, false);
		}
		
	    // Start is called before the first frame update
	    void Start()
	    {
		    NarratorID = Random.Range(0,narrators.Length-1);
		    narrator = narrators[NarratorID];
	    }
	
	    // Update is called once per frame
	    void Update()
	    {
	        
	    }
	}

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using BrennanHatton.Networking;

namespace DetectiveGPT
{

	[System.Serializable]
	public class QuestionAudioData
	{
		public QuestionIDs questionId;
		public AudioClip Ask, Conclude, ConcludeNegative;
	}
	
	[System.Serializable]
	public class Narrator
	{
		public string name;
		public AudioClip Intro;
		public QuestionAudioData[] Questions;
		public AudioClip Conclusion, AIWin, AILose;
		public GameObject[] models;
		
		public void EnableModel(bool enabled)
		{
			for(int i = 0; i < models.Length; i++)
			{
				models[i].SetActive(enabled);
			}
		}
		
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
	
	public class DetectiveGPTNarrator : MonoBehaviourPunCallbacks
	{
		public Narrator[] narrators;
		public int NarratorID;
		Narrator narrator;
		public AudioSource source;
		public UnityEvent onIntroDone, onConclusionDone, onWinnerDone;
		
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
		
		
		
		public override void OnJoinedRoom()
		{
			if(PhotonNetwork.IsMasterClient)
			{
				PickRandomNarrator();
			
			}else
			{
				PickNarrator(PlayerCustomProperties.GetCustomProp<int>(PhotonNetwork.MasterClient,"nid"));
			}
		}
		
		public void PlayIntro()
		{
			source.clip = narrator.Intro;
			source.Play();
			
			StartCoroutine(playWhenDone(onIntroDone));
		}
		
		public void PlayConclusion()
		{
			source.clip = narrator.Conclusion;
			source.Play();
			
			StartCoroutine(playWhenDone(onConclusionDone));
		}
		
		public void PlayWinner()
		{
			source.clip = narrator.AILose;
			source.Play();
			
			StartCoroutine(playWhenDone(onWinnerDone));
		}
		
		IEnumerator playWhenDone(UnityEvent eventToPlay)
		{
			while(source.isPlaying)
				yield return new WaitForEndOfFrame();
				
			eventToPlay.Invoke();
		}
		
		public string GetNarratorName()
		{
			return narrator.name;
		}
		
		bool PlayQuestion(QuestionIDs questionId, bool ask, bool positive = false)
		{
			
			QuestionAudioData qad = narrator.GetQuestion(questionId);
			
			if(qad != null)
			{
				if(ask)
					source.clip = qad.Ask;
				else if(positive)
					source.clip = qad.Conclude;
				else
					source.clip = qad.ConcludeNegative;
				source.Play();
				return true;
			}
			
			return false;
		}
		
		public bool AskQuestion(QuestionIDs questionId)
		{
			return PlayQuestion(questionId, true);
		}
		
		public bool ConcludeQuestion(QuestionIDs questionId, bool Positive = true)
		{
			return PlayQuestion(questionId, false, Positive);
		}
	    
		void PickRandomNarrator()
		{
			PickNarrator(Random.Range(0,narrators.Length));
		}
		
		public void PickNarrator(int id)
		{
			NarratorID = id;
			PlayerCustomProperties.SetCustomProp<int>("nid",NarratorID);
			narrator = narrators[NarratorID];
			
			for(int i =0; i < narrators.Length; i++)
				narrators[i].EnableModel(false);
				
			narrator.EnableModel(true);
		}
	}

}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;
using BrennanHatton.Logging;
using BrennanHatton.Networking;

namespace DetectiveGPT
{
	public enum QuetionData
	{
		Object = 0,
		Point = 1,
		Actions = 2,
		Location = 3,
		General = 4,
		Bool
	}
	
	public enum QuestionIDs
	{
		Reenactment = 0,
		MurderWeapon = 1,
		Accustation = 2,
		CrimeOfPassion = 3,
		DidTheySeeItComing = 4,
		GruesomeCrime = 5,
		IsItObvious = 6,
		TiltedLoverCrime = 7,
		MaybeAnAccident = 8,
		PerpetratorWellLiked = 9,
		PersonInNature = 10,
		ReOffend = 11,
		SmellyCrime = 12
	}
	
	[System.Serializable]
	public class Question
	{
		public QuestionIDs questionId;
		public QuetionData dataType;
		public TextAsset prompt;
		public string descrption, concludeDescriptioon;
		//public AudioClip askQuestionClip, concludeQuestoinClip;
		public float time = -1f;
		
		AudioSource source;
	}
	
	public class AnswerData
	{
		public List<string> answerData = new List<string>();
		public List<string> playerIds = new List<string>();
		public Question question;
		
		public AnswerData(Question q, string p, string a)
		{;
			question = q;
			playerIds.Add(p);
			answerData.Add(a);
		}
		
		public void AddData(string p, string a)
		{
			playerIds.Add(p);
			answerData.Add(a);
		}
		
		public string GetPromptData()
		{
			string output = question.prompt.text;
			output += GetData("","","");
			
			return output;
		}
		
		public string GetData(string start, string end, string mid)
		{
			string output = "";
			
			for(int i = 0 ;i < playerIds.Count; i++)
			{
				output += start + playerIds[i] +mid+ answerData[i] + end;
			}
			
			return output;
		}
	}
	
	public class DetectiveGPTQuestioning : MonoBehaviour, IOnEventCallback
	{
		public AudioSource soundFXAudioSound;
		public DetectiveGPTNarrator narrator;
		public SpeechManager speechManager;
		public ActionLogger logger;
		public int questionsToAsk = 1;
		public Question[] questions;
		public List<AnswerData> answers = new List<AnswerData>();
		
		public AudioClip timerCountingSound;
		public LogManager logManager;
		
		int id = 0;
		
		void Reset()
		{
			narrator = this.GetComponent<DetectiveGPTNarrator>();
			speechManager = GameObject.FindObjectOfType<SpeechManager>();
			logger = GameObject.FindObjectOfType<ActionLogger>();
			logManager = this.GetComponent<LogManager>();
		}
		
		public QuetionData currentQuestionType;
		
		private void OnEnable()
		{
			PhotonNetwork.AddCallbackTarget(this);
			logManager.DisableAll();
		}
	
		private void OnDisable()
		{
			PhotonNetwork.RemoveCallbackTarget(this);
		}
		
		public void AskNextQuestion()
		{
			//play audio
			if(!narrator.AskQuestion(questions[id].questionId))
				speechManager.SpeakWithSDKPlugin(questions[id].descrption);
				
			//set type
			currentQuestionType = questions[id].dataType;
			
			//prep logger
			logger.Clear();
			logManager.SetLoggersFromQuestion(currentQuestionType);
			
			//set timer
			if(questions[id].time >= 0)
				StartCoroutine(getDataFromLogAfterTime(questions[id].time));
			
		}
		
		public void TurnOffAllLoggers()
		{
			
		}
		
		IEnumerator getDataFromLogAfterTime(float time)
		{
			Debug.Log("getDataFromLogAfterTime");
			while(narrator.source.isPlaying || speechManager.audioSource.isPlaying)
				yield return new WaitForSeconds(1f);
			
			Debug.Log("Play " + timerCountingSound.name);
			soundFXAudioSound.PlayOneShot(timerCountingSound);
			yield return new WaitForSeconds(time);
			Debug.Log("Waited " + time);
			soundFXAudioSound.Stop();
			
			SubmitAnswer(logger.output,PhotonNetwork.LocalPlayer.NickName);
				
			yield return null;
		}
		
		public void SubmitAnswer(string answerData,string player)
		{
			float delay = 0;
			
			Debug.Log(answerData);
			//play audio
			if(!narrator.ConcludeQuestion(questions[id].questionId))
			{
				speechManager.SpeakWithSDKPlugin(questions[id].concludeDescriptioon);
				
				delay = 5f;
			}
				
			
			answers.Add(new AnswerData(questions[id],player, answerData));
			
			DectectiveGPTSendEventManager.SendQuestionAnswerString(answers.Count-1,answerData);
			
			StartCoroutine(nextQuestion(delay));
		}
		
		IEnumerator nextQuestion(float delay)
		{
			yield return new WaitForSeconds(delay);
			
			while(narrator.source.isPlaying || speechManager.audioSource.isPlaying)
				yield return new WaitForFixedUpdate();
			
			id++;
			
			if(id >= questionsToAsk)
				GameStateManager.Instance.DrawConclusion();
			else
				AskNextQuestion();
				
			yield return null;
		}
	
		public void OnEvent(EventData photonEvent)
		{
			byte eventCode = photonEvent.Code;
			
			if(eventCode == DectectiveGPTSendEventManager.SendQuestionAnswerStringEventCode)
			{
				object[] data = (object[])photonEvent.CustomData;
				int pid = (int)data[0];
				int questionId = (int)data[1];
				string answer= (string)data[2];
				string player = PhotonCalls.GetPlayerName(pid);
				//Debug.Log("RecieveDamageEvent id:" + id + " targetPlayerId:" + target+" damage:" +damage);
				if(pid != PhotonNetwork.LocalPlayer.ActorNumber)
				{
					
					answers[questionId].AddData(player, answer);
				}
				
			}
		}
	}
}
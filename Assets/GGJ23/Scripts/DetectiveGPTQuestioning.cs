using System.Collections;
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
		General = 4
	}
	
	[System.Serializable]
	public class Question
	{
		public QuetionData dataType;
		public TextAsset prompt;
		public string descrption, concludeDescriptioon;
		public AudioClip askQuestionClip, concludeQuestoinClip;
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
		public AudioSource source;
		public SpeechManager speechManager;
		public ActionLogger logger;
		public int questionsToAsk = 1;
		public Question[] questions;
		public List<AnswerData> answers = new List<AnswerData>();
		
		public AudioClip timerCountingSound;
		
		int id = 0;
		
		void Reset()
		{
			source = this.GetComponent<AudioSource>();
			speechManager = GameObject.FindObjectOfType<SpeechManager>();
			logger = GameObject.FindObjectOfType<ActionLogger>();
		}
		
		public QuetionData currentQuestionType;
		
		private void OnEnable()
		{
			PhotonNetwork.AddCallbackTarget(this);
		}
	
		private void OnDisable()
		{
			PhotonNetwork.RemoveCallbackTarget(this);
		}
		
		public void AskNextQuestion()
		{
			//play audio
			if(questions[id].askQuestionClip)
				source.PlayOneShot(questions[id].askQuestionClip);
			else
				speechManager.SpeakWithSDKPlugin(questions[id].descrption);
				
			//set type
			currentQuestionType = questions[id].dataType;
			
			//prep logger
			logger.Clear();
			
			//set timer
			if(questions[id].time >= 0)
				StartCoroutine(getDataFromLogAfterTime(questions[id].time));
			
		}
		
		IEnumerator getDataFromLogAfterTime(float time)
		{
			Debug.Log("getDataFromLogAfterTime");
			while(source.isPlaying || speechManager.audioSource.isPlaying)
				yield return new WaitForSeconds(1f);
			
			Debug.Log("Play " + timerCountingSound.name);
			source.PlayOneShot(timerCountingSound);
			yield return new WaitForSeconds(time);
			Debug.Log("Waited " + time);
			source.Stop();
			
			SubmitAnswer(logger.output,PhotonNetwork.LocalPlayer.NickName);
		}
		
		public void SubmitAnswer(string answerData,string player)
		{
			Debug.Log(answerData);
			if(questions[id].concludeQuestoinClip)
				source.PlayOneShot(questions[id].concludeQuestoinClip);
			else
				speechManager.SpeakWithSDKPlugin(questions[id].concludeDescriptioon);
			
			
			answers.Add(new AnswerData(questions[id],player, answerData));
			
			DectectiveGPTSendEventManager.SendQuestionAnswerString(answers.Count-1,answerData);
			
			id++;
			
			if(id >= questionsToAsk)
				GameStateManager.Instance.DrawConclusion();
			else
				AskNextQuestion();
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
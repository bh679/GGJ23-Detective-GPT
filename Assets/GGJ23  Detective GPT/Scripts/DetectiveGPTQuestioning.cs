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
		SmellyCrime = 12,
		TotalQuest = 13
	}
	
	[System.Serializable]
	public class Question
	{
		public QuestionIDs questionId;//remove
		public QuetionData dataType;
		public TextAsset prompt, negativePrompt;//remove neg
		public string descrption, concludeDescriptioon;
		public float time = -1f;
	}
	
	[System.Serializable]
	public class VoteQuestion
	{
		public QuestionIDs questionId;
		public TextAsset yesPrompt, noPrompt;
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
	
	public class DetectiveGPTQuestioning : MonoBehaviourPunCallbacks, IOnEventCallback
	{
		public MusicManager musicManager;
		public DetectiveGPTNarrator narrator;
		public SpeechManager speechManager;
		public ActionLogger logger;
		public int questionsToAsk = 1;
		public Question[] questions;
		public VoteQuestion[] voteQuestions;
		VoteQuestion[] voteQuestionsToAsk;
		int[] voteQids;
		public float voteTime = 5f;
		public List<AnswerData> answers = new List<AnswerData>();
		
		//public AudioClip timerCountingSound;
		public LogManager logManager;
		
		int qid = 0, vid = 0;
		
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
			
			base.OnEnable();
		}
		
		public override void OnJoinedRoom()
		{
			if(PhotonNetwork.IsMasterClient)
			{
				voteQids = new int[questionsToAsk];
				
				for(int i = 0; i < voteQids.Length; i++)
					voteQids[i] = -1;
				
				for(int i = 0; i < voteQids.Length; i++)
				{
					while(CheckVoteQuestionDuplicate(voteQids, voteQids[i], i) && i < voteQuestions.Length)
						voteQids[i] = Random.Range(0,voteQuestions.Length);
				}
				
				PlayerCustomProperties.SetCustomProp<int[]>("vids",voteQids);
			
			}else
			{
				voteQids = PlayerCustomProperties.GetCustomProp<int[]>(PhotonNetwork.MasterClient,"vids");
				PlayerCustomProperties.SetCustomProp<int[]>("vids",voteQids);
			}
			
			voteQuestionsToAsk = new VoteQuestion[voteQids.Length];
			for(int i = 0; i < voteQids.Length; i++)
				voteQuestionsToAsk[i] = voteQuestions[voteQids[i]];
		}
		
		public bool CheckVoteQuestionDuplicate(int[] voteQids, int voteId, int exception)
		{
			if(voteId == -1)
				return true;
			
			for(int i = 0; i < voteQids.Length; i++)
			{
				if(i != exception && voteQids[i] == voteId)
					return true;
			}
			return false;
		}
	
		private void OnDisable()
		{
			PhotonNetwork.RemoveCallbackTarget(this);
			
			base.OnDisable();
		}
		
		public void AskNextQuestion()
		{
			//play audio
			if(!narrator.AskQuestion(questions[qid].questionId))
				speechManager.SpeakWithSDKPlugin(questions[qid].descrption);
				
			//set type
			currentQuestionType = questions[qid].dataType;
			
			//prep logger
			logger.Clear();
			logManager.SetLoggersFromQuestion(currentQuestionType);
			
			//set timer
			if(questions[qid].time >= 0)
				StartCoroutine(getDataFromLogAfterTime(questions[qid].time, true));
			
		}
		
		IEnumerator getDataFromLogAfterTime(float time, bool question)
		{
			while(narrator.source.isPlaying || speechManager.audioSource.isPlaying)
				yield return new WaitForSeconds(1f);
			
			musicManager.WaitingForAnswer();
				
			yield return new WaitForSeconds(time);
			
			musicManager.AskQuestion();
			
			if(question)
				SubmitAnswer(logger.output,PhotonNetwork.LocalPlayer.NickName);
			else
				SubmitVote();
			yield return null;
		}
		
		public void SubmitAnswer(string answerData,string player)
		{
			float delay = 0;
			
			Debug.Log(answerData);
			//play audio
			if(!narrator.ConcludeQuestion(questions[qid].questionId))
			{
				speechManager.SpeakWithSDKPlugin(questions[qid].concludeDescriptioon);
				
				delay = 5f;
			}
			answers.Add(new AnswerData(questions[qid], player, answerData));
			
				DectectiveGPTSendEventManager.SendQuestionAnswerString(answers.Count-1,answerData);
			
			StartCoroutine(nextQuestion(delay));
		}
		
		IEnumerator nextQuestion(float delay)
		{
			yield return new WaitForSeconds(delay);
			
			while(narrator.source.isPlaying || speechManager.audioSource.isPlaying)
				yield return new WaitForFixedUpdate();
			
			qid++;
			
			if(qid >= questions.Length)
			{
				if(qid > questions.Length)
					vid++;
				
				if(vid < questionsToAsk)
					AskNextVoteQuestion();
				else
					GameStateManager.Instance.DrawConclusion();
			}
			else
				AskNextQuestion();
				
			yield return null;
		}
		
		public void AskNextVoteQuestion()
		{
			//play audio
			narrator.AskQuestion(voteQuestionsToAsk[vid].questionId);
				
			//prep logger
			logger.Clear();
			logManager.DisableAll();
			
			//set timer
			if(voteTime >= 0)
				StartCoroutine(getDataFromLogAfterTime(voteTime, false));
		}
		
		public void SubmitVote()
		{
			float delay = 0;
			
			//play audio
			narrator.ConcludeQuestion(voteQuestionsToAsk[vid].questionId);
				
			//save my vote
			PlayerCustomProperties.SetCustomProp<int>(voteQuestionsToAsk[vid].questionId.ToString(), (int)GestureManager.Instance.GetState());
			
			StartCoroutine(nextQuestion(delay));
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
				
				if(pid != PhotonNetwork.LocalPlayer.ActorNumber)
					answers[questionId].AddData(player, answer);
				
				
			}/*else if(eventCode == DectectiveGPTSendEventManager.VoteQuestionsEventCode)
			{
				object[] data = (object[])photonEvent.CustomData;
				int pid = (int)data[0];
				int[] vids = (int[])data[1];
				
				PlayerCustomProperties.SetCustomProp<int[]>("vids",voteQids);
				Debug.Log("vids receieved: " + vids.Length);
				voteQuestionsToAsk = new VoteQuestion[vids.Length];
				for(int i = 0; i < vids.Length; i++)
					voteQuestionsToAsk[i] = voteQuestions[vids[i]];
				
				
			}*/
		}
		
		Question GetQuestion(QuestionIDs id)
		{
			for(int i = 0; i < questions.Length; i++)
			{
				if(id == questions[i].questionId)
					return questions[i];
			}
			
			return null;
		}
		
		bool GetVoteFor(QuestionIDs q)
		{
			int voteFor = 0, voteAgainst = 0;
			
			for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
			{
				if(((ThumbState)PlayerCustomProperties.GetCustomProp<int>(PhotonNetwork.PlayerList[i], q.ToString())) == ThumbState.Up)
					voteFor++;
				else
					voteAgainst++;
			}
			
			return (voteFor >= voteAgainst);
				
		}
		
		public string GetVoteQuestionPrompts()
		{
			string output = "";
			
			for(int i = 0; i < voteQuestionsToAsk.Length; i++)	
			{
				bool voteFor = GetVoteFor(voteQuestionsToAsk[i].questionId);
				
				if(voteFor)
					output += "(10)" + voteQuestionsToAsk[i].yesPrompt +"\n";
				else
					output += "(10)" + voteQuestionsToAsk[i].noPrompt +"\n";
				
			}
			
			return output;
		}
		
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.UnityTools;

namespace DetectiveGPT
{
	[System.Serializable]
	public class AudioTrack 
	{
		public AudioClip clip;
		public float relativeVolume = 0f, fadeInLength;
		
	}

	public class MusicManager : MonoBehaviour
	{
		public AudioSource source;
		public AudioSourceExt sourceExt;
		public AudioTrack preMurder, murder, investigateAsk, investigateTimer, conlcusion, win, lose, end;
		public GameStateManager manager;
		public float firstQuestionFadeDelay = 5f;
		float volume;
		
		int questions = 0;
		
		void Play(AudioTrack track)
		{
			source.clip = track.clip;
			source.Play();
			sourceExt.VolumeFadeToTarget(track.fadeInLength, volume + track.relativeVolume);
		}
		
		void Start()
		{
			volume = source.volume;
			Play(preMurder);
			
			manager.onMurder.AddListener(()=>{Murder();});
			manager.onInvestigate.AddListener(()=>{AskQuestion();});
			manager.onDrawConclusion.AddListener(()=>{DrawConclusion();});
			manager.onEnd.AddListener(()=>{Win();});
		}
	    
		public void Murder()
		{
			Play(murder);
		}
	    
		public void AskQuestion()
		{
			if(questions > 0)
				Play(investigateAsk);
			else
			{
				Debug.LogError(firstQuestionFadeDelay);
				sourceExt.VolumeFadeToZero(firstQuestionFadeDelay);
			}
			
			questions++;
		}
	    
		public void WaitingForAnswer()
		{
			Play(investigateTimer);
		}
			
		public void DrawConclusion()
		{
			Play(conlcusion);
		}
			
		public void Win()
		{
			Play(win);
			
			StartCoroutine(playAfterFinished(end));
		}
			
		public void Lose()
		{
			Play(lose);
			
			StartCoroutine(playAfterFinished(end));
		}
		
		IEnumerator playAfterFinished(AudioTrack track)
		{
			while(source.isPlaying)
				yield return new WaitForEndOfFrame();
				
			Play(track);
		}
	}
}

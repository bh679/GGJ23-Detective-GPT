using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.UnityTools;

namespace DetectiveGPT
{

	public class MusicManager : MonoBehaviour
	{
		public AudioSource source;
		public AudioSourceExt sourceExt;
		public AudioClip preMurder, murder, investigateAsk, investigateTimer, conlcusion, win, lose, end;
		public GameStateManager manager;
		public float firstQuestionFadeDelay = 5f;
		
		int questions = 0;
		
		void Start()
		{
			source.clip = preMurder;
			source.Play();
			
			manager.onMurder.AddListener(()=>{Murder();});
			manager.onInvestigate.AddListener(()=>{AskQuestion();});
			manager.onDrawConclusion.AddListener(()=>{DrawConclusion();});
			manager.onEnd.AddListener(()=>{Win();});
		}
	    
		public void Murder()
		{
			source.clip = murder;
			source.Play();
		}
	    
		public void AskQuestion()
		{
			if(questions > 0)
			{
				source.clip = investigateAsk;
				source.Play();
			}else
				sourceExt.VolumeFadeToZero(firstQuestionFadeDelay);
			
			questions++;
		}
	    
		public void WaitingForAnswer()
		{
			source.clip = investigateTimer;
			source.Play();
		}
			
		public void DrawConclusion()
		{
			source.clip = conlcusion;
			source.Play();
		}
			
		public void Win()
		{
			source.clip = win;
			source.Play();
		}
			
		public void Lose()
		{
			source.clip = lose;
			source.Play();
		}
		
		IEnumerator playAfterFinished(AudioClip clip)
		{
			while(source.isPlaying)
				yield return new WaitForEndOfFrame();
				
			source.clip = end;
			source.Play();
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DetectiveGPT
{

	public class MusicManager : MonoBehaviour
	{
		public AudioSource source;
		public AudioClip preMurder, murder, investigate, conlcusion, win, lose, end;
		public GameStateManager manager;
		
		
		void Start()
		{
			source.clip = preMurder;
			source.Play();
			
			manager.onMurder.AddListener(()=>{Murder();});
			manager.onInvestigate.AddListener(()=>{StartInvestigation();});
			manager.onDrawConclusion.AddListener(()=>{DrawConclusion();});
			manager.onEnd.AddListener(()=>{Win();});
		}
	    
		public void Murder()
		{
			source.clip = murder;
			source.Play();
		}
	    
		public void StartInvestigation()
		{
			source.clip = investigate;
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
	}
}

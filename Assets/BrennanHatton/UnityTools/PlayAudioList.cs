using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.UnityTools
{
	

	[System.Serializable]
	public class AudioData
	{
		public AudioClip clip;
		public AudioList list;
		public PlayRandomClip random;
		public float delay = 0.1f;
		AudioSource source;
		
		int id = 0;
		
		public void Reset(AudioSource newSource)
		{
			id = 0;
			source = newSource;
			
			if(list != null) list.source = source;
			if(random != null) random.source = source;
			
		}
		
		//plays next item and returns if here is more to play
		public bool PlayNext()
		{
			if(id == 0 && clip!=null)
			{
				source.PlayOneShot(clip);
				id++;
				return true;
			}
			else if(list!=null && id < list.audioClips.Count )
			{
				list.PlayClip(id-1);
				id++;
				return true;
			}else if(random!=null)
			{
				random.playRandomClip();
				id++;
				return false;
			}
			
			return false;
		}
	}
	
	public class PlayAudioList : MonoBehaviour
	{
		public AudioSource source;
		public List<AudioData> audio;
		public bool playOnStart = true;
		
		void Reset()
		{
			source = this.GetComponent<AudioSource>();
		}
		
	    // Start is called before the first frame update
	    void Start()
	    {
		    if(playOnStart)
			    Play(0);
	    }
	    
		public void Play(int id = 0)
		{
			checkSourceFreeFirst = false;
			Setup();
			
			StartCoroutine(playAll(id));
		}
		
		public void PLayWhenSourceFree(int id = 0)
		{
			checkSourceFreeFirst = true;
			Setup();
			
			StartCoroutine(playAll(id));
		}
		
		void Setup()
		{
			for(int i = 0; i < audio.Count; i++)
			{
				audio[i].Reset(source);
			}
		}	
		
		bool checkSourceFreeFirst = false;
		IEnumerator playAll(int id)
		{
			if(checkSourceFreeFirst)
			{
				while(source.isPlaying)
				{
					yield return new WaitForEndOfFrame();
				}
			}
			
			bool more = true;
			for(int i = 0; i < audio.Count; i++)
			{
				
				while(more)
				{
					yield return new WaitForSeconds(audio[i].delay);
					more = audio[i].PlayNext();
					while(source.isPlaying)
					{
						yield return new WaitForEndOfFrame();
					}
				}
			}
		}	
		
		
	}

}
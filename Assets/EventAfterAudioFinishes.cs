using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BrennanHatton.UnityTools
{
	
	public class EventAfterAudioFinishes : MonoBehaviour
	{
		
		public UnityEvent onFinish = new UnityEvent();
	    
		public void RunWhenFinished(AudioSource audioSource)
		{
			StopCoroutine(runWhenFinished(audioSource));
		}
		
		IEnumerator runWhenFinished(AudioSource audioSource)
		{
			while(audioSource.isPlaying)
			{
				yield return new WaitForEndOfFrame();
			}
			
			onFinish.Invoke();
			
			yield return null;
		}
	}
	
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.UnityTools
{

	public enum Platform
	{
		Android,
		Windows,
		Mac
	}

	public class PlatformEnable : MonoBehaviour
	{
		public GameObject[] Android, Windows;
		public Platform debugPlatform;
		
	    // Start is called before the first frame update
	    void Start()
		{
	 #if UNITY_ANDROID
			Android.SetActive(true);
			Windows.SetActive(false);
	 #endif
	 #if UNITY_WINDOWS
			Android.SetActive(false);
			Windows.SetActive(true);
	 #endif
	 #if UNITY_MAC
			Android.SetActive(false);
			Windows.SetActive(true);
	 #endif
	 
	 #if UNITY_EDITOR
			if(debugPlatform == Platform.Android)
			{
				Android.SetActive(true);
				Windows.SetActive(false);
			}else if(debugPlatform == Platform.Windows)
			{
				Android.SetActive(false);
				Windows.SetActive(true);
			}
	 #endif
	    }
	
	    // Update is called once per frame
	    void Update()
	    {
	        
	    }
	}

}
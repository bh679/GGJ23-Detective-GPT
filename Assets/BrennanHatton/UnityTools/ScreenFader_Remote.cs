using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

namespace BrennanHatton.UnityTools
{

	public class ScreenFader_Remote : MonoBehaviour
	{
		public void DoFadeIn()
		{
			Camera.main.GetComponent<ScreenFader>()?.DoFadeIn();
			Camera.main.GetComponent<UIImageFade>()?.StartFadeToBlack(2f);
		}
		
		public void DoFadeOut_BlackToClear()
		{
			Camera.main.GetComponent<ScreenFader>()?.DoFadeOut();
			Camera.main.GetComponent<UIImageFade>()?.StartFadeToClear(2f);
		}
	}

}
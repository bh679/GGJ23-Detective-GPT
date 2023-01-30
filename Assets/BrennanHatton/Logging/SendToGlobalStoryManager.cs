using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrennanHatton.Logging
{

	public class SendToGlobalStoryManager : MonoBehaviour
	{
		public void RunActions()
		{
			GlobalStoryManager.Instance.storyMaker.RunActions();
		}
	}
}
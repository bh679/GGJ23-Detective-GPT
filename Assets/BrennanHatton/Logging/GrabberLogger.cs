using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using Photon.Pun;

namespace BrennanHatton.Logging
{
	
	[System.Serializable]
	public class VelocityLogs
	{
		public float velocityMin;
		public string log;
		[Range(0,10)]
		public int relativeImportance = 0;
		
		public VelocityLogs(float f, string l, int r)
		{
			velocityMin = f;
			log = l;
			relativeImportance = r;
			
		}
	}
	
	public class GrabberLogger : MonoBehaviour
	{
		public Grabber grabber;
		public string handSide;
		public Importance importance = Importance.Important;
		public bool usePhotonName = true;
		
		public List<VelocityLogs> velocityLogs = new List<VelocityLogs>();
		void Reset()
		{
			grabber = this.GetComponent<Grabber>();
			handSide = grabber.HandSide.ToString() + " hand";
			velocityLogs.Add(new VelocityLogs(0,"Release",0));
				
		}
		
		void Start()
		{
			grabber.onGrabEvent.AddListener(OnGrab);
			grabber.onReleaseEvent.AddListener(OnRelease);
		}
	
		public void OnGrab(Grabbable grabbable) {
			
			LogAction log = new LogAction(true);
			if(usePhotonName)
				log.who = PhotonNetwork.LocalPlayer.NickName;
			log.did = "Grabbed";
			log.what = grabbable.name;
			log.with = handSide;
			log.importance = importance;
			ActionLogger.Instance.Add(log);
		}
		
		VelocityLogs highesetMinLog ;
		public void OnRelease(Grabbable grabbable) {
			
			getVelocityLog();
			
			LogAction log = new LogAction(true);
			if(usePhotonName)
				log.who = PhotonNetwork.LocalPlayer.NickName;
			log.did =  highesetMinLog.log;
			log.what = grabbable.name;
			log.with = handSide;
			log.importance = (Importance)((int)importance + (int)highesetMinLog.relativeImportance);
			
			ActionLogger.Instance.Add(log);
		}
	    
		VelocityLogs getVelocityLog()
		{
			Debug.Log(grabber.GetGrabberAveragedVelocity().magnitude);
			
			float magVelocity = grabber.GetGrabberAveragedVelocity().magnitude;
			highesetMinLog = null;
			
			for(int i = 0;i < velocityLogs.Count; i++)
			{
				if(highesetMinLog == null)
					highesetMinLog = velocityLogs[i];
				else
				{
					if(magVelocity >= velocityLogs[i].velocityMin && highesetMinLog.velocityMin < velocityLogs[i].velocityMin)
						highesetMinLog = velocityLogs[i];
				}
			}
			
			return highesetMinLog;
		}
	}
	
}
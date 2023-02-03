using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using Photon.Pun;

namespace BrennanHatton.Logging
{
	public enum ThumbState
	{
		Up = 2,
		Side = 1,
		Down = 0
	}
	public class GestureLogger : MonoBehaviour
	{
		public ControllerHandedness hand;
		public Vector2 midPoint;
		public ThumbState state;
		public bool useLogging = true;
		ThumbState lastState;
		public LogAction log;
		
		void Reset()
		{
				log = new LogAction();
				log.who = "";
				log.did = "gave a";
				log.what = "";
				log.with = hand.ToString() + " hand";
		}
	
	    // Update is called once per frame
	    void Update()
		{
			if(transform.up.y > midPoint.y)
				state = ThumbState.Up;
			else if(transform.up.y < midPoint.x)
				state = ThumbState.Down;
			else
				state = ThumbState.Side;
				
			if(lastState != state)
			{
				if(useLogging)
				{
					log.who = PhotonNetwork.LocalPlayer.NickName;
					log.what = state.ToString();
					ActionLogger.Instance.Add(log);
				}
				
				lastState = state;
			}
	    }
	    
		
	}

}
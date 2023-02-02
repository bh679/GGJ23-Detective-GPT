using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DGPTAnimationController : MonoBehaviour
{
	
	public AudioSource source;
	public Animator animator;
	public UnityEvent onEnd;
	
    // Start is called before the first frame update
    void Start()
    {
	    animator.SetTrigger("Spawn");
	    animator.SetInteger("Random",(int)Random.Range(0,3));
    }

    // Update is called once per frame
    void Update()
    {
	    animator.SetBool("Talking",source.isPlaying);
        
    }
    
	public void End()
	{
		animator.SetTrigger("Goodbye");
		onEnd.Invoke();
	}
}

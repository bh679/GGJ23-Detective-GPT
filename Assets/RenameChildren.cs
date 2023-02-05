using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenameChildren : MonoBehaviour
{
	
	void Reset()
	{
		RenameAndChild(this.transform);
	}
	
	public void RenameAndChild(Transform trans)
	{
		
		for(int i =0 ; i< trans.childCount; i++)
		{
			trans.GetChild(i).name = trans.GetChild(i).name.Replace("SM_Prop_","");
			trans.GetChild(i).name = trans.GetChild(i).name.Replace("SM_Bld_","");
			trans.GetChild(i).name = trans.GetChild(i).name.Replace("SM_Env_","");
			trans.GetChild(i).name = trans.GetChild(i).name.Replace("(1)","");
			trans.GetChild(i).name = trans.GetChild(i).name.Replace("(2)","");
			trans.GetChild(i).name = trans.GetChild(i).name.Replace("(3)","");
			trans.GetChild(i).name = trans.GetChild(i).name.Replace("(4)","");
			trans.GetChild(i).name = trans.GetChild(i).name.Replace("(5)","");
			trans.GetChild(i).name = trans.GetChild(i).name.Replace("_Combined","");
			trans.GetChild(i).name = trans.GetChild(i).name.Replace("_01","");
			trans.GetChild(i).name = trans.GetChild(i).name.Replace("_02","");
			trans.GetChild(i).name = trans.GetChild(i).name.Replace("_03","");
			trans.GetChild(i).name = trans.GetChild(i).name.Replace("_04","");
			trans.GetChild(i).name = trans.GetChild(i).name.Replace("_05","");
			RenameAndChild(trans.GetChild(i));
		}
	}
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

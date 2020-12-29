using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenManager : MonoBehaviour
{
    private MotionTweenObject[] allBasicTweenObjs;

	// Use this for initialization
	void Start ()
    {
       allBasicTweenObjs = gameObject.GetComponentsInChildren<MotionTweenObject>(true);
	}
	
    public void setNewTween(float maxProg, GameObject startPoint, GameObject endPoint, Sprite sprite)
    {
        for (int i = 0; i < allBasicTweenObjs.Length; i++)
        {
            if (!allBasicTweenObjs[i].isActive())
            {
                allBasicTweenObjs[i].setNewTween(maxProg, startPoint, endPoint, sprite);
                break;
            }
        }
    }

	// Update is called once per frame
	void Update ()
    {
		
	}
}

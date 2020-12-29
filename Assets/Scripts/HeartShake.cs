using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartShake : MonoBehaviour
{
    private Vector3 startingPoint;
    private float shakeOffset = -5f;

    private float progress;
    private float maxProgress;
    private bool travelLeft;
    private int shakeNo;
    private bool canShake;
    RectTransform rectTransform;

    public void startShake()
    {
        canShake = true;
    }

	// Use this for initialization
	void Start ()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        startingPoint = rectTransform.localPosition;
        shakeNo = 0;
        progress = 0f;
        maxProgress = 0.1f;
        travelLeft = true;
        canShake = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (canShake)
        {
            if (progress < maxProgress)
                progress += Time.deltaTime;
            else
            {
                progress = maxProgress;

                travelLeft = !travelLeft;
                shakeNo += 1;
            }

            if (shakeNo >= 8)
            {
                rectTransform.localPosition = startingPoint;
                canShake = false;
            }
            else
            {
                rectTransform.localPosition = new Vector3(
                    GameLogicFacilitator.interpolateLocation(progress, startingPoint.x, (travelLeft) ? shakeOffset : -shakeOffset)
                    , startingPoint.y, startingPoint.z);
            }
        }
	}
}

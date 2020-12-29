using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionTweenObject : MonoBehaviour
{
    public GameObject defaultPosition;
    private GameObject startPoint, endPoint;

    private SpriteRenderer spriteRenderer;
    private float progress;
    private float maxProgress;
    private bool active; public bool isActive() { return active; }

    public void setNewTween(float maxProg, GameObject startPoint, GameObject endPoint, Sprite sprite)
    {
        maxProgress = maxProg;
        progress = 0;
        this.startPoint = startPoint;
        this.endPoint = endPoint;
        spriteRenderer.sprite = sprite;
        active = true;
        gameObject.SetActive(true);
    }

	// Use this for initialization
	void Start ()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        active = false;
        transform.localPosition = defaultPosition.transform.position;//using position to get location of parent as well
        gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (active)
        {
            progress += Time.deltaTime;

            gameObject.transform.position = GameLogicFacilitator.interpolateLocation(GameLogicFacilitator.progressToPercent(progress, maxProgress)
                , startPoint.transform.position, endPoint.transform.position);

            if (progress >= maxProgress)
            {
                active = false;
                gameObject.SetActive(false);
                transform.localPosition = defaultPosition.transform.position;
            }
        }
	}
}

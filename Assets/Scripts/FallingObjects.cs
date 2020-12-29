using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjects : MonoBehaviour
{
    public GameObject rotationPoint;
    public GameObject[] fallOnHealth2;
    public GameObject[] fallOnHealth1;

    private GameManager gameManager;

	// Use this for initialization
	void Start ()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gameManager.getHealth() < 2)
        {
            updateBackground(fallOnHealth1, false);
            updateBackground(fallOnHealth2, false);
        }
        else if (gameManager.getHealth() < 3)
        {
            updateBackground(fallOnHealth2, false);
        }
	}

    private void updateBackground(GameObject[] objs, bool up)
    {
        for (int i = 0; i < objs.Length; i++)
        {
            if (!up)
            {
                objs[i].transform.localRotation = rotationPoint.transform.localRotation;
            }
            else
            {

            }
        }
    }
}

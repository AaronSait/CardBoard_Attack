using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{

    public VideoPlayer vP;
    public VideoClip vC;
    bool HasPlayed = false;
    float cd = 0.5f, cdLeft;
    int count = 0;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (cdLeft <= 0)
        {
            cdLeft = cd;
            vP.Play();
            count++;
            if (count == 5)
                HasPlayed = true;
        }
        else
        {
            cdLeft -= Time.deltaTime;
        }
           
        if (vP.isPlaying == false && HasPlayed)
            SceneManager.LoadScene(1);
    }
}

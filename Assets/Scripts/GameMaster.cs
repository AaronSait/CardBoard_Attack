using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instanc = null;
    public float mainVol = .5f, sfxVol = .5f;

    private void Awake()
    {
        if (instanc == null)
        {
            instanc = this;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(this);
        }
        this.GetComponent<SoundController>().MainVolChanged(mainVol);
        this.GetComponent<SoundController>().SFXVolChanged(sfxVol);
    }
    


    public void SetMainVol(float x)
    {
        mainVol = x;
        this.GetComponent<SoundController>().MainVolChanged(x);
    }
    public float GetMainVol()
    {
        return mainVol;
    }
    public void SetSFXVol(float x)
    {
        sfxVol = x;
        this.GetComponent<SoundController>().SFXVolChanged(x);
    }
    public float GetSFXVol()
    {
        return sfxVol;
    }
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    
}

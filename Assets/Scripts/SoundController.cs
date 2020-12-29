using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController instanc = null;


    public AudioSource ButtonClick, Orb1, Orb2, Orb3, Orb4, EnemyAttack, EnemyKilled, PlayerAttack;
    public AudioSource HPLoss, PanickBar, MainTheam, Trash;




    private void Awake()
    {
        if (instanc == null)
        {
            instanc = this;
            DontDestroyOnLoad(transform.gameObject);
            MainTheam.Play();
        }
        else
        {
            Destroy(this);
        }
    }


    public void PlayMainTheam(AudioClip x)
    {
        MainTheam.clip = x;
    }

    public void PlayButtonClick(AudioClip x)
    {
        //if (ButtonClick.isPlaying == false)
        //{
            ButtonClick.clip = x;
            ButtonClick.Play();
        //}
    }

    public void PlayOrb1(AudioClip x)
    {
        //if (Orb1.isPlaying == false)
        //{
            Orb1.clip = x;
            Orb1.Play();
        //}
    }

    public void PlayOrb2(AudioClip x)
    {
        //if (Orb2.isPlaying == false)
        //{
            Orb2.clip = x;
            Orb2.Play();
        //}
    }

    public void PlayOrb3(AudioClip x)
    {
        //if (Orb3.isPlaying == false)
        //{
            Orb3.clip = x;
            Orb3.Play();
        //}
    }

    public void PlayOrb4(AudioClip x)
    {
        //if (Orb4.isPlaying == false)
        //{
            Orb4.clip = x;
            Orb4.Play();
        //}
    }

    public void PlayEnemyAttack(AudioClip x)
    {
        //if (EnemyAttack.isPlaying == false)
        //{
            EnemyAttack.clip = x;
            EnemyAttack.Play();
        //}
    }

    public void PlayEnemyKilled(AudioClip x)
    {
        //if (EnemyKilled.isPlaying == false)
        //{
            EnemyKilled.clip = x;
            EnemyKilled.Play();
        //}
    }

    public void PlayPlayerAttack(AudioClip x)
    {
        //if (PlayerAttack.isPlaying == false)
        //{
            PlayerAttack.clip = x;
            PlayerAttack.Play();
        //}
    }

    public void PlayHPLoss(AudioClip x)
    {
        //if (HPLoss.isPlaying == false)
        //{
            HPLoss.clip = x;
            HPLoss.Play();
        //}
    }

    public void PlayPanickBar(AudioClip x)
    {
        Debug.Log("Panic Bar Sound Called");
        Debug.Log(x.name);
        if (PanickBar.isPlaying == false)
        {
            PanickBar.clip = x;
            PanickBar.Play();
        }
    }
    public void PlayTrash(AudioClip x)
    {
        //if (Trash.isPlaying == false)
        //{
            Trash.clip = x;
            Trash.Play();
        //}
    }


    public void SFXVolChanged (float x)
    {
        ButtonClick.volume = x;
        Orb1.volume = x;
        Orb2.volume = x;
        Orb3.volume = x;
        Orb4.volume = x;
        EnemyAttack.volume = x;
        EnemyKilled.volume = x;
        PlayerAttack.volume = x;
        HPLoss.volume = x;
        PanickBar.volume = x;
        Trash.volume = x;
    }


    public void MainVolChanged(float x)
    {
        MainTheam.volume = x;
    }


}

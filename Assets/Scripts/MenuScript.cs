using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using UnityEngine.Video;

public class MenuScript : MonoBehaviour
{
    bool Play, Exit, HowTo, Settings, Back, Pause, Resume, Paused, clicked, continueOn;
    float mainVol, sfxVol;
    public GameObject MainMenuPan, HowToPan, SettingsPan, HowToClonePan, PausePan;
    float PlayTimerCD = 3.0f, PlayTimerCDLeft;
    public Slider mainVolSlider, sfxVolSlider;
    public Text mainVolTxt, sfxVolTxt, PauseScore;
    GameManager Gm;
    public AudioClip ButtonClick, PlayClick;
    public Text[] hsName, hsScore, hsChain;


    public VideoPlayer vP;

    // Use this for initialization
    void Start ()
    {
        Play = false;
        Exit = false;
        HowTo = false;
        Settings = false;
        Back = false;
        Pause = false;
        Resume = false;
        Paused = false;
        clicked = false;
        continueOn = false;

        PlayTimerCDLeft = PlayTimerCD;
        if (mainVolSlider != null)
        {
            if (GameObject.Find("GameManager") != null)
            {
                Gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            }

            mainVol = GameMaster.instanc.GetComponent<GameMaster>().GetMainVol();
            sfxVol = GameMaster.instanc.GetComponent<GameMaster>().GetSFXVol();
            mainVolSlider.value = mainVol;
            sfxVolSlider.value = sfxVol;
            float mVol = mainVol * 100;
            float sVol = sfxVol * 100;
            mainVolTxt.text = "Main Volume: " + GameLogicFacilitator.convertIntToFont((int)mVol) + "%";
            sfxVolTxt.text = "SFX Volume: " + GameLogicFacilitator.convertIntToFont((int)sVol) + "%";
            mainVolSlider.onValueChanged.AddListener(delegate { mainVolchange(); });

            sfxVolSlider.onValueChanged.AddListener(delegate { sfxVolchange(); });
        }

        /*
         * Setting the High Score Table
         * 1) Get Player Name
         * 2) Get Player Score
         * 
         * Player Data 
         * ScoreManager 1 el 1 place
         */
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            PlayerData[] allScores = ScoreManager.getHighScores();
            for (int i = 0; i < allScores.Length; i++)
            {
                hsName[i].text = "" + allScores[i].getName();
                hsScore[i].text = "" + GameLogicFacilitator.convertIntToFont(allScores[i].getScore());
                hsChain[i].text = "X" + GameLogicFacilitator.convertIntToFont(allScores[i].getHighestChain());
            }
        }

        

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Play)
        {
            if (clicked)
                SoundController.instanc.GetComponent<SoundController>().PlayButtonClick(PlayClick);
            Time.timeScale = 1.0f;
            MainMenuPan.SetActive(false);
            if(PlayTimerCDLeft <= 0 || continueOn)
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                HowToClonePan.SetActive(true);
                PlayTimerCDLeft -= Time.deltaTime;
            }
            clicked = false;
        }

        if (HowTo)
        {
            MainMenuPan.SetActive(false);
            HowToPan.SetActive(true);
            HowTo = false;
            SoundController.instanc.GetComponent<SoundController>().PlayButtonClick(ButtonClick);
        }

        if (Settings)
        {
            if (Paused)
            {
                PausePan.SetActive(false);
                SettingsPan.SetActive(true);
                Settings = false;
                SoundController.instanc.GetComponent<SoundController>().PlayButtonClick(ButtonClick);
            }
            else
            {
                SettingsPan.SetActive(true);
                MainMenuPan.SetActive(false);
                Settings = false;
                SoundController.instanc.GetComponent<SoundController>().PlayButtonClick(ButtonClick);
            }
        }

        if (Back)
        {
            if (Paused)
            {
                PausePan.SetActive(true);
                SettingsPan.SetActive(false);
                Back = false;
                Settings = false;

                SoundController.instanc.GetComponent<SoundController>().PlayButtonClick(ButtonClick);
            }
            else
            {
                SettingsPan.SetActive(false);
                HowToPan.SetActive(false);
                MainMenuPan.SetActive(true);
                Back = false;
                Settings = false;
                HowTo = false;

                SoundController.instanc.GetComponent<SoundController>().PlayButtonClick(ButtonClick);
            }
        }

        if (Pause)
        {
            Time.timeScale = 0.0f;
            PausePan.SetActive(true);
            Paused = true;
            PauseScore.text = "" + GameLogicFacilitator.convertIntToFont((int)Gm.getScore());
            Pause = false;
            SoundController.instanc.GetComponent<SoundController>().PlayButtonClick(ButtonClick);
        }

        if (Exit)
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(0);

            SoundController.instanc.GetComponent<SoundController>().PlayButtonClick(ButtonClick);
        }

        if (Resume)
        {
            Time.timeScale = 1.0f;
            Paused = false;
            PausePan.SetActive(false);
            Resume = false;
            SoundController.instanc.GetComponent<SoundController>().PlayButtonClick(ButtonClick);

        }
    }

    public void PlayBtn()
    {
        Debug.Log("PLAY");
        Play = true;
        clicked = true;
    }

    public void HowToBtn()
    {
        HowTo = true;
    }
    public void ExitBtn()
    {
        Exit = true;
    }
    public void ResumeBtn()
    {
        Resume = true;
    }

    public void SettingsBtn()
    {
        Settings = true;
    }

    public void BackBtn()
    {
        Back = true;
    }

    public void PauseBtn()
    {
        Pause = true;
    }
    void mainVolchange()
    {
        mainVol = mainVolSlider.value;
        GameMaster.instanc.GetComponent<GameMaster>().SetMainVol(mainVol);
        mainVolTxt.text = "" + mainVol * 100;
        float mVol = mainVol * 100;
        mainVolTxt.text = "Main Volume: " + GameLogicFacilitator.convertIntToFont((int)mVol) + "%";
    }

    void sfxVolchange()
    {
        sfxVol = sfxVolSlider.value;
        GameMaster.instanc.GetComponent<GameMaster>().SetSFXVol(sfxVol);
        sfxVolTxt.text = "" + sfxVol * 100;
        float sVol = sfxVol * 100;
        sfxVolTxt.text = "SFX Volume: " + GameLogicFacilitator.convertIntToFont((int)sVol) + "%";
    }

    public void PlayVod()
    {
        vP.Play();
    }

    public void conBtn()
    {

        continueOn = true;
    }
}

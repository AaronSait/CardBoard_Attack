using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public static int getLastScore()
    {
        return PlayerPrefs.GetInt("score last", 0);
    }
    public static void setLastScore(int score)
    {
        PlayerPrefs.SetInt("score last", score);
    }

    public static bool tryToAddNewScore(PlayerData playerData)
    {
        bool success = false;
        PlayerData[] allScores = getHighScores();
        PlayerData[] finalScores = new PlayerData[allScores.Length];
        PlayerData tempPD;
        for (int i = 0; i <  allScores.Length; i++)
        {
            //replace lowest score that this beats
            if (playerData.getScore() >= allScores[i].getScore())
            {
                tempPD = allScores[i];
                finalScores[i] = playerData;//set this score here
                playerData = tempPD;//start testing for th next lowest score
                success = true;
            }
            else
            {
                finalScores[i] = allScores[i];
            }
        }
        saveHighScores(finalScores);
        return success;
    }

    public static PlayerData[] getHighScores()
    {
        PlayerData[] allScores = new PlayerData[5];

        for (int i = 0; i < allScores.Length; i++)
        {
            allScores[i] = new PlayerData(
            PlayerPrefs.GetString("hscore" + i + "name", ""),
            PlayerPrefs.GetInt("hscore" + i + "score", 0),
            PlayerPrefs.GetInt("hscore" + i + "chain", 0));
        }
        return allScores;
    }
    public static void saveHighScores(PlayerData[] scores)
    {
        for (int i = 0; i < scores.Length; i++)
        {
            PlayerPrefs.SetString("hscore" + i + "name" , scores[i].getName());
            PlayerPrefs.SetInt("hscore" + i + "score", scores[i].getScore());
            PlayerPrefs.SetInt("hscore" + i + "chain", scores[i].getHighestChain());
        }
    }
}

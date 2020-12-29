using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Text chainText;
    public Image[] heartImages;
    public SpriteRenderer[] requiredElementsArray;
    public SpriteRenderer[] combinedElementsArray;
    public Image enemyTimerBar, enemyTimerBackground;
    public GameObject endPanel;
    public Text endPanel_nameInput;
    public Text endPanel_scoreOutput;
    public Text endPanel_chainOutput;
    public Button endPanel_enterButton;
    public MenuScript menuScript;
    public AttackOrbTween attackOrbTween;
    public TweenManager tweenManager;
    public GameObject point_redOrb;
    public GameObject point_greenOrb;
    public GameObject point_blueOrb;
    public GameObject point_yellowOrb;
    public GameObject point_centreOrb;
    public Image chainBackground;
    public HeartShake[] heartShakes;

    private int score; public int getScore() { return score; } public void addToScore(int a) { score += a; }
    private int chainNo; public int getChain() { return chainNo; } public void resetChain() { chainNo = 0; }
    private int highestChain;
    private int health; public int getHealth() { return health; }
    private int numberOfFoesDefeated; public int getNumberOfFoesDefeated() { return numberOfFoesDefeated; } public void foeDefeated() { numberOfFoesDefeated++; }

    private int finalChain = 0, finalScore = 0;

    private AssetManager assetManager;
    public Enemy enemy;
    public ComboSpell comboSpell;
    public AudioClip playerAttack, enemyAttack1, enemyAttack2, enemyAttack3, hpLoss, enemyDeath, timerSlow, timerMadium, timerFast;
    AudioClip[] EnemyAttackArray;
    AudioClip enemyAttack;


    bool gameEnd;

    // Use this for initialization
    void Start ()
    {
        gameEnd = false;
        EnemyAttackArray = new AudioClip[3];
        EnemyAttackArray[0] = enemyAttack1;
        EnemyAttackArray[1] = enemyAttack2;
        EnemyAttackArray[2] = enemyAttack3;
        assetManager = GameObject.Find("AssetManager").GetComponent<AssetManager>();
        score = 0;
        chainNo = 0;
        health = 3;
        numberOfFoesDefeated = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (health <= 0)
        {
            endGame();
        }

        //update values relevant to the player
        scoreText.text = "" + GameLogicFacilitator.convertIntToFont(score);
        string str = GameLogicFacilitator.convertIntToFont(chainNo);
        if (chainNo < 16)
            chainText.text = "CHAIN X" + ((str.Length == 1)? ("9" + str) : str);
        else
            chainText.text = "MAX CHAIN!";
        if (chainNo < assetManager.chainBackgrounds.Length)
            chainBackground.sprite = assetManager.chainBackgrounds[chainNo];

        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < health)
            {
                heartImages[i].sprite = assetManager.fullHeart;
            }
            else//for heart that should be empty
            {
                heartImages[i].sprite = assetManager.emptyHeart;
            }
        }

        //update enemy's required types outputs
        for (int i = 0; i < enemy.getRequiredTypes().Length; i++)
        {
            requiredElementsArray[i].sprite = assetManager.getSpriteFromEnum(enemy.getRequiredTypes()[i]);
        }

        //update spell's combined types outputs
        for (int i = 0; i < comboSpell.getContainedTypes().Length; i++)
        {
            combinedElementsArray[i].sprite = assetManager.getSpriteFromEnum(comboSpell.getContainedTypes()[i]);
        }

        float timePercent = GameLogicFacilitator.progressToPercent(enemy.getTimer(), enemy.getMaxTimer());
        enemyTimerBar.rectTransform.sizeDelta = new Vector2(enemyTimerBar.rectTransform.sizeDelta.x, 
            GameLogicFacilitator.interpolateLocation(timePercent, 0, enemyTimerBackground.rectTransform.sizeDelta.y));
        if (timePercent > 0.6f)
        {
            Debug.Log(timerSlow.name);
            SoundController.instanc.GetComponent<SoundController>().PlayPanickBar(timerSlow);
            enemyTimerBar.color = Color.green;
        }
        else if (timePercent > 0.3f)
        {
            Debug.Log(timerMadium.name);
            SoundController.instanc.GetComponent<SoundController>().PlayPanickBar(timerMadium);
            enemyTimerBar.color = Color.yellow;
        }
        else
        {
            Debug.Log(timerFast.name);
            if (!gameEnd)
                SoundController.instanc.GetComponent<SoundController>().PlayPanickBar(timerFast);
            enemyTimerBar.color = Color.red;
        }
    }

    public void testSpellOnEnemy(bool inEditor)
    {
        bool success = true;
        for (int i = 0; i < enemy.getRequiredTypes().Length; i++)
        {
            if (enemy.getRequiredTypes()[i] != comboSpell.getContainedTypes()[i])
            {
                success = false;
                break;
            }
        }

        comboSpell.resetSpell();

        if (success || inEditor)
        {
            SoundController.instanc.GetComponent<SoundController>().PlayPlayerAttack(playerAttack);
            if (chainNo < 16)
                chainNo++;
            if (chainNo > highestChain)
                highestChain = chainNo;
            enemy.setState(GameSupport.EnemyState.BeingAttacked);//to pause timer whilst attacking
            attackOrbTween.setNewTween(0.25f);
        }
        else
        {
            int num = Random.Range(0, 2);
            enemyAttack = EnemyAttackArray[num];
            SoundController.instanc.GetComponent<SoundController>().PlayEnemyAttack(enemyAttack);
            SoundController.instanc.GetComponent<SoundController>().PlayHPLoss(hpLoss);
            health--;
            chainNo = 0;
            heartShakes[health].startShake();
        }
    }

    public void whenEnemyHit()
    {
        enemy.spriteRenderer.sprite = assetManager.defeatedEnemies[enemy.getEnemyID()];
        addToScore(GameLogicFacilitator.calculateScore(chainNo));
        foeDefeated();
        SoundController.instanc.GetComponent<SoundController>().PlayEnemyKilled(enemyDeath);
    }

    public void endGame()
    {
        gameEnd = true;
        Debug.Log("You died!");
        Time.timeScale = 0.0f;
        ScoreManager.setLastScore(score);
        finalChain = highestChain;
        finalScore = score;
        string str = GameLogicFacilitator.convertIntToFont(finalChain);
        endPanel_chainOutput.text = "BIGGEST CHAIN: " + ((str.Length == 1) ? ("9" + str) : str);
        endPanel_scoreOutput.text = "YOU SCORED: " + GameLogicFacilitator.convertIntToFont(finalScore);
        endPanel.SetActive(true);
    }

    public void endGameEnterData()
    {
        if (!endPanel_nameInput.text.Equals(""))
        {
            ScoreManager.tryToAddNewScore(new PlayerData(endPanel_nameInput.text, finalScore, finalChain));
            endPanel_enterButton.enabled = false;
        }
    }

    public void replayGame()
    {
        Time.timeScale = 1.0f;

        SceneManager.LoadScene(1);//reload this scene
    }

    public void setTweenForOrbs(GameSupport.ElementType type)
    {
        switch (type)
        {
            case GameSupport.ElementType.Blue:
                tweenManager.setNewTween(0.1f, point_blueOrb, point_centreOrb, assetManager.getSpriteFromEnum(type));
                break;

            case GameSupport.ElementType.Red:
                tweenManager.setNewTween(0.1f, point_redOrb, point_centreOrb, assetManager.getSpriteFromEnum(type));
                break;

            case GameSupport.ElementType.Green:
                tweenManager.setNewTween(0.1f, point_greenOrb, point_centreOrb, assetManager.getSpriteFromEnum(type));
                break;

            case GameSupport.ElementType.Yellow:
                tweenManager.setNewTween(0.1f, point_yellowOrb, point_centreOrb, assetManager.getSpriteFromEnum(type));
                break;

            default:// GameSupport.ElementType.NULL:
                //tweenManager.setNewTween(0.5f, point_centreOrb, point_centreOrb, assetManager.getSpriteFromEnum(type));
                break;
        }
    }
}

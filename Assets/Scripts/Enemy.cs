using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject enterExitPoint, activePoint;

    private GameSupport.ElementType[] requiredTypes; public GameSupport.ElementType[] getRequiredTypes() { return requiredTypes; }
    private GameSupport.EnemyState state; public GameSupport.EnemyState getState() { return state; } public void setState(GameSupport.EnemyState es) { state = es; } 

    private float timer; public float getTimer() { return timer; } //public void setTimer(float t) { timer = t; }
    private float maxTimer = 1f; public float getMaxTimer() { return maxTimer; } public void resetMaxTimer(float t) { maxTimer = t; timer = t; }
    private GameManager gameManager;

    public SpriteRenderer spriteRenderer;
    private AssetManager assetManager;

    private float progress;
    private float maxProgress = 1f;

    private int enemyID; public int getEnemyID() { return enemyID; } public void setEnemyID(int id) { enemyID = id; }
    public void setRandomEnemyID()
    {
        enemyID = Random.Range(0, assetManager.activeEnemies.Length);
    }

	// Use this for initialization
	void Start ()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        assetManager = GameObject.Find("AssetManager").GetComponent<AssetManager>();
        requiredTypes = new GameSupport.ElementType[4];
        state = new GameSupport.EnemyState();
        progress = 0f;
        resetEnemy();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (state == GameSupport.EnemyState.Active)
        {
            progress = 0;
            if (timer > 0f)
                timer -= Time.deltaTime;
            else
                gameManager.endGame();
        }
        else if (state != GameSupport.EnemyState.BeingAttacked)//allow a pause if it is being attacked
        { 
            progress += Time.deltaTime;
            if (progress >= maxProgress)
                progress = maxProgress;
        }
        //move/interpolate enemy depending on its state
        if (state == GameSupport.EnemyState.Entering)
        {
            gameObject.transform.localPosition = GameLogicFacilitator.interpolateLocation(GameLogicFacilitator.progressToPercent(progress, maxProgress), enterExitPoint.transform.localPosition, activePoint.transform.localPosition);
            if (progress >= maxProgress)//when interpolation has been completed
            {
                state = GameSupport.EnemyState.Active;
                progress = 0;

                //auto play code - only run in editor
#if UNITY_EDITOR
                //gameManager.testSpellOnEnemy(true);
#endif
            }
        }
        else if (state == GameSupport.EnemyState.Leaving)
        {
            gameObject.transform.localPosition = GameLogicFacilitator.interpolateLocation(GameLogicFacilitator.progressToPercent(progress, maxProgress), activePoint.transform.localPosition, enterExitPoint.transform.localPosition);
            if (progress >= maxProgress)//when interpolation has been completed
            {
                //create new enemy over old one
                resetEnemy();
                state = GameSupport.EnemyState.Entering;
                progress = 0;
            }
        }
        else//whilst active or about to leave
        {

        }
    }

    public void resetEnemy()
    {
        for (int i = 0; i < requiredTypes.Length; i++)
        {
            requiredTypes[i] = GameSupport.ElementType.NULL;
        }
        state = GameSupport.EnemyState.Entering;
        timer = 0;
        progress = 0f;

        GameLogicFacilitator.generateEnemy(gameManager.getNumberOfFoesDefeated(), this);
        spriteRenderer.sprite = assetManager.activeEnemies[enemyID];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOrbTween : MonoBehaviour
{
    public SpriteRenderer characterSpriteRenderer;

    private GameManager gameManager;
    private AssetManager assetManager;
    private SpriteRenderer spriteRenderer; //public void setSprite(Sprite sprite) { spriteRenderer.sprite = sprite; }
    public GameObject startPoint;
    public GameObject endPoint;
    private float progress;
    private float maxProgress;
    private bool active;

    public void setNewTween(float maxProg)
    {
        maxProgress = maxProg;
        progress = 0;
        active = true;
        gameObject.SetActive(true);
        characterSpriteRenderer.sprite = assetManager.playerCharacterAttacking;
    }

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        assetManager = GameObject.Find("AssetManager").GetComponent<AssetManager>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        active = false;
        transform.localPosition = startPoint.transform.localPosition;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            progress += Time.deltaTime;

            transform.localPosition = GameLogicFacilitator.interpolateLocation(GameLogicFacilitator.progressToPercent(progress, maxProgress)
                , startPoint.transform.localPosition, endPoint.transform.localPosition);

            if (progress >= maxProgress)
            {
                gameManager.enemy.setState(GameSupport.EnemyState.Leaving);
                gameManager.whenEnemyHit();
                active = false;
                transform.localPosition = startPoint.transform.localPosition;
                gameObject.SetActive(false);
                characterSpriteRenderer.sprite = assetManager.playerCharacterNormal;
            }
        }
    }
}

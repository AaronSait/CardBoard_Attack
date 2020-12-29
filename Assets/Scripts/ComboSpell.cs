using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSpell : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private GameSupport.ElementType[] containedTypes; public GameSupport.ElementType[] getContainedTypes() { return containedTypes; }
    private GameManager gameManager;
    private AssetManager assetManager;

    // Use this for initialization
    void Start ()
    {
        containedTypes = new GameSupport.ElementType[4];
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        assetManager = GameObject.Find("AssetManager").GetComponent<AssetManager>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void addToSpell(GameSupport.ElementType el)
    {
        for (int i = 0; i < containedTypes.Length; i++)
        {
            //only add over a blank space
            if (containedTypes[i] == GameSupport.ElementType.NULL)
            {
                spriteRenderer.sprite = assetManager.readyAttack;
                containedTypes[i] = el;
                gameManager.setTweenForOrbs(el);//request a motion tween start
                break;
            }
        }
    }

    public void resetSpell()
    {
        spriteRenderer.sprite = assetManager.normalAttack;
        for (int i = 0; i < containedTypes.Length; i++)
        {
            containedTypes[i] = GameSupport.ElementType.NULL;
        }
    }
}

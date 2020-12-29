using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssetManager : MonoBehaviour
{
    public Sprite fullHeart, emptyHeart;
    public Sprite elementRed, elementBlue, elementGreen, elementYellow, elementNULL;
    public Sprite selectedElementRed, selectedElementBlue, selectedElementGreen, selectedElementYellow;
    public Sprite normalAttack, readyAttack;
    public Sprite attackSprite;
    public Sprite trashOrb, selectedTrashOrb;
    public Sprite playerCharacterNormal, playerCharacterAttacking;

    public Sprite[] activeEnemies;
    public Sprite[] defeatedEnemies;
    public Sprite[] chainBackgrounds;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public Sprite getSpriteFromEnum(GameSupport.ElementType el)
    {
        switch (el)
        {
            case GameSupport.ElementType.Blue:
                return elementBlue;

            case GameSupport.ElementType.Red:
                return elementRed;

            case GameSupport.ElementType.Green:
                return elementGreen;

            case GameSupport.ElementType.Yellow:
                return elementYellow;

            default:// GameSupport.ElementType.NULL
                return elementNULL;
        }
    }

    public Sprite getSelectedSpriteFromEnum(GameSupport.ElementType el)
    {
        switch (el)
        {
            case GameSupport.ElementType.Blue:
                return selectedElementBlue;

            case GameSupport.ElementType.Red:
                return selectedElementRed;

            case GameSupport.ElementType.Green:
                return selectedElementGreen;

            case GameSupport.ElementType.Yellow:
                return selectedElementYellow;

            default:// GameSupport.ElementType.NULL
                return elementNULL;
        }
    }
}

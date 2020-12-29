using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndTap : MonoBehaviour
{
    private GameManager gameManager;

    public AudioClip Orb1, Orb2, Orb3, Orb4, Attack, Trash;
    public Sprite Fire1, Fire2, ActivOrb1, ActivOrb2, ActivOrb3, ActivOrb4, sOrb1, sOrb2, sOrb3, sOrb4, sTrash, ActiveTrash;
    float countDown = 0.5f, countDownLeft = 0;
    public GameObject bOrb, rOrb, yOrb, gOrb, fOrb, tOrb;
    private bool pressedFireSpellButton;
    // Use this for initialization
    void Start ()
    {
        pressedFireSpellButton = false;
        Time.timeScale = 1.0f;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        tapOrb();
        if (countDownLeft <= 0)
        {
            countDownLeft = countDown;
            bOrb.GetComponent<SpriteRenderer>().sprite = sOrb1;
            rOrb.GetComponent<SpriteRenderer>().sprite = sOrb2;
            gOrb.GetComponent<SpriteRenderer>().sprite = sOrb3;
            yOrb.GetComponent<SpriteRenderer>().sprite = sOrb4;
            tOrb.GetComponent<SpriteRenderer>().sprite = sTrash;
            if (pressedFireSpellButton)
            {
                fOrb.GetComponent<SpriteRenderer>().sprite = Fire1;
                pressedFireSpellButton = false;
            }
        }
        else
        {
            countDownLeft -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("red");
            gameManager.comboSpell.addToSpell(GameSupport.ElementType.Red);
            //hitRay.collider.gameObject.GetComponent<SpriteRenderer>().sprite = ActivOrb2;
            //SoundController.instanc.GetComponent<SoundController>().PlayButtonClick(Orb2);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            gameManager.testSpellOnEnemy(false);
            pressedFireSpellButton = true;
        }
	}

    void tapOrb()
    {
        if (Time.timeScale != 0f)
        {
            Vector3 tapStart = new Vector3(0, 0, 0), tapEnd = new Vector3(0, 0, 0);
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit hitRay;
                    if (Physics.Raycast(ray, out hitRay))
                    {
                        if (hitRay.collider.gameObject.tag == "YellowOrb")
                        {
                            Debug.Log("YELLOW ORB");
                            gameManager.comboSpell.addToSpell(GameSupport.ElementType.Yellow);
                            hitRay.collider.gameObject.GetComponent<SpriteRenderer>().sprite = ActivOrb4;
                            SoundController.instanc.GetComponent<SoundController>().PlayButtonClick(Orb4);
                        }
                        else if (hitRay.collider.gameObject.tag == "BlueOrb")
                        {
                            Debug.Log("BLUE ORB");
                            gameManager.comboSpell.addToSpell(GameSupport.ElementType.Blue);
                            hitRay.collider.gameObject.GetComponent<SpriteRenderer>().sprite = ActivOrb1;
                            SoundController.instanc.GetComponent<SoundController>().PlayButtonClick(Orb1);
                        }
                        else if (hitRay.collider.gameObject.tag == "RedOrb")
                        {
                            Debug.Log("RED ORB");
                            gameManager.comboSpell.addToSpell(GameSupport.ElementType.Red);
                            hitRay.collider.gameObject.GetComponent<SpriteRenderer>().sprite = ActivOrb2;
                            SoundController.instanc.GetComponent<SoundController>().PlayButtonClick(Orb2);
                        }
                        else if (hitRay.collider.gameObject.tag == "GreenOrb")
                        {
                            Debug.Log("GREEN ORB");
                            gameManager.comboSpell.addToSpell(GameSupport.ElementType.Green);
                            hitRay.collider.gameObject.GetComponent<SpriteRenderer>().sprite = ActivOrb3;
                            SoundController.instanc.GetComponent<SoundController>().PlayButtonClick(Orb3);
                        }
                        else if (hitRay.collider.gameObject.tag == "DepositOrb")
                        {
                            Debug.Log("DEPLOY ORB");
                            gameManager.testSpellOnEnemy(false);

                            hitRay.collider.gameObject.GetComponent<SpriteRenderer>().sprite = Fire2;
                            SoundController.instanc.GetComponent<SoundController>().PlayButtonClick(Attack);
                            pressedFireSpellButton = true;
                        }
                        else if (hitRay.collider.gameObject.tag == "TrashOrb")
                        {
                            Debug.Log("TRASH ORB");
                            gameManager.comboSpell.resetSpell();
                            hitRay.collider.gameObject.GetComponent<SpriteRenderer>().sprite = ActiveTrash;
                            SoundController.instanc.GetComponent<SoundController>().PlayButtonClick(Trash);
                        }
                    }
                }
                else if (touch.phase == TouchPhase.Ended)
                {


                }
            }
        }
    }
}

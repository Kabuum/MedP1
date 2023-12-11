using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;

public class PlayerBehavior : MonoBehaviour
{
    public float speed;
    bool InteractKey = false;
    bool Hidden;
    public GameObject door;
    private Color myColor;
    private Renderer Renderer;
    float ElapsedTime = 0;
    float WaitTime = 0.5f;
    private float deltaspeed;
    private Animator PlayerAnimator;
    private int directionindex = 3;
    public bool lantern = false;
    private bool moving = false;
    public GameObject ESprite;
    public GameObject NormalMusic;
    public GameObject TenseMusic;

    private Collider2D Interactable;
    public bool canMove = true;

    public GameObject billboard;
    public GameObject poster;

    bool isOnBillBoard = false;
    bool isOnPoster = false;
    bool posterOpen = false;
    bool doorclose;
    bool isOnNote;
    bool noteOpen;
    GameObject doorObject;

    public GameObject GameManager;
    public GameObject tableLatern;
    public GameObject Yamamba;
    bool billboardOpen = false;
    public GameObject deathNote;
    public GameObject laternLight;
    public GameObject groundFloorBefore;
    public GameObject groundFloorAfter;
    public GameObject UpperfloorTools;
    public bool yamaubavisited = false;
    private bool youDied;
    public bool KeyReceived;
    private bool onDoor = false;
    private bool onTools = false;
    private bool gotTools;
    private bool onDrawer;
    private bool onBed;


    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        PlayerAnimator = gameObject.GetComponentInChildren<Animator>();
        Renderer = this.GetComponent<Renderer>();
        if (billboard != null)
        {
            billboard.SetActive(false);
        }
        if (poster != null)
        {
            poster.SetActive(false);
        }
        if (laternLight != null)
        {
            laternLight.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("DeathDone"))
        {
            //SceneMangment.LoadScene(9); //Death Scene needs to restart right level
            SceneMangment.RestartScene();
        }
        deltaspeed = speed * Time.deltaTime;
        if (billboard == null || billboard.activeInHierarchy == false)
        {
            if (canMove == true)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    moving = true;
                    directionindex = 1;
                    PlayAnim(directionindex, moving, lantern);
                    this.transform.Translate(-deltaspeed, 0f, 0f);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    moving = true;
                    directionindex = 2;
                    PlayAnim(directionindex, moving, lantern);
                    this.transform.Translate(deltaspeed, 0f, 0f);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    moving = true;
                    directionindex = 3;
                    PlayAnim(directionindex, moving, lantern);
                    this.transform.Translate(0.0f, -deltaspeed, 0);
                }
                else if (Input.GetKey(KeyCode.W))
                {
                    moving = true;
                    directionindex = 4;
                    PlayAnim(directionindex, moving, lantern);
                    this.transform.Translate(0.0f, deltaspeed, 0f);
                }
                else
                {
                    moving = false;
                    PlayAnim(directionindex, moving, lantern);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (onBed)
            {
                StartCoroutine(GameManager.GetComponent<CutSceneStuff>().Cut5("Something doesn't feel right... Maybe I should check the back door despite what that old lady said"));
            }
            if (onDrawer)
            {
                if (gotTools)
                { 
                    KeyReceived = true;
                    StartCoroutine(GameManager.GetComponent<CutSceneStuff>().Cut5("*You found a key* I should get out of here now!"));
                    groundFloorAfter.SetActive(true);
                    groundFloorBefore.SetActive(false);
                    NormalMusic.SetActive(false);
                    TenseMusic.SetActive(true);
                }
                else
                {
                    StartCoroutine(GameManager.GetComponent<CutSceneStuff>().Cut5("This drawer is locked. Maybe a tool could open it"));
                }
            }
            if (onTools)
            {
                StartCoroutine(GameManager.GetComponent<CutSceneStuff>().Cut5("These tools might be useful"));
                UpperfloorTools.SetActive(false);
                gotTools = true;
            }
            if (onDoor)
            {
                if (KeyReceived)
                {
                    SceneMangment.LoadScene(8);
                }
                else if (yamaubavisited)
                {
                    StartCoroutine(GameManager.GetComponent<CutSceneStuff>().Cut5("The door is locked. The Note said I should look upstairs"));
                }
                else
                {
                    
                    StartCoroutine(GameManager.GetComponent<CutSceneStuff>().Cut5("The door is locked..."));
                }
            }
            if (isOnBillBoard == true && billboardOpen == false)
            {
                if (billboard.activeInHierarchy == true && billboardOpen == false)
                {
                    billboard.SetActive(false);
                    Yamamba.SetActive(true);
                    StartCoroutine(GameManager.GetComponent<CutSceneStuff>().Cut1());
                    billboardOpen = true;
                }
                else { billboard.SetActive(true); }
            }
            else if (isOnPoster == true || posterOpen == true)
            {
                if (isOnPoster == true && posterOpen == false)
                {
                    poster.SetActive(true);
                    posterOpen = true;
                }
                else if (posterOpen == true)
                {
                    poster.SetActive(false);
                    posterOpen = false;
                    lantern = true;
                    Destroy(tableLatern);
                    laternLight.SetActive(true);
                }
            }
            else if (isOnNote || noteOpen)
            { 
                if (isOnNote && noteOpen == false)
                {
                    canMove = false;
                    deathNote.gameObject.SetActive(true);
                    noteOpen = true;
                }
                else if (noteOpen)
                {
                    canMove = true;
                    deathNote.gameObject.SetActive(false);
                    noteOpen = false;
                    if (yamaubavisited == false)
                    {
                        StartCoroutine(GameManager.GetComponent<CutSceneStuff>().Cut4());
                        yamaubavisited = true;
                        groundFloorBefore.SetActive(false);
                        groundFloorAfter.SetActive(true);
                    }
                }
            }

            if (WaitTime > ElapsedTime)
            { 
                ElapsedTime += Time.deltaTime;
            }
            else
            { 
                InteractKey = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bed"))
        {
            ESprite.SetActive(true);
            onBed = true;
        }
        if (coll.gameObject.CompareTag("Drawer"))
        {
            ESprite.SetActive(true);
            onDrawer = true;
        }
        if (coll.gameObject.CompareTag("Tools"))
        {
            ESprite.SetActive(true);
            onTools = true;
        }
        if (coll.gameObject.CompareTag("FrontDoor"))
        {
            onDoor = true;
            ESprite.SetActive(true);
        }
        if (coll.gameObject.CompareTag("Billboard") && billboardOpen == false)
        {
            ESprite.SetActive(true);
            isOnBillBoard = true;
        }

        if (coll.gameObject.CompareTag("DeathNote"))
        {
            ESprite.SetActive(true);
            isOnNote = true;
        }

        if (coll.gameObject.CompareTag("RevealRoom"))
        {
            coll.gameObject.SetActive(false);
        }
        if (coll.gameObject.CompareTag("MissingPoster"))
        {
            isOnPoster = true;
            ESprite.SetActive(true);
        }
        else if (coll.gameObject.CompareTag("Yamamba"))
        {
            if (coll.gameObject.GetComponent<Enemy>().dontFollow == false)
            {
                Yamamba.SetActive(false);
                youDied = true;
                canMove = false;
                PlayerAnimator.Play("Death");
            }
        }
    }
    private void OnTriggerStay2D(Collider2D coll)
    {
        if (InteractKey == true && coll.gameObject.CompareTag("Hiding Range"))
        {
            Hidden = true;
            myColor = new Color(1f, 1f, 1f, 0.2f);
            Renderer.material.color = myColor;
        }
        if (Input.GetKeyDown(KeyCode.E) && coll.gameObject.CompareTag("Doors"))
        {
            openDoor.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bed"))
        {
            ESprite.SetActive(false);
            onBed = false;
        }
        if (coll.gameObject.CompareTag("Drawer"))
        {
            ESprite.SetActive(false);
            onDrawer = false;
        }
        if (coll.gameObject.CompareTag("Tools")) 
        {
            ESprite.SetActive(false); 
            onTools = false;
        }
        if (coll.gameObject.CompareTag("FrontDoor"))
        {
            onDoor = false;
            ESprite.SetActive(false);
        }
        if (coll.gameObject.CompareTag("Billboard"))
        {
            isOnBillBoard = false;
            ESprite.SetActive(false);
        }

        if (coll.gameObject.CompareTag("HidingRange") && Hidden == true)
        {
            Hidden = false;
            myColor = new Color(1f, 1f, 1f, 1f);
            Renderer.material.color = myColor;
        }
        if (coll.gameObject.CompareTag("MissingPoster") && posterOpen == true)
        {
            isOnPoster = false;
            ESprite.SetActive(false);
            poster.SetActive(false);
            posterOpen = false;
            lantern = true;
            laternLight.SetActive(true);
            Destroy(tableLatern);
        }
        if (coll.gameObject.CompareTag("MissingPoster") && posterOpen == false)
        { 
            isOnPoster = false; 
            ESprite.SetActive(false);
        }

        if (coll.gameObject.CompareTag("DeathNote"))
        {
            isOnNote = false;
            ESprite.SetActive(false);
        }
        isOnBillBoard = false;
        isOnPoster = false;
    }
    void DestroyComponent()
    {
        Destroy(door.GetComponent<BoxCollider2D>());
        Debug.Log("Door Open");
    }

    void PlayAnim(int direction, bool isMoving, bool lanternOn)
    {
        if (!youDied)
        {
            if (isMoving == true)
            {
                if (lanternOn)
                {
                    switch (direction)
                    {
                        case 1:
                            PlayerAnimator.Play("PriestLantern-Walk-Left");
                            break;
                        case 2:
                            PlayerAnimator.Play("PriestLantern-Walk-Right");
                            break;
                        case 3:
                            PlayerAnimator.Play("PriestLantern-Walk-Down");
                            break;
                        case 4:
                            PlayerAnimator.Play("PriestLantern-Walk-Up");
                            break;
                    }
                }
                else
                {
                    switch (direction)
                    {
                        case 1:
                            PlayerAnimator.Play("Priest-Walk-Left");
                            break;
                        case 2:
                            PlayerAnimator.Play("Priest-Walk-Right");
                            break;
                        case 3:
                            PlayerAnimator.Play("Priest-Walk-Down");
                            break;
                        case 4:
                            PlayerAnimator.Play("Priest-Walk-Up");
                            break;
                    }
                }
            }
            else
            {
                if (lanternOn)
                {
                    switch (direction)
                    {
                        case 1:
                            PlayerAnimator.Play("PriestLantern-Left");
                            break;
                        case 2:
                            PlayerAnimator.Play("PriestLantern-Right");
                            break;
                        case 3:
                            PlayerAnimator.Play("PriestLantern-Down");
                            break;
                        case 4:
                            PlayerAnimator.Play("PriestLantern-Up");
                            break;
                    }
                }
                else
                {
                    switch (direction)
                    {
                        case 1:
                            PlayerAnimator.Play("Priest-Left");
                            break;
                        case 2:
                            PlayerAnimator.Play("Priest-Right");
                            break;
                        case 3:
                            PlayerAnimator.Play("Priest-Down");
                            break;
                        case 4:
                            PlayerAnimator.Play("Priest-Up");
                            break;
                    }
                }
            }
        }
    }
}
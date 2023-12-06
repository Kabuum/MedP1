using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
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
    public UnityEvent openDoor;
    public GameObject ESprite;
    private Collider2D Interactable;
    public bool canMove;


    // Start is called before the first frame update
    void Start()
    {
        PlayerAnimator = gameObject.GetComponentInChildren<Animator>();
        Renderer = this.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        deltaspeed = speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            moving = true;
            directionindex = 1;
            PlayAnim(directionindex,moving,lantern);
            this.transform.Translate(-deltaspeed, 0f, 0f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moving = true;
            directionindex = 2;
            PlayAnim(directionindex,moving,lantern);
            this.transform.Translate(deltaspeed, 0f, 0f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moving = true;
            directionindex = 3;
            PlayAnim(directionindex,moving,lantern);
            this.transform.Translate(0.0f, -deltaspeed, 0);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            moving = true;
            directionindex = 4;
            PlayAnim(directionindex,moving,lantern);
            this.transform.Translate(0.0f, deltaspeed, 0f);
        }
        else
        {
            moving = false;
            PlayAnim(directionindex,moving,lantern);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractKey = true;
            ElapsedTime = 0f;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
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

    void interact()
    {
        if (Interactable.CompareTag("Billboard"))
        {
            
        }
        else if (Interactable.CompareTag("Key1"))
        {
            
        }
        else if (Interactable.CompareTag("Key2"))
        {
            
        }
        else if (Interactable.CompareTag("Crowbar"))
        {
            
        }
        else if (Interactable.CompareTag("Hiding Range"))
        {
            
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Billboard"))
        {
            ESprite.SetActive(true);
            coll = Interactable;
        }
        else if (coll.gameObject.CompareTag("Yamamba"))

        {
            SceneMangment.RestartScene();
        }
    }
    
    private void OnTriggerStay2D(Collider2D coll)
    {
        if (InteractKey == true && coll.gameObject.CompareTag("Hiding Range"))
        {
            Hidden = true;
            myColor = new Color(1f, 1f, 1f, 0.2f);
            Debug.Log("Hidden");
            Renderer.material.color = myColor;
        }
        if (InteractKey == true && coll.gameObject.CompareTag("Doors"))
        {
            openDoor.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Billboard"))
        {
            Interactable = null;
        }
        if (coll.gameObject.CompareTag("HidingRange") && Hidden == true)
        {
            Hidden = false;
            Debug.Log("Not Hidden");
            myColor = new Color(1f, 1f, 1f, 1f);
            Renderer.material.color = myColor;
        }
    }
    void DestroyComponent()
    {
        Destroy(door.GetComponent<BoxCollider2D>());
        Debug.Log("Door Open");

    }

    void PlayAnim(int direction, bool isMoving, bool lanternOn)
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

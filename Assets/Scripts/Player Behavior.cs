using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Yamamba"))

        {
            SceneManager.RestartScene();
        }
    }

    private void OnTriggerStay2D(Collider2D Coll)
    {

        if (InteractKey == true && Coll.gameObject.CompareTag("Item1"))
        {
            Destroy(Coll.gameObject);
            Debug.Log("Item Up Picked");
            DestroyComponent();
        }
        if (InteractKey == true && Coll.gameObject.CompareTag("Hiding Range"))
        {
            Hidden = true;
            myColor = new Color(1f, 1f, 1f, 0.2f);
            Debug.Log("Hidden");
            Renderer.material.color = myColor;
        }
        if (InteractKey == true && Coll.gameObject.CompareTag("Doors"))
        {
            openDoor.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
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

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
            PlayerAnimator.Play("Priest-Walk-Left");
            this.transform.Translate(-deltaspeed, 0f, 0f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            PlayerAnimator.Play("Priest-Walk-Right");
            this.transform.Translate(deltaspeed, 0f, 0f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            PlayerAnimator.Play("Priest-Walk-Down");
            this.transform.Translate(0.0f, -deltaspeed, 0);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            PlayerAnimator.Play("Priest-Walk-Up");
            this.transform.Translate(0.0f, deltaspeed, 0f);
        }
        else
        {
            //animator parameter Moving = false;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractKey = true;
            ElapsedTime = 0f;
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
        if (Coll.gameObject.CompareTag("Doors"))
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                openDoor.Invoke();
            }
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
}

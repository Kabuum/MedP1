using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

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


    // Start is called before the first frame update
    void Start()
    {
        Renderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        deltaspeed = speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {

            this.transform.Translate(-deltaspeed, 0f, 0f);

        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(deltaspeed, 0f, 0f);

        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(0.0f, -deltaspeed, 0);

        }
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(0.0f, deltaspeed, 0f);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractKey = true;
            ElapsedTime = 0f;
        }
        if(WaitTime > ElapsedTime)
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
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Hiding Range") && Hidden == true)
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

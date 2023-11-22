using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float speed;
    bool ItemPickup = false;
    bool Hidden;
    public GameObject door;
    public Color myColor;
    public Renderer Renderer;
    // Start is called before the first frame update
    void Start()
    {
        Renderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
          
            this.transform.Translate(-speed, 0f, 0f);

        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(speed, 0f, 0f);

        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(0.0f, -speed, 0);

        }
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(0.0f, speed, 0f);
        }
    }
    
    private void OnTriggerStay2D(Collider2D Coll)
    {
        if (Input.GetKey(KeyCode.E) && Coll.gameObject.CompareTag("Item1"))
        {
            Destroy (Coll.gameObject);
            ItemPickup = true;
            Debug.Log("Item Up Picked");
            DestroyComponent();
        }
        if (Input.GetKey(KeyCode.E)&& Coll.gameObject.CompareTag("Hiding Range"))
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float speed;
    bool ItemPickup = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-speed, 0f, 0f);

        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(speed, 0f, 0f);

        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0.0f, -speed, 0);

        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0.0f, speed, 0f);
        }
    }
    private void OnTriggerStay2D(Collider2D Coll)
    {
        if (Input.GetKeyDown(KeyCode.E) && Coll.gameObject.CompareTag("Item1"))
        {
            Destroy (Coll.gameObject);
            ItemPickup = true;
            Debug.Log("Item Up Picked");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorOpenerScript : MonoBehaviour
{
    public GameObject Player;
    public GameObject Yamauba;
    public AudioSource DoorSFX;
    public GameObject Door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            Door.SetActive(false);
            DoorSFX.Play();
            this.GetComponent<TilemapRenderer>().enabled = false;
        }
        else if (coll.gameObject.CompareTag("Yamamba"))
        {
            Door.SetActive(false);
            DoorSFX.Play();
            this.GetComponent<TilemapRenderer>().enabled = false;
        }
    } 
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            Door.SetActive(true);
            this.GetComponent<TilemapRenderer>().enabled = true;
        }
        else if (coll.gameObject.CompareTag("Yamamba"))
        {
            Door.SetActive(true);
            this.GetComponent<TilemapRenderer>().enabled = true;
        }
    }
}

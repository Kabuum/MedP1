using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DoorController : MonoBehaviour
{
    public UnityEvent EnterRoom;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            EnterRoom.Invoke();
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                EnterRoom.Invoke();
                other.gameObject.SetActive(false);
            }
        }

    }

}

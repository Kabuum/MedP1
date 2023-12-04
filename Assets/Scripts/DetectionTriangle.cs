using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionTriangle : MonoBehaviour
{
    public bool playerInSight = false;
    float outOfSightTimer = 0;
    public float secToFollow = 3;
    public GameObject enemy;
    public Enemy enemyScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = enemy.transform.position;
    }

    void FixedUpdate()
    {
        if (outOfSightTimer > 0)
        {
            outOfSightTimer -= Time.deltaTime;
        }
        else
        {
            playerInSight = false;
        }
        Debug.Log(outOfSightTimer);
    }

    public bool CheckInSight()
    {
        return playerInSight;
    }

    public void UpdateDetectionAngle(int a)
    {
        transform.localEulerAngles = new Vector3(0,0,a);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInSight = true;
            outOfSightTimer = secToFollow;
        }
    }
}

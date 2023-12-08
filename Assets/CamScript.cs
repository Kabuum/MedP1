using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    public GameObject player;
    public GameObject yamuba;
    public bool followYamauba = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (followYamauba)
        {
            this.transform.position = new Vector3(yamuba.transform.position.x, yamuba.transform.position.y,-5f);
        }
        else
        {
            this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y,-5f);
        }
    }
}

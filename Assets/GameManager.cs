using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        if (SceneMangment.ActiveSceneIndex() == 1)
        {
        
            StartCoroutine(gameObject.GetComponent<CutSceneStuff>().Cut2());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
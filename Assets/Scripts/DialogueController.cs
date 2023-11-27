using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogueController : MonoBehaviour
{

    public Event DialogueEvent;
    public GameObject textBack;
    public TMP_Text textTmp;



    // Start is called before the first frame update
    void Start()
    {

        ShowDialougue("Du er pisse klam at se på. Gå din vej dig", 10);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShowDialougue(string text, int maxChar)
    {
        textTmp.text = "";
        string[] words = text.Split(" ");
        foreach (string word in words)
        {
            if (textTmp.text.Length + word.Length <= maxChar)
            {
                char[] chars = word.ToCharArray();
                foreach (char c in chars)
                {
                    textTmp.text = textTmp.text + c;
                }
                textTmp.text += " ";
            }
            if (textTmp.text.Length > maxChar)
            {
                StartCoroutine(WaitForKey(KeyCode.Space));
                textTmp.text = "";
                continue;
               
            }
           
        }
        //textTmp.maxVisibleCharacters = 5;

        //figure out length of next word. + space

        //        textTmp.maxVisibleLines = 2;
        //check next word lenght

        //check how many charcters inside box. //Figure out how many chars fit inside box.
        //   TMP_Text.textInfo.characterInfo[Index]



    }

    IEnumerator WaitForKey(KeyCode key)
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
       
    }
}

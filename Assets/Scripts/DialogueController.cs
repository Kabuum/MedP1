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
        ShowDialougue("gay");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShowDialougue(string text, int maxChar)
    {
        string a = "du grim";
        string[] words = a.Split(" ");
        char[] b = a.ToCharArray();
        foreach (string c in words)
        {  
            if (textTmp.text.Length + c.Length <= maxChar)
            {
                textTmp.text = textTmp.text + " " + c;
            }
       
        }
        textTmp.maxVisibleCharacters = 5;

        //figure out length of next word. + space

        if (textTmp.text.Length + 3 >= maxChar)
        {

        }
      

        textTmp.maxVisibleLines = 2;
        //check next word lenght

        //check how many charcters inside box. //Figure out how many chars fit inside box.
        //   TMP_Text.textInfo.characterInfo[Index]



    }
}

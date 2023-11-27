using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;

public class DialogueController : MonoBehaviour
{

    public Event DialogueEvent;
    public GameObject textBack;
    public TMP_Text textSpeech;

    int i = 0;

    // Start is called before the first frame update
    void Start()
    {

        ShowDialougue("Du klam at se på. Gå din vej dig", 10);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShowDialougue(string text, int maxChar)
    {
        textSpeech.text = "";
        string[] words = text.Split(" ");
        int i = 0;
        for (i = 0; i < words.Length; i++)
        {
            Debug.Log(textSpeech.text.Length);
            if (textSpeech.text.Length + words[i].Length <= maxChar)
            {
                char[] chars = words[i].ToCharArray();
                foreach (char c in chars)
                {
                    textSpeech.text = textSpeech.text + c;
                }
                textSpeech.text = textSpeech.text + " ";
            }
            else if (textSpeech.text.Length + words[i].Length > maxChar)
            {
                StartCoroutine(WaitForKey());
            }



        }


        /*   foreach (string word in words)
           {
               if (textTmp.text.Length + word.Length <= maxChar)
               {
                   char[] chars = word.ToCharArray();
                   foreach (char c in chars)
                   {
                       textTmp.text = textTmp.text + c;
                   }
                   textTmp.text += " ";*/
    }
    //else if (textTmp.text.Length > maxChar)
    //{
    //   Wait(6);
    // textTmp.text = "";
    //           continue;
    //      }
    // }
    //textTmp.maxVisibleCharacters = 5;

    //figure out length of next word. + space

    //        textTmp.maxVisibleLines = 2;
    //check next word lenght

    //check how many charcters inside box. //Figure out how many chars fit inside box.
    //   TMP_Text.textInfo.characterInfo[Index]

    IEnumerator WaitForKey()
    {
        Debug.Log("Du er homo");
        yield return new WaitForSeconds(5);
        Debug.Log("Sike");
        i--;
        textSpeech.text = "";


    }
}




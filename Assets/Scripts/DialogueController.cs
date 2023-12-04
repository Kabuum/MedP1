using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{

    public Event DialogueEvent;
    public GameObject textBack;
    public GameObject spacebar;
    public GameObject dialogField;
    public TMP_Text textSpeech;

    public string text;
    public int maxChar;
    public float charDelay;


    // Start is called before the first frame update
    void Start()
    {
        dialogField.SetActive(false);
        OpenDialog(text);
        
    }
    public void OpenDialog(string text)
    {
        dialogField.SetActive(true);
        spacebar.SetActive(false);
        StartCoroutine(ShowDialogue(KeyCode.Space, text, maxChar));
    }
    public IEnumerator ShowDialogue(KeyCode key, string text, int maxChar)
    {
        int i = 0;
        textSpeech.text = "";
        string[] words = text.Split(" ");
        for (i = 0; i < words.Length; i++)
        {
            Debug.Log(words[i]);
            if (textSpeech.text.Length + words[i].Length <= maxChar)
            {
                char[] chars = words[i].ToCharArray();
                foreach (char c in chars)
                {
                    textSpeech.text = textSpeech.text + c.ToString(); ;
                    yield return new WaitForSeconds(charDelay);
                }
                textSpeech.text = textSpeech.text + " ";
            }
            else if (textSpeech.text.Length + words[i].Length > maxChar)
            {
                spacebar.SetActive(true);
                yield return new WaitUntil(() => Input.GetKeyDown(key));
                textSpeech.text = "";
                i--;
                spacebar.SetActive(false);
            }
        }
        spacebar.SetActive(true);
        yield return new WaitUntil(() => Input.GetKeyDown(key));
        dialogField.SetActive(false);

    }
}





using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public GameObject spacebar;
    public GameObject dialogField;
    public TMP_Text textSpeech;

    public int maxChar;
    public bool dialogDone = false;
    public float charDelay;

    void Start()
    {
        dialogField.SetActive(false);
    }
    public void OpenDialog(string text)
    {
        dialogDone = false;
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
        dialogDone = true;
        dialogField.SetActive(false);
    }
}
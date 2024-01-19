
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
        //vi aktivere blokken så vi kan se den
        dialogField.SetActive(true);
        spacebar.SetActive(false);
        //vi kalder vores coroutine med vores text varieble fra cutscenemanageren
        StartCoroutine(ShowDialogue(KeyCode.Space, text, maxChar));
    }
    public IEnumerator ShowDialogue(KeyCode key, string text, int maxChar)
    {
        int i = 0;
        //text der bliver vist
        textSpeech.text = "";

        //tjekker efter mellemrum og sætter hvert ord i en array
        string[] words = text.Split(" ");
        for (i = 0; i < words.Length; i++)
        {
            //vi tjekker om ordet kan være der ved at se om det er mindre eller lig med maxChar
            if (textSpeech.text.Length + words[i].Length <= maxChar)
            {
                //tager ordet fra "words" og laver en array med alle bogstaverne
                char[] chars = words[i].ToCharArray();
                //skriver ordet, character efter character i en array
                foreach (char c in chars)
                {
                    textSpeech.text = textSpeech.text + c.ToString(); ;
                    yield return new WaitForSeconds(charDelay);
                }
                //Laver mellemrum inden vi går til næste ord
                textSpeech.text = textSpeech.text + " ";
            }
            //her tjekker vi hvis ordet er for lang
            else if (textSpeech.text.Length + words[i].Length > maxChar)
            {
                //vi aktivere spacebar så man kan se den
                spacebar.SetActive(true);
                //vi venter på at der bliver trykket mellemrum
                yield return new WaitUntil(() => Input.GetKeyDown(key));
                //vi tømmer variablen som bliver skrevet så der ikke står noget
                textSpeech.text = "";
                //vi går en tak tilbage i for loopet så vi skriver det ord der ikke kunne være der
                i--;
                //vi deaktivere spacebar så man ikke kan se den
                spacebar.SetActive(false);
            }
            // hvis der er flere ord går vi op igen, ellers går vi ud af for loopet
        }

        //vi aktivere spacebaren så man kan se den
        spacebar.SetActive(true);
        //vi venter på at der bliver trykket space
        yield return new WaitUntil(() => Input.GetKeyDown(key));
        //vi sætter dialoguedone til true så cutscene kan se det
        dialogDone = true;
        //vi deaktivere blokken så man ikke kan se den
        dialogField.SetActive(false);
    }
}
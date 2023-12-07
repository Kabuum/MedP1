using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class CutSceneStuff : MonoBehaviour
{
    public GameObject enemy;
    public GameObject player;
    public DialogueController dialogueController;
    public List<IEnumerator> events = new List<IEnumerator>();

    public bool isWalking;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            enemy.GetComponent<Enemy>().animated = true;
            StartCoroutine(Cut3());
        }
    }
    public IEnumerator Cut1()
    {
        //færdig
        enemy.GetComponent<Enemy>().animated = true;
        Talk("Hello Darling, the storm is rough these days and it seems to show no signs of stopping. Many people before you have ventured these paths along the mountain, only to find themselves lost in the horrid weather. It is my job to shelter these people... At the end of the road you find an old house in which I live. In there you can find warmth and a place to sleep. I'll Even prepare a delicious meal. You'll do well to watch out for anything suspicious though. There has been an increase in yokai sightings recently Follow me, I'll lead the way!", false);
        yield return new WaitWhile(() => dialogueController.dialogDone == false);
        Move(new Vector2(22.9f, -9.8f), false, false);
    }
    public IEnumerator Cut2()
    {
        enemy.GetComponent<Enemy>().animated = true;
        Move(player.transform.position + new Vector3(1, 1, 0), false, true);

        yield return new WaitForSeconds(2);
        Talk("Your Room is the second on the left. I'll bring some food at a later point, so do make yourself at home¨. But whatever you do Do Not Look through the Back Door! I'll be right back", false);
        yield return new WaitWhile(() => dialogueController.dialogDone == false);
        enemy.GetComponent<Enemy>().animated = false;
    }
    public IEnumerator Cut3()
    {
        enemy.GetComponent<Enemy>().animated = true;
        Talk("Your Room is the second on the left. I'll bring some food at a later point, so do make yourself at home¨. But whatever you do Do Not Look through the Back Door! I'll be right back", false);
        yield return new WaitWhile(() => dialogueController.dialogDone == false);
        // Move(player.transform.position + new Vector3(1, 1, 0), false, true);
        yield return new WaitForSeconds(2);
        Move(player.transform.position + new Vector3(1, 1, 0), false, false);
        enemy.GetComponent<Enemy>().animated = false;
        yield return new WaitWhile(() => isWalking == true);
        enemy.GetComponent<Enemy>().animated = true;
    }
    public IEnumerator Cut4()
    {
        Transformation(false);
        yield break;
    }
    public IEnumerator Cut5()
    {
        yield break;
    }

    public void Move(Vector2 target, bool playerMove, bool instant)
    {
        if (enemy.GetComponent<NavMeshAgent>() != null)
        {
            if (playerMove == false)
            {
                player.GetComponent<PlayerBehavior>().canMove = false;
            }
            else
            {
                player.GetComponent<PlayerBehavior>().canMove = true;
            }
            if (instant == true)
            {
                enemy.transform.position = target;
            }
            else
            {
                enemy.GetComponent<NavMeshAgent>().SetDestination(target);
            }
        }
    }
    public void Talk(string text, bool playerMove)
    {
        dialogueController.OpenDialog(text);
        enemy.GetComponent<Enemy>().animated = true;
        if (playerMove == false)
        {
            player.GetComponent<PlayerBehavior>().canMove = false;
        }
        else
        {
            player.GetComponent<PlayerBehavior>().canMove = true;
        }
    }
    public void Transformation(bool playerMove)
    {

    }
}

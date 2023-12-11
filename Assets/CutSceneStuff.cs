using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CutSceneStuff : MonoBehaviour
{
    public GameObject enemy;
    public GameObject player;
    public GameObject camera;
    public DialogueController dialogueController;
    public bool isWalking;
    public IEnumerator Cut1()
    {
        //f�rdig
        enemy.GetComponent<Enemy>().animated = true;
        Talk("Hello Darling, the storm is rough these days and it seems to show no signs of stopping. Many people before you have ventured these paths along the mountain, only to find themselves lost in the horrid weather. It is my job to shelter these people... At the end of the road you find an old house in which I live. In there you can find warmth and a place to sleep. I'll Even prepare a delicious meal. You'll do well to watch out for anything suspicious though. There has been an increase in yokai sightings recently Follow me, I'll lead the way!", false);
        yield return new WaitWhile(() => dialogueController.dialogDone == false);
        Move(new Vector2(22.9f, -9.8f), true, false);
    }
    public IEnumerator Cut2()
    {

        enemy.GetComponent<Enemy>().animated = true;
        enemy.GetComponent<Enemy>().dontFollow = true;
        enemy.SetActive(true);
        //Move(player.transform.position + new Vector3(1, 1, 0), false, true);
        // yield return new WaitForSeconds(2);
        Talk("Phew, Quite some weather out there. I locked the front door, there might be yokai lurking in the shadows out there. Your Room is the second on the left. I'll bring some food at a later point, so do make yourself at home. But whatever you do \"Do Not Look through the Back Door! I'll be right back ", false);
        yield return new WaitWhile(() => dialogueController.dialogDone == false);
        enemy.GetComponent<Enemy>().animated = false;
        enemy.GetComponent<Enemy>().dontFollow = true;
        Move(new Vector2(-10.52f, 4.4f), true, false);
    }
    public IEnumerator Cut3()
    {
        enemy.GetComponent<Enemy>().animated = true;
        enemy.GetComponent<Enemy>().dontFollow = true;
        Talk("Your Room is the second on the left. I'll bring some food at a later point, so do make yourself at home�. But whatever you do Do Not Look through the Back Door! I'll be right back", false);
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
        enemy.SetActive(true);
        Talk("Where did that little priest go I'M GOING TO RIP HIM APART WHEN I FIND HIM",true);
        camera.GetComponent<CamScript>().followYamauba = true;
        Move(new Vector2(-1f, 0f), false, false);
        yield return new WaitUntil(() => Vector2.Distance(enemy.transform.position, new Vector2(-1f, 0f)) <= 0.5f);
        Transformation(false);
        enemy.GetComponent<Enemy>().animated = true;
        enemy.GetComponent<Enemy>().transformationDone = false;
        enemy.GetComponent<Enemy>().TransformToMonster();
        yield return new WaitForSeconds(4);
        enemy.GetComponent<Enemy>().animated = false;
        Move(new Vector2(-1.57f, 6.38f), true, false);
        yield return new WaitUntil(() => Vector2.Distance(enemy.transform.position, new Vector2(-1.57f, 6.38f)) <= 0.5f);
        camera.GetComponent<CamScript>().followYamauba = false;
        enemy.SetActive(false);
        yield break;
    }
    public IEnumerator Cut5(string TextLine) //call this coroutine for any just text prompt you wanna pull up on a homie with.
    {
        Talk(TextLine, true);
        yield return new WaitWhile(() => dialogueController.dialogDone == false);
        enemy.GetComponent<Enemy>().animated = false;
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

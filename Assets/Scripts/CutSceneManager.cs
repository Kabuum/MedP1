using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus;
using Unity.VisualScripting;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEditor.AnimatedValues;

public class CutSceneManager : MonoBehaviour
{
    public CutScene[] cutScenes = new CutScene[5];
    public CutScene scene = new CutScene();
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        scene.enemy = GameObject.FindWithTag("Yamamba");
        scene.dialogueController = GameObject.FindWithTag("GameManager").GetComponent<DialogueController>();
        scene.player = GameObject.FindWithTag("Player");
        scene.agent = scene.enemy.GetComponent<NavMeshAgent>();

        CutSceneEvent evs = new CutSceneEvent();
        evs.targetPosition = player.transform.position;
        evs.isMoveEvent = true;
        evs.isTalkEvent = false;
        evs.speed = 3;
        evs.moveInstant = true;
        evs.eventDelay = 2;
        evs.dialogText = "";

        cutScenes[0] = scene;
        cutScenes[0].events = new CutSceneEvent[6];
        cutScenes[0].events[0] = evs;

        CutSceneEvent evs2 = new CutSceneEvent();
        evs.targetPosition = player.transform.position;
        evs.isMoveEvent = false;
      //  evs.isTalkEvent = true;
        evs.dialogText = "Hello Darling, the storm is rough these days and it seems to show no signs of stopping. Many people before you have ventured these paths along the mountain, only to find themselves lost in the horrid weather. It is my job to shelter these people... At the end of the road you find an old house in which I live. In there you can find warmth and a place to sleep. I'll Even prepare a delicious meal. You'll do well to watch out for anything suspicious though. There has been an increase in yokai sightings recently Follow me, I'll lead the way!";
        cutScenes[0].events[1] = evs2;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(cutScenes[0].Cutscene());
        }
    }
}
public class CutSceneEvent
{
    public Vector2 targetPosition;
    public float speed;
    public bool transformation;
    public string dialogText;
    public bool moveInstant;
    public bool playerCanMove;
    public float eventDelay;

    public bool isTransformEvent;
    public bool isMoveEvent;
    public bool isTalkEvent = false;

}
public class CutScene
{
    public DialogueController dialogueController;
    //private NavMeshAgent agent = GameObject.FindWithTag("Yamamba").GetComponent<NavMeshAgent>();

    public NavMeshAgent agent;
    public CutSceneEvent[] events;
    public GameObject player;
    public GameObject enemy;
    private float targetOffset = 0.5f;
    public IEnumerator Cutscene()
    {
        enemy.GetComponent<Enemy>().animated = true;
        foreach (CutSceneEvent ev in events)
        {
            Debug.Log("Is talk?" + ev.isTalkEvent);
            enemy.GetComponent<Enemy>().animated = true;
            if (ev.playerCanMove == false)
            {
                player.GetComponent<PlayerBehavior>().canMove = false;
            }
            else
            {
                player.GetComponent<PlayerBehavior>().canMove = true;
            }
            if (ev.isTalkEvent == true)
            {
                if (ev.dialogText != "")
                {
                    // Debug.Log(ev.dialogText);
                    dialogueController.OpenDialog(ev.dialogText);
                }
                yield return new WaitUntil(() => dialogueController.dialogDone == true);
                yield return new WaitForSeconds(ev.eventDelay);
                yield break;
            }
            else if (ev.isMoveEvent)
            {
                if (ev.moveInstant == true)
                {
                    enemy.transform.position = ev.targetPosition;
                    yield return new WaitForSeconds(100);
                    
                    yield break;
                }
                else
                {
                    enemy.GetComponent<Enemy>().animated = true;
                    agent.SetDestination(ev.targetPosition);
                    agent.speed = ev.speed;

                    yield return new WaitUntil(() => Vector2.Distance(enemy.transform.position, player.transform.position) <= 0.25f);
                    yield return new WaitForSeconds(ev.eventDelay);
                    yield break;
                }
            }
            else if (ev.isTransformEvent == true)
            {
                enemy.GetComponent<Enemy>().TransformToMonster();
                //Transofmration time
                yield return new WaitForSeconds(4);
                yield return new WaitForSeconds(ev.eventDelay);
                yield break;
            }
        }
        enemy.GetComponent<Enemy>().animated = false;
        enemy.GetComponent<Enemy>().DestinationUpdate();
    }
    //Unityevent add listner for CutSceneIEnumartori
}
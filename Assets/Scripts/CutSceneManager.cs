using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus;
using Unity.VisualScripting;
using UnityEngine.AI;
using UnityEngine.Events;

public class CutSceneManager : MonoBehaviour
{
    public CutScene[] cutScenes = new CutScene[5];
    public CutScene scene = new CutScene();
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        CutSceneEvent evs = new CutSceneEvent();
        evs.targetPosition = player.transform.position;
        evs.isMoveEvent = true;
        evs.isTalkEvent = false;
        evs.speed = 1;
        evs.moveInstant = true;
        evs.eventDelay = 2;
        evs.dialogText = "Sut min diller";
        evs.targetPosition = new CutScene().player.transform.position;
        cutScenes[0] = scene;
        cutScenes[0].events = new CutSceneEvent[6];
        cutScenes[0].events[0] = evs;

        
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
    public bool isTalkEvent;

}
public class CutScene
{

    public UnityEvent stopPlayerEvent;
    DialogueController dialogueController = GameObject.FindWithTag("GameManager").GetComponent<DialogueController>();
    public GameObject enemy = GameObject.FindWithTag("Yamamba");
    //private NavMeshAgent agent = GameObject.FindWithTag("Yamamba").GetComponent<NavMeshAgent>();

    private NavMeshAgent agent;
    public CutSceneEvent[] events;
    public GameObject player = GameObject.FindWithTag("Player");
    private float targetOffset = 0.5f;

    public IEnumerator Cutscene()
    {
        Debug.Log("bruh");
        foreach (CutSceneEvent ev in events)
        {
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
                    yield return new WaitForSeconds(ev.eventDelay);
                    yield break;
                }
                else
                {
                    agent.SetDestination(ev.targetPosition);
                    agent.speed = ev.speed;
                    yield return new WaitUntil(() => Vector2.Distance(enemy.transform.position, ev.targetPosition) <= targetOffset);
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
    }
    //Unityevent add listner for CutSceneIEnumartori
}
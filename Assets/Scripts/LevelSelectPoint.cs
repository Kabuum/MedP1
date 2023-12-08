using UnityEngine;

public class LevelSelectPoint : MonoBehaviour
{
    public int levelSceneIndex;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneMangment.LoadScene(levelSceneIndex);
        }
    }
}

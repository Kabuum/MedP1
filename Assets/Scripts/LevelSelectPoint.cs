using UnityEngine;

public class LevelSelectPoint : MonoBehaviour
{
    public int levelSceneIndex;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneMangment.LoadScene(levelSceneIndex);
        }
    }
}

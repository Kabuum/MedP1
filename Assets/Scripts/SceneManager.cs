using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class SceneManager

{

    public static void RestartScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        
        }
        

}

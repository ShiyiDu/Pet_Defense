using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChanges : MonoBehaviour
{

    public int sceneNumber;

    public void toScene()
    {
        SceneManager.LoadScene(sceneNumber);
    }


    //for inside code
    public static void changeScene(int num)
    {
        SceneManager.LoadScene(num);
    }
}

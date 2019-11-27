using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dontDestroy : MonoBehaviour
{
    private void Awake()
    {
            DontDestroyOnLoad(this.transform);
            DontDestroyOnLoad(this);
    }
}

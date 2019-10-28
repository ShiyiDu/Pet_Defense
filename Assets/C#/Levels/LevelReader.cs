using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

//this class read file .pet, and generate it into a PetLevel Class
public class LevelReader : MonoBehaviour
{
    public object level;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.dataPath);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

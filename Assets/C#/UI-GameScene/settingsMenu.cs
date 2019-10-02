using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settingsMenu : MonoBehaviour
{

    //if toggle is disables - stop music and vice versa
    //back to menu button

    public Toggle toggle;
    public Text toggleText;

    public GameObject Menu;
    public GameObject SettingsPanel;



    public void Update()
    {
        if (toggle.isOn == true)
        {
            toggleText.text = "Sound On";
            //enable audio
        }


        if (toggle.isOn == false)
        {
            toggleText.text = "Sound Off";
            //disable audio
        }
    }


    public void BackToMenu()
    {
        SettingsPanel.SetActive(false);
        Menu.SetActive(true);
    }





}

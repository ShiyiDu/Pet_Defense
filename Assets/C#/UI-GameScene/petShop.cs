using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class petShop : MonoBehaviour
{

    public Image img;
    public Sprite pom;
    public Sprite fish;
    public Sprite snake;
    public Sprite ham;
    public Sprite bird;
    public Sprite turtle;


    public void Pom()
    {
        img.sprite = pom;
    }

    public void Ham()
    {
        img.sprite = ham;
    }

    public void Snake()
    {
        img.sprite = snake;
    }

    public void Fish()
    {
        img.sprite = fish;
    }

    public void Turtle()
    {
        img.sprite = turtle;
    }

    public void Bird()
    {
        img.sprite = bird;
    }
}

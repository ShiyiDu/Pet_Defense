﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour
{

    public GameObject step1;
    public GameObject step2;
    public GameObject step3;
    public GameObject step4;
    public GameObject step5;
    public GameObject step6;

    private Vector2 target;

    void Start()
    {
        step1.SetActive(false);
        step2.SetActive(false);
        step3.SetActive(false);
        step4.SetActive(false);
        step5.SetActive(false);
        step6.SetActive(false);

    }

    private void Update()
    {
        target = new Vector2(FindObjectOfType<Pommy>().transform.position.x, FindObjectOfType<Pommy>().transform.position.y + 2f);
    }

    public void ShowStepOne()
    {
        step1.SetActive(true);
    }

    public void CloseStepOne()
    {
        Destroy(step1);
    }

    public void ShowStepTwo()
    {
        step2.SetActive(true);

        step2.transform.position = target;
    }

    public void CloseStepTwo()
    {
        Destroy(step2);
    }

    public void ShowStepThree()
    {
        step3.SetActive(true);
    }

    public void CloseStepThree()
    {
        Destroy(step3);
    }

    public void ShowStepFour()
    {
        step4.SetActive(true);
    }

    public void CloseStepFour()
    {
        Destroy(step4);
    }

    public void ShowStepFive()
    {
        step5.SetActive(true);

        step5.transform.position = target;
    }

    public void CloseStepFive()
    {
        Destroy(step5);
    }

    public void ShowStepSix()
    {
        step6.SetActive(true);
    }

    public void CloseStepSix()
    {
        Destroy(step6);
    }
}

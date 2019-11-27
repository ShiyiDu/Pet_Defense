using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
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


    public void ShowStepOne()
    {
        if (step1) step1.SetActive(true);
    }

    public void CloseStepOne()
    {
        if (step1) Destroy(step1);
    }

    public void ShowStepTwo()
    {
        if (step2) step2.SetActive(true);
    }

    public void CloseStepTwo()
    {
        if (step2) Destroy(step2);
    }

    public void ShowStepThree()
    {
        if (step3) step3.SetActive(true);
    }

    public void CloseStepThree()
    {
        if (step3) Destroy(step3);
    }

    public void ShowStepFour()
    {
        if (step4) step4.SetActive(true);
    }

    public void CloseStepFour()
    {
        if (step4) Destroy(step4);
    }

    public void ShowStepFive()
    {
        if (step5) step5.SetActive(true);
    }

    public void CloseStepFive()
    {
        if (step5) Destroy(step5);
    }

    public void ShowStepSix()
    {
        if (step6) step6.SetActive(true);
    }

    public void CloseStepSix()
    {
        if (step6) Destroy(step6);
    }
}

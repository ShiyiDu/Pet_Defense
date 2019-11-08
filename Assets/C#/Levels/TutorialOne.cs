using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialOne : MonoBehaviour
{
    public Tutorial tutor;
    public float stepOne;
    public Bed desiredBed;

    public float stepTwo;
    public float stepThree;
    public float stepFour;
    public Bed desiredBed2;
    public float stepFive;
    public float stepSix;
    // Start is called before the first frame update

    private bool started = false;
    private List<Bed> deactivated = new List<Bed>();

    private bool oneDone;
    public void StepOne()
    {
        if (oneDone) return;
        if (!started) {
            //disable all other beds
            foreach (Bed bed in FindObjectsOfType<Bed>()) {
                if (bed != desiredBed) {
                    bed.gameObject.SetActive(false);
                    deactivated.Add(bed);
                }
            }
            //cover up all other cards
            //a pommy is draged into desired beds
            tutor.ShowStepOne();
            PetUtility.TutorialPause();
            EventManager.TriggerEvent(GameEvent.scrollFreezed);
            started = true;
        }

        if (desiredBed.IsOccupied()) {
            //reactivate all other beds
            foreach (Bed bed in deactivated) {
                bed.gameObject.SetActive(true);
            }
            tutor.CloseStepOne();
            PetUtility.ContinueGame();
            started = false;
            oneDone = true;
            EventManager.TriggerEvent(GameEvent.scrollUnfreezed);
        }
    }

    Pet target;
    private bool twoDone;
    public void StepTwo()
    {
        if (twoDone) return;
        if (!started) {
            started = true;
            if (target == null) target = FindObjectOfType<Pet>();
            tutor.ShowStepTwo();
            PetUtility.TutorialPause();
            EventManager.TriggerEvent(GameEvent.scrollFreezed);
        }

        if (target.rest) {
            tutor.CloseStepTwo();
            started = false;
            PetUtility.ContinueGame();
            EventManager.TriggerEvent(GameEvent.scrollUnfreezed);
            twoDone = true;
        }
    }

    private bool threeDone = false;
    public void StepThree()
    {
        if (threeDone) return;
        if (!started) {
            tutor.ShowStepThree();
            PetUtility.TutorialPause();
            started = true;
            EventManager.TriggerEvent(GameEvent.scrollFreezed);
        }

        if (InputManager.InputIsPressing()) {
            tutor.CloseStepThree();
            PetUtility.ContinueGame();
            started = false;
            threeDone = true;
            EventManager.TriggerEvent(GameEvent.scrollUnfreezed);
        }
    }

    private bool fourDone;
    public void StepFour()
    {
        if (fourDone) return;
        if (!started) {
            //disable all other beds
            deactivated.Clear();
            foreach (Bed bed in FindObjectsOfType<Bed>()) {
                if (bed != desiredBed2 && !bed.IsOccupied()) {
                    bed.gameObject.SetActive(false);
                    deactivated.Add(bed);
                }
            }
            //cover up all other cards
            //a pommy is draged into desired beds
            tutor.ShowStepFour();
            PetUtility.TutorialPause();
            started = true;
            EventManager.TriggerEvent(GameEvent.scrollFreezed);
        }

        if (desiredBed2.IsOccupied()) {
            //reactivate all other beds
            foreach (Bed bed in deactivated) {
                bed.gameObject.SetActive(true);
            }
            tutor.CloseStepFour();
            PetUtility.ContinueGame();
            started = false;
            fourDone = true;
            EventManager.TriggerEvent(GameEvent.scrollUnfreezed);
        }
    }

    private bool fiveDone;
    public void StepFive()
    {
        if (fiveDone) return;
        if (!started) {
            started = true;
            if (target == null) target = FindObjectOfType<Pet>();
            tutor.ShowStepFive();
            PetUtility.TutorialPause();
            EventManager.TriggerEvent(GameEvent.scrollFreezed);
        }

        if (!target.rest) {
            tutor.CloseStepFive();
            started = false;
            PetUtility.ContinueGame();
            fiveDone = true;
            EventManager.TriggerEvent(GameEvent.scrollUnfreezed);
        }
    }

    private bool sixDone;
    public void StepSix()
    {
        if (sixDone) return;
        if (!started) {
            tutor.ShowStepSix();
            PetUtility.TutorialPause();
            started = true;
            EventManager.TriggerEvent(GameEvent.scrollUnfreezed);
        }

        if (InputManager.InputIsPressing()) {
            tutor.CloseStepSix();
            PetUtility.ContinueGame();
            started = false;
            sixDone = true;
            Destroy(gameObject);
            EventManager.TriggerEvent(GameEvent.scrollUnfreezed);
        }

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= stepSix) {
            StepSix();
        } else if (Time.time >= stepFive) {
            StepFive();
        } else if (Time.time >= stepFour) {
            StepFour();
            Debug.Log("try step 4");
        } else if (Time.time >= stepThree) {
            StepThree();
        } else if (Time.time >= stepTwo) {
            StepTwo();
        } else if (Time.time >= stepOne) {
            StepOne();
        }
    }
}

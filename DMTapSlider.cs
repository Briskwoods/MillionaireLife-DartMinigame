using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DMTapSlider : MonoBehaviour
{
    public Animator sliderAnimator;
    public Slider mySlider;
    public float SliderValue;
    public GameObject myUI;
    public WrongAnswer WrongAnswer_;

    public float m_throwHeight = 10f;

    public Transform m_throwPoint;
    public Transform Parent;

    public Rigidbody originalThrowable;

    public Rigidbody dart;

    public Transform Target;

    public GameObject DartPlaceHolder;

    public float force = 10f;

    public DMController dartMinigameController_;

    public DMSwitchController dartMinigameSwitchController_;

    public bool ended = false;

    public bool canThrow = true;

    public bool needsUIDelay = false;

    //public DMThrowCounterUIController dMThrowCounter;

    public void OnEnable()
    {
        sliderAnimator.speed = 1f;
        dart = originalThrowable;
    }

    private void OnDisable()
    {
        StopCoroutine(DelayAfterThrow(0f));
    }

    public void Update()
    {
        SliderValue = mySlider.value;

        if (Input.GetMouseButtonDown(0) && sliderAnimator.speed != 0)
        {
            if (SliderValue > 0.27f && SliderValue < 0.66f)
            {
                sliderAnimator.speed = 0;

                switch (canThrow)
                {
                    case true:
                        needsUIDelay = true;
                        // Throw dart here
                        dart = Instantiate(originalThrowable, m_throwPoint.transform.position, dart.transform.rotation, Parent); // Instantiate Stick to throw at hand position
                        dart.transform.LookAt(Target);
                        dart.AddForce((dart.transform.forward + new Vector3(0, m_throwHeight, 1)) * force, ForceMode.Impulse);// Add force in the Z direction (forward)        
                        dartMinigameController_.throwCount += 1;
                        //dMThrowCounter.ThrowCounter();
                        dartMinigameSwitchController_.SwitchDarts();
                        CheckIfRestart();

                        // Haptic Feedback Added on Tap - Jeff
                        //ControlManager.Instance.HapticsController_.Selection();

                        Vibration.VibratePop();
                        break;
                    case false:
                        break;
                }

            }
            else
            {
                switch (canThrow)
                {
                    case true:
                        needsUIDelay = false;
                        dart = Instantiate(originalThrowable, m_throwPoint.transform.position, dart.transform.rotation, Parent); // Instantiate Stick to throw at hand position
                        dart.transform.LookAt(Target);
                        dart.AddForce((dart.transform.forward + new Vector3(Random.Range(-.3f, .3f), m_throwHeight, 0.6f)) * force, ForceMode.Impulse);// Add force in the Z direction (forward)        

                        CodeManager.Instance.CashManager_Script.DecreaseCash(1000);

                        //dartMinigameController_.throwCount += 1;
                        //dMThrowCounter.ThrowCounter();
                        ended = dartMinigameSwitchController_.CheckCount();
                        CheckIfRestart();
                        ShakeCamera();

                        break;
                    case false:
                        break;
                }

            }
        }
    }

    public void ShakeCamera()
    {
        Vibration.Vibrate();
        CancelInvoke("DisableCamera");
        WrongAnswer_.enabled = true;
        WrongAnswer_.TriggerWrongTap();
        Invoke("DisableCamera", 0.6f);
    }

    public void DisableCamera()
    {
        WrongAnswer_.enabled = false;
    }

    public void CheckIfRestart()
    {
        switch (ended)
        {
            case true:
                // Set UI Inactive
                myUI.SetActive(false);
                // Activate Restart UI
                Debug.Log("Restart UI up.");
                dartMinigameController_.CheckWin();
                break;
            case false:
                StartCoroutine(DelayAfterThrow(0.5f));
                switch (needsUIDelay)
                {
                    case true:
                        StartCoroutine(UIDelayAfterThrow(0.51f));
                        break;
                    case false:
                        break;
                }
                break;
        }

    }

    IEnumerator DelayAfterThrow(float seconds)
    {
        switch (this.enabled)
        {
            case true:
                canThrow = false;
                dart = originalThrowable; // Control Variable
                DartPlaceHolder.SetActive(false);
                yield return new WaitForSeconds(seconds); // Slight Delay before throwing again        
                DartPlaceHolder.SetActive(true);
                canThrow = true;
                break;
            case false:
                break;
        }


    }

    IEnumerator UIDelayAfterThrow(float seconds)
    {
        yield return new WaitForSeconds(seconds); // Slight Delay before throwing again
        myUI.SetActive(false);
    }
}

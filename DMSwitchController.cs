using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMSwitchController : MonoBehaviour
{
    public DMController dartMinigameController;

    public GameObject tapSliderForShot1;
    public GameObject tapSliderForShot2;
    public GameObject tapSliderForShot3;

    public bool state;

    [ContextMenu("Switch Darts")]
    public void SwitchDarts()
    {
        Invoke("CheckAfterDelay", 2f);
    }

    public void CheckAfterDelay()
    {
        switch (dartMinigameController.throwCount < dartMinigameController.maxThrows)
        {
            case true:
                switch (dartMinigameController.currentScore)
                {
                    case 0:
                        SetSlider0Active();
                        SetSlider1Inactive();
                        SetSlider2Inactive();
                        break;
                    case 1:
                        SetSlider0Inactive();
                        SetSlider1Active();
                        SetSlider2Inactive();
                        break;
                    case 2:
                        SetSlider0Inactive();
                        SetSlider1Inactive();
                        SetSlider2Active();
                        break;
                    case 3:
                        SetSlider0Inactive();
                        SetSlider1Inactive();
                        SetSlider2Inactive();
                        break;
                }
                break;
            case false:
                break;
        }
    }

    public bool CheckCount()
    {
        switch (dartMinigameController.throwCount)
        {
            case 1:
                state = false;
                break;
            case 2:
                state = false;
                break;
            case 3:
                state = false;
                break;
            case 4:
                state = false;
                break;
            case 5:
                state = true;
                break;
        }
        return state;
    }

    public void SetSlider0Active()
    {
        tapSliderForShot1.SetActive(true);
    }

    public void SetSlider1Active()
    {
        tapSliderForShot2.SetActive(true);
    }

    public void SetSlider2Active()
    {
        tapSliderForShot3.SetActive(true);
    }

    public void SetSlider0Inactive()
    {
        tapSliderForShot1.SetActive(false);
    }

    public void SetSlider1Inactive()
    {
        tapSliderForShot2.SetActive(false);
    }

    public void SetSlider2Inactive()
    {
        tapSliderForShot3.SetActive(false);
    }
}

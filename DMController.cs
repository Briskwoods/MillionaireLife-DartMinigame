using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DMController : MonoBehaviour
{
    public int throwCount = 0;
    public int maxThrows = 3;

    public int goal = 3;
    public int currentScore = 0;

    public Transform MoneySpawnPoint;

    public Slider Score;

    public DMSwitchController dartMinigameSwitchController_;

    public GameObject[] notes;

    public Transform dartsParent;

    public GameObject canvas;

    private void OnEnable()
    {
        //ControlManager.Instance.TriggerSecondChance_.SecondChanceUI.GetComponent<SecondChance>().AdSuccess += Restart;
    }

    private void OnDisable()
    {
        //ControlManager.Instance.TriggerSecondChance_.SecondChanceUI.GetComponent<SecondChance>().AdSuccess -= Restart;
    }

    [ContextMenu("Check Win")]
    // Use at end of level
    public void CheckWin()
    {
        switch (throwCount < maxThrows)
        {
            case true:
                switch (currentScore == goal)
                {
                    case true:
                        Debug.Log(true);

                        Score.DOValue(currentScore, 0.5f);
                        //CodeManager.Instance.CashManager_Script.IncreaseCash(15000);

                        //Invoke("NewLevel", 2f);
                        NewLevel(2f);
                        break;
                    case false:
                        // Retry can be placed here
                        Debug.Log(false);

                        Score.DOValue(currentScore, 0.5f);

                        CodeManager.Instance.CashManager_Script.IncreaseCash(15000);
                        break;
                }
                break;
            case false:
                // Restart up
                //Debug.Log("Restart UI Up");
                RestartUIUp();
                break;
        }
    }

    public void RestartUIUp()
    {
        // Restart UI up
        //ControlManager.Instance.TriggerSecondChance_.SecondChanceUI.GetComponent<SecondChance>().AdSuccess += Restart;
        //ControlManager.Instance.TriggerSecondChance_.ShowUI();
    }

    [ContextMenu("Restart")]
    public void Restart()
    {
        Debug.Log("Restart UI Down");
        //dMThrowCounter.OnRestart();

        throwCount = 0;
        currentScore = 0;
        Score.value = 0;
        dartMinigameSwitchController_.CheckAfterDelay();
        // Delete Darts in darts parents
        foreach (Transform child in dartsParent)
        {
            GameObject.Destroy(child.gameObject);
        }
        // Enable notes again
        foreach (GameObject note in notes)
        {
            note.SetActive(true);
        }
    }

    public void NewLevel(float delay)
    {
        canvas.SetActive(false);
        //TTPManager.Instance.ShowInterstitialAd("AfterDartsMiniGame", () =>
        //{
        //    ControlManager.Instance.LevelManager_Script.NextLevel();
        CodeManager.Instance.LevelManager_Script.EndLevel(delay);
        //});
    }
}

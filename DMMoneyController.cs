using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMMoneyController : MonoBehaviour
{
    public DMController DartMinigameController_;

    public ParticleSystem startBurst;

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.collider.tag == "DMDart")
        {
            case true:
                collision.collider.gameObject.GetComponentInParent<Rigidbody>().isKinematic = true;
                //collision.collider.gameObject.transform.parent.localScale *=2;
                startBurst.Play();
                DartMinigameController_.currentScore += 1;
                DartMinigameController_.CheckWin();
                StartCoroutine(DelayBeforeDisable(0.5f));

                CodeManager.Instance.CashManager_Script.IncreaseCash(5000);

                break;
            case false:
                break;
        }
    }

    IEnumerator DelayBeforeDisable(float seconds)
    {
        yield return new WaitForSeconds(seconds); // Slight Delay before throwing again
        this.gameObject.SetActive(false);

    }
}

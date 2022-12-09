using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMDartboardController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.collider.tag == "DMDart")
        {
            case true:
                collision.collider.gameObject.transform.localScale *= 2;
                collision.collider.gameObject.GetComponentInParent<Rigidbody>().isKinematic = true;

                // Haptic Feedback Added on MediumImpact - Jeff
                //ControlManager.Instance.HapticsController_.MediumImpact();
                break;
            case false:
                break;
        }
    }
}

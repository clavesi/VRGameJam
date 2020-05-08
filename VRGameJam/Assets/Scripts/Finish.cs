using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Finish : MonoBehaviour
{
    public TMPro.TextMeshPro timer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hand")
        {
            timer.GetComponent<Timer>().enabled = false;
        }
    }
}

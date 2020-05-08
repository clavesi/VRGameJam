using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is just for the Tutorial text that moves up and down
public class PingPong : MonoBehaviour
{
    private void Update()
    {
        this.transform.position = new Vector3(0f, Mathf.Lerp(.9f, 1.1f, Mathf.PingPong(Time.time, 1)), 5.1f);
    }
}

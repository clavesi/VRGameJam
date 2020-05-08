using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TMPro.TextMeshPro timer;

    void Update()
    {
        timer.text = System.Math.Round((Time.time), 3).ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPuzzle : MonoBehaviour
{
    public GetCubeColliders cubePad;
    public GetCubeColliders spherePad;
    public GameObject button;

    private void Start()
    {
        button.active = false;
    }

    void Update()
    {
        if (cubePad.condition == true && spherePad.condition == true)
        {
            button.active = true;
        }
    }
}

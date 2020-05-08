using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class GoToLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.UnloadSceneAsync(0);
        SceneManager.LoadScene(1);
    }
}

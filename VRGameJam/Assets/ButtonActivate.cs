using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivate : MonoBehaviour
{
    public bool spawnCube;
    public GameObject cube;

    public bool spawnSphere;
    public GameObject sphere;

    public bool moveWall;
    public GameObject wall;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (spawnCube)
            {
                Debug.Log("Spawn CUBE");
                Instantiate(cube, GetComponentsInChildren<Transform>()[1]);
            }
        
            if (spawnSphere)
            {
                Debug.Log("Spawn SPHERE");
                Instantiate(sphere, GetComponentsInChildren<Transform>()[1]);
            }

            if (moveWall)
            {
                Debug.Log("Move wall");
                wall.transform.position = wall.transform.position + Vector3.up;
            }
        }
        
    }
}

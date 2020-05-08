using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPlayer : MonoBehaviour
{
    public GameObject bullet;
    [Range(0f, 1f)]
    public float percentage;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("LaunchProjectile", 0f, 1f);
    }

    private void Update()
    {
        transform.LookAt(player);
    }

    void LaunchProjectile()
    {
        // Will shoot a percent of the time - prevents multiple enemies from shooting all at once
        float chanceToShoot = Random.Range(0f, 1f);
        if (chanceToShoot <= percentage)
        {
            Instantiate(bullet, new Vector3(transform.forward.x + transform.position.x, transform.position.y + 1, transform.forward.z + transform.position.z), Quaternion.identity);
        }
    }
}
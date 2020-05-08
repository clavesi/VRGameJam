using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    private Transform player;

    void Start()
    {
        if (this.gameObject.tag != "Bullet")
        {
            // Shoot towards the player
            player = GameObject.FindGameObjectWithTag("Head").transform;
            Vector3 bodyTransform = new Vector3(player.position.x, player.position.y - 0.15f, player.position.z);
            this.GetComponent<Rigidbody>().velocity = (bodyTransform - this.transform.position).normalized * speed;
        }

        Destroy(gameObject, 4.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (this.gameObject.tag != "Bullet")
        {
            if (collision.gameObject.tag == "Umbrella")
            {
                // Destroy if it hits something the umbrella at all, but easier in shield mode
                Destroy(this.gameObject);
            }
        } else 
        {
            if (collision.gameObject.tag != "Umbrella")
            {
                // This is seperate so that the player's bullets don't collide with the umbrella
                Destroy(this.gameObject);
            }
        }
    }
}

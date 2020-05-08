using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Windpad : MonoBehaviour
{
    public float boostHeight = 100f;
    public CharacterController player;

    private float gravity;
    private float maxHeight;
    private bool flying = false;

    private void Start()
    {
        maxHeight = player.gameObject.transform.position.y + boostHeight;
        gravity = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().gravity;
    }

    // Update is above OnTriggerEnter even though it relies on it because I always put Start and Update first
    private void Update()
    {
        // Check if player is above the maxHeight which is set in the OnTriggerEnter and is the current y of the player + boostHeight
        // If it's higher, the player is no longer going upwards
        if (player.gameObject.transform.position.y > maxHeight)
        {
            flying = false;
        }

        // If flying, move upwards by PlayerController's gravity * 2
        if (flying)
        {
            Vector3 direction = new Vector3(0, 1, 0) * Time.deltaTime;
            player.Move(Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up) - new Vector3(0, -gravity * 2, 0f) * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Make sure to only work with player who's in shield mode
        if (other.gameObject.tag == "Player" && GameObject.FindGameObjectWithTag("Umbrella").gameObject.GetComponent<ChangeMode>().weaponMode.ToString() == "Shield")
        {
            maxHeight = player.gameObject.transform.position.y + boostHeight; // Set maxHeight for Update
            flying = true; // Set flying for Update
        }
    }
}

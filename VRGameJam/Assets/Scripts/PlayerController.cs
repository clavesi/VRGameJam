using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerController : MonoBehaviour
{
    public SteamVR_Action_Boolean forward;
    public SteamVR_Action_Boolean backward;
    public float speed = 1;
    public float gravity = 9.81f;
    public float slowGravity;

    private CharacterController characterController;
    private GameObject umbrella;
    private Vector3 direction;

    void Start()
    {
        slowGravity = gravity / 4;

        characterController = GetComponent<CharacterController>();
        umbrella = GameObject.FindGameObjectWithTag("Umbrella");
    }

    void Update()
    {
        if(forward.state) // Move forward
        {
            direction = Player.instance.hmdTransform.TransformDirection(transform.forward);
        }
        else if (backward.state) // Move backward
        {
            direction = Player.instance.hmdTransform.TransformDirection(-transform.forward);
        }
        else // Don't move at all
        {
            direction = new Vector3(0 ,0, 0);
        }

        if (umbrella.GetComponent<ChangeMode>().weaponMode.ToString() == "Shield")
        {
            // Divide gravity by 2 to slow down fall
            gravity =  slowGravity;
        } else
        {
            gravity = 9.81f;
        }
        // Move in whatever direction is being held - foward, backward, or not at all
        // The new Vector3 also allows for falling
        characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up) - new Vector3(0, gravity, 0) * Time.deltaTime);
    }
}

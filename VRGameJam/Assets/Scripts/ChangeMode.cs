using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;

// Enum to store the weapon modes
public enum Modes
{
    Sword,
    Staff,
    Gun,
    Shield
}

public class ChangeMode : MonoBehaviour
{
    [Header("Inputs")]
    public SteamVR_Action_Boolean selectRight;
    public SteamVR_Action_Boolean selectLeft;
    public SteamVR_Action_Boolean fireAction;

    [Header("Modes")]
    public Modes weaponMode = Modes.Sword; // Set an initial weapon mode
    public GameObject[] weaponParts = new GameObject[5]; // All different parts of the weapon to toggle on and off
    private Interactable interactable;
    public SteamVR_Skeleton_Pose regularSkeleton; // Hand pose for most modes
    public SteamVR_Skeleton_Pose staffSkeleton; // Hand pose for staff mode

    [Header("Gun")]
    public GameObject bullet;
    public Transform barrelPivot;
    public float shootingSpeed = 1;

    [Header("Staff Casting")]
    public LineRenderer lineRenderer;
    public float lineWidth = .1f;
    public GameObject fireball;
    private GameObject hitPoint;

    void Start()
    {
        interactable = GetComponent<Interactable>();
        ChangeWeaponMode();

        // Initialize everything to do with the line renderer for staff mode
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.useWorldSpace = true;
        lineRenderer.startWidth = lineRenderer.endWidth = lineWidth;
    }

    void Update()
    {
        if (interactable.attachedToHand != null)
        {
            SteamVR_Input_Sources source = interactable.attachedToHand.handType;

            // Change weapon mode using SnapTurn Right/Left
            if (selectRight[source].stateDown)
            {
                if ((int)weaponMode + 2 > Enum.GetNames(typeof(Modes)).Length)
                {
                    // Checks if at end, go to the beginning
                    weaponMode = (Modes)0;
                }
                else
                {
                    weaponMode = (Modes)(int)weaponMode + 1;
                }
                ChangeWeaponMode();
            }
            else if (selectLeft[source].stateDown)
            {
                if ((int)weaponMode - 1 < 0)
                {
                    // Checks if beginning, go to the end
                    weaponMode = (Modes)Enum.GetNames(typeof(Modes)).Length - 1;
                }
                else
                {
                    weaponMode = (Modes)(int)weaponMode - 1;
                }
                ChangeWeaponMode();
            }

            if (weaponMode.ToString() == "Gun")
            {
                if (fireAction[source].stateDown)
                {
                    Fire();
                }
            } 
            if (weaponMode.ToString() == "Staff")
            {
                RaycastHit hit;
                Vector3 gemTransform = weaponParts[0].GetComponentsInChildren<Transform>()[1].transform.position;
                if (Physics.Raycast(gemTransform, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
                {
                    // Technically, this if and else if statement could be combined and Layer 8 could be renamed
                    // But they're seperate because you can use different colors for the line renderer this way
                    if (hit.collider.gameObject.layer == 8)
                    {
                        lineRenderer.enabled = true;
                        lineRenderer.SetPosition(0, gemTransform); // Start point
                        lineRenderer.SetPosition(1, hit.point); // End point
                        lineRenderer.startColor = Color.red;
                        lineRenderer.endColor = Color.red;

                        if (fireAction[source].stateDown)
                        {
                            Cast(hit);
                        }
                    } 
                    else if (hit.collider.gameObject.tag == "Enemy")
                    {
                        lineRenderer.enabled = true;
                        lineRenderer.SetPosition(0, gemTransform); // Start point
                        lineRenderer.SetPosition(1, hit.point); // End point
                        lineRenderer.startColor = Color.green;
                        lineRenderer.endColor = Color.green;

                        if (fireAction[source].stateDown)
                        {
                            Cast(hit);
                        }
                    } 
                    else
                    {
                        // Disables if not hitting floor
                        lineRenderer.enabled = false;
                    }
                } 
                else
                {
                    // Disables if hitting nothing/sky
                    lineRenderer.enabled = false;
                }
            } 
            else
            {
                // Disables to remove the lineRenderer that used to exist to prevent it from just staying alive
                lineRenderer.enabled = false;
            }
        }
    }

    void ChangeWeaponMode()
    {
        switch (weaponMode)
        {
            case Modes.Sword:
                this.GetComponent<SteamVR_Skeleton_Poser>().SetBlendingBehaviourValue("StaffBlend", 0f);
                weaponParts[0].SetActive(true); // Gem
                weaponParts[1].SetActive(true); // Handle/Blade
                weaponParts[2].SetActive(false); // Sheathe
                weaponParts[3].SetActive(false); // Umbrella Closed
                weaponParts[4].SetActive(false); // Umbrella Opened
                break;

            case Modes.Staff:
                this.GetComponent<SteamVR_Skeleton_Poser>().SetBlendingBehaviourValue("StaffBlend", 1f);
                weaponParts[0].SetActive(true); // Gem
                weaponParts[1].SetActive(true); // Handle/Blade
                weaponParts[2].SetActive(true); // Sheathe
                weaponParts[3].SetActive(false); // Umbrella Closed
                weaponParts[4].SetActive(false); // Umbrella Opened
                break;

            case Modes.Gun:
                this.GetComponent<SteamVR_Skeleton_Poser>().SetBlendingBehaviourValue("StaffBlend", 0f);
                weaponParts[0].SetActive(true); // Gem
                weaponParts[1].SetActive(true); // Handle/Blade
                weaponParts[2].SetActive(true); // Sheathe
                weaponParts[3].SetActive(true); // Umbrella Closed
                weaponParts[4].SetActive(false); // Umbrella Opened
                break;

            case Modes.Shield:
                this.GetComponent<SteamVR_Skeleton_Poser>().SetBlendingBehaviourValue("StaffBlend", 0f);
                weaponParts[0].SetActive(true); // Gem
                weaponParts[1].SetActive(true); // Handle/Blade
                weaponParts[2].SetActive(true); // Sheathe
                weaponParts[3].SetActive(false); // Umbrella Closed
                weaponParts[4].SetActive(true); // Umbrella Opened
                break;
        }
    }

    // Gun shooting function
    void Fire()
    {
        // Shoot forwards from the direction the tip of the umbrella is pointing
        Rigidbody bulletrb = Instantiate(bullet, barrelPivot.position, barrelPivot.rotation).GetComponent<Rigidbody>();
        bulletrb.velocity = barrelPivot.forward * shootingSpeed;

        // Destroy after 3 seconds because if it isn't destroyed in the Projectile script, it's fallen out of the world
        Destroy(bulletrb.gameObject, 3.0f);
    }

    // Staff shooting function
    void Cast(RaycastHit hit)
    {
        // Find position on surface that was hit using empty GameObject and set that as spawnpoint for the fireball
        hitPoint = hit.collider.gameObject.GetComponentsInChildren<Transform>()[1].gameObject;
        hitPoint.transform.position = hit.point;
        GameObject fire = Instantiate(fireball, hitPoint.transform);

        // Destroy after 1 second because that's how long the particle system attached to it is
        Destroy(fire, 1f);
    }
}

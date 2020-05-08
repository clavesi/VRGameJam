 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public GameObject canvasUI;
    public Slider slider;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        if (health <= 0) // Kill
        {
            Destroy(gameObject);
        }
        else if (health >= maxHealth) // Prevent overhealing
        {
            health = maxHealth;
        }
        /*else // Heal
        {
            health += .25f;
        }*/

        slider.value = health / maxHealth;
    }

    // This needs to be OnTriggerEnter because the umbrella becomes kinematic when held which disables the use of OnCollisionEnter
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Umbrella")
        {
            if (other.gameObject.GetComponentInParent<ChangeMode>().weaponMode == Modes.Sword) {
                health -= maxHealth;
            }
        }

        slider.value = health / maxHealth;
    }
}

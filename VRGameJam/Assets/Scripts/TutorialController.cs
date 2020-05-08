using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;
    public TMPro.TextMeshProUGUI swordText;
    public TMPro.TextMeshProUGUI staffText;
    public TMPro.TextMeshProUGUI gunText;
    public TMPro.TextMeshProUGUI shieldText;
    public GameObject swordModel;
    public GameObject staffModel;
    public GameObject gunModel;
    public GameObject shieldModel;

    private void Start()
    {
        // Disable all secondary text
        swordModel.active = staffModel.active = gunModel.active = shieldModel.active = 
            swordText.enabled = staffText.enabled = gunText.enabled = shieldText.enabled = false; 
    }

    public void OnUmbrellaPickup()
    {
        swordModel.active = staffModel.active = gunModel.active = shieldModel.active =
            swordText.enabled = staffText.enabled = gunText.enabled = shieldText.enabled = true;

        text.gameObject.GetComponentInParent<PingPong>().enabled = false;
        text.text = "There are 4 modes. Change with your grip button. Shoot in Staff and Gun mode with your trigger.";
    }
}

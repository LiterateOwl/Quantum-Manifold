using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PressMultiButton : MonoBehaviour
{
    public TextMeshProUGUI textCanvas;

    private bool playerInRange;

    private GameObject multiButton;
    // Start is called before the first frame update
    void Start()
    {
        playerInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            multiButton.GetComponent<MultiButton>().button = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MultiButton")
        {
            playerInRange = true;
            multiButton = other.gameObject;
            textCanvas.gameObject.SetActive(true);
            textCanvas.text = "Press E together with your copy to open the bridge";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MultiButton")
        {
            playerInRange = false;
            multiButton = null;
            textCanvas.gameObject.SetActive(false);
        }
    }
}

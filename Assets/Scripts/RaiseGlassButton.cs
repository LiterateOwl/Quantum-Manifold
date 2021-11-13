using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RaiseGlassButton : MonoBehaviour
{
    public GameObject wall;
    public TextMeshProUGUI textCanvas;

    private bool playerInRange;
    private bool wallActive;
    // Start is called before the first frame update
    void Start()
    {
        playerInRange = false;
        wallActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            wallActive = !wallActive;
            wall.SetActive(wallActive);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Button") playerInRange = true;
        textCanvas.gameObject.SetActive(true);
        textCanvas.text = "Press E to open glass wall";
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Button") playerInRange = false;
        textCanvas.gameObject.SetActive(false);
    }
}

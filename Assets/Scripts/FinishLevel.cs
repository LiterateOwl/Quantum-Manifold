using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class FinishLevel : MonoBehaviour
{
    public string nextScene;

    public TextMeshProUGUI textCanvas;
    public GameObject quantumManager;

    private bool isNearGate;

    private QuantumManager qm;

    // Start is called before the first frame update
    void Start()
    {
        isNearGate = false;
        qm = quantumManager.GetComponent<QuantumManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((isNearGate && !qm.GetQuantum()))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(nextScene);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Gate" && !qm.GetQuantum())
        {
            isNearGate = true;
            textCanvas.gameObject.SetActive(true);
            textCanvas.text = "Press E to finish the level";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Gate")
        {
            isNearGate = false;
            textCanvas.gameObject.SetActive(false);
        }
    }
}

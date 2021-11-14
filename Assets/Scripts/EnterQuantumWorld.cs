using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnterQuantumWorld : MonoBehaviour
{
    public TextMeshProUGUI textCanvas;
    public GameObject entrataMondoQuantico;
    public GameObject cam;
    public Image fade;
    public QuantumManager quantManager;
    private bool isNearQuantumCube;
    private bool isNearGate;
    private bool animazioneVersoEntrata;
    private bool startFade;
    private bool dissolveFade;

    private void Awake()
    {
        fade.canvasRenderer.SetAlpha(0.0f);
    }

    void Update()
    {
        if ((isNearQuantumCube && !quantManager.GetQuantum()) || (isNearGate && quantManager.GetQuantum()))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isNearQuantumCube = false;
                textCanvas.gameObject.SetActive(false);
                GetComponent<PlayerController>().enabled = false;
                animazioneVersoEntrata = true;
                StartCoroutine(startFadeRetard());
            }
        }
    }

    void FixedUpdate()
    {
        //if (animazioneVersoEntrata) Debug.Log("lol");
        //cam.transform.position = Vector3.MoveTowards(cam.transform.position, entrataMondoQuantico.transform.position, 0.005f);

        if (startFade)
            fade.CrossFadeAlpha(1, 3, false);
        else if (dissolveFade)
            fade.CrossFadeAlpha(0, 5, false);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "QuantumCube" && !quantManager.GetQuantum())
        {
            isNearQuantumCube = true;
            textCanvas.gameObject.SetActive(true);
            textCanvas.text = "Press E to go into the quantum world";
        }
        else if (other.tag == "Gate" && quantManager.GetQuantum())
        {
            isNearGate = true;
            textCanvas.gameObject.SetActive(true);
            textCanvas.text = "Press E to exit the quantum world";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "QuantumCube")
        {
            isNearQuantumCube = false;
            if (!quantManager.GetQuantum()) textCanvas.gameObject.SetActive(false);
            else textCanvas.text = "Reach a gate to exit the quantum world";
        }
        else if (other.tag == "Gate")
        {
            isNearGate = false;
            textCanvas.gameObject.SetActive(false);
        }
    }

    IEnumerator startFadeRetard()
    {
        startFade = true;
        yield return new WaitForSeconds(4f);
        startFade = false;
        transform.position = GameObject.Find("StartingPoint").transform.position;
        //cam.transform.position = new Vector3(0, 0.5f, 0);
        quantManager.ToggleQuantum();
        Debug.Log(quantManager.GetQuantum());
        yield return new WaitForSeconds(0.5f);
        dissolveFade = true;
        //animazioneVersoEntrata = false;
        yield return new WaitForSeconds(3f);
        GetComponent<PlayerController>().enabled = true;
        yield return new WaitForSeconds(3.1f);
        dissolveFade = false;
    }
}

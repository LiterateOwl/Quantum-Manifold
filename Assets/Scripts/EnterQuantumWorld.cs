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
    private bool animazioneVersoEntrata;
    private bool startFade;

    private void Awake()
    {
        fade.canvasRenderer.SetAlpha(0.0f);
    }

    void Update()
    {
        if (isNearQuantumCube)
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
        if (startFade)
            fade.CrossFadeAlpha(1, 5, false);
    }

    void FixedUpdate()
    {
        if (animazioneVersoEntrata)
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, entrataMondoQuantico.transform.position, 0.005f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "QuantumCube")
        {
            isNearQuantumCube = true;
            textCanvas.gameObject.SetActive(true);
            textCanvas.text = "Press E to go into the quantum world";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "QuantumCube")
        {
            isNearQuantumCube = false;
            textCanvas.gameObject.SetActive(false);
        }
    }

    IEnumerator startFadeRetard()
    {
        yield return new WaitForSeconds(1);
        startFade = true;
        yield return new WaitForSeconds(2.5f);
        quantManager.ToggleQuantum();
    }
}

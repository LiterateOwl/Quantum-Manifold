using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WallTunneling : MonoBehaviour
{
    public TextMeshProUGUI textCanvas;
    private GameObject wall;
    private bool isNearGlass;
    private Rigidbody rbPlayer;

    private void Awake()
    {
        rbPlayer = GetComponent<Rigidbody>();
        wall = GameObject.FindGameObjectWithTag("Glass");
    }


    void Update()
    {
        if (isNearGlass)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isNearGlass = false;
                GetComponent<PlayerController>().enabled = false;
                StartCoroutine(animationCrossWall());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Glass")
        {
            isNearGlass = true;
            textCanvas.gameObject.SetActive(true);
            textCanvas.text = "Press E to try to cross the wall";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Glass")
        {
            isNearGlass = false;
            textCanvas.gameObject.SetActive(false);
        }
    }

    IEnumerator animationCrossWall()
    {
        int attempts = Random.Range(2, 5);
        for (int i = 0; i < attempts; i++)
        {
            rbPlayer.AddForce(transform.forward * 600);
            yield return new WaitForSeconds(0.3f);
            rbPlayer.AddForce(-transform.forward * 300);
            yield return new WaitForSeconds(0.5f);
        }
        wall.GetComponent<MeshCollider>().enabled = false;
        rbPlayer.AddForce(transform.forward * 700);
        yield return new WaitForSeconds(0.5f);

        wall.GetComponent<MeshCollider>().enabled = true;
        GetComponent<PlayerController>().enabled = true;
    }
}

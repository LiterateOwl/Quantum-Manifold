using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBridge : MonoBehaviour
{

    public Vector3 openPos;

    public GameObject bridge;
    public GameObject button1;
    public GameObject button2;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (button1.GetComponent<MultiButton>().button && button2.GetComponent<MultiButton>().button) bridge.transform.position = openPos;
    }
}

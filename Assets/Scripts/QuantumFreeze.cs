using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantumFreeze : MonoBehaviour
{
    public GameObject quantumManager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (quantumManager.GetComponent<QuantumManager>()) GetComponent<Rigidbody>().isKinematic = true;
        else GetComponent<Rigidbody>().isKinematic = false;
    }
}

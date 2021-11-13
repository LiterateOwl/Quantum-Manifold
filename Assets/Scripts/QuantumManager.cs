using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantumManager : MonoBehaviour
{


    public Material quantumMaterial;
    public Texture classicTex;
    public Texture quantumTex;
    
    private bool quantum;

    public bool GetQuantum()
    {
        return quantum;
    }

    public bool ToggleQuantum()
    {
        quantum = !quantum;
        SwitchTextures();
        return quantum;
    }

    // Start is called before the first frame update
    void Start()
    {
        quantum = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SwitchTextures()
    {
        quantumMaterial.mainTexture = (quantum) ? quantumTex : classicTex;
    }
}

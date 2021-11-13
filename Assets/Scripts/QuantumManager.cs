using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantumManager : MonoBehaviour
{


    public Material quantumMaterial;
    public Material classicGlass;
    public Material quantumGlass;
    public Texture classicTex;
    public Texture quantumTex;

    public GameObject[] glassWalls;
    
    private bool quantum;

    public bool GetQuantum()
    {
        return quantum;
    }

    public bool ToggleQuantum()
    {
        quantum = !quantum;
        SwitchTextures();
        SwitchMaterials();
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

    void SwitchMaterials()
    {
        for (int i = 0; i < glassWalls.Length; i++)
        {
            glassWalls[i].GetComponent<MeshRenderer>().material = (quantum) ? quantumGlass : classicGlass;
        }
    }
}

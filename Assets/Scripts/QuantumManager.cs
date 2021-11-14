using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantumManager : MonoBehaviour
{
    public Vector2 textureScroll;

    public Material quantumMaterial;
    public Material classicGlass;
    public Material quantumGlass;
    public Material classicSky;
    public Material quantumSky;
    public Texture classicTex;
    public Texture quantumTex;

    public GameObject[] glassWalls;

    public GameObject occhio;
    public GameObject vfx;

    private bool quantum;

    public bool GetQuantum()
    {
        return quantum;
    }

    public bool ToggleQuantum()
    {
        quantum = !quantum;
        occhio.SetActive(!quantum);
        vfx.SetActive(quantum);
        GetComponent<MeshRenderer>().enabled = !quantum;
        GetComponent<MeshCollider>().enabled = !quantum;
        SwitchTextures();
        SwitchMaterials();
        SwitchSkyBox();
        return quantum;
    }

    // Start is called before the first frame update
    void Start()
    {
        quantum = false;
        quantumMaterial.mainTexture = classicTex;
    }

    // Update is called once per frame
    void Update()
    {
        if (quantum)
        {
            quantumMaterial.mainTextureOffset += textureScroll * Time.deltaTime;
        }
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

    void SwitchSkyBox()
    {
        RenderSettings.skybox = (quantum) ? quantumSky : classicSky;
    }
}

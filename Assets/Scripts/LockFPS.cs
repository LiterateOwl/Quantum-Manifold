using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockFPS : MonoBehaviour
{

    void Update()
    {
        if (Application.targetFrameRate != 60)
            Application.targetFrameRate = 60;
    }
}

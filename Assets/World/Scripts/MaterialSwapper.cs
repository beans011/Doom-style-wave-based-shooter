using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwapper : MonoBehaviour
{
    public Material[] materials;
    public Renderer rend;
    public float swapInterval;

    private int n;

    void Start()
    {
        rend = GetComponent<Renderer>();
        n = 0;
        InvokeRepeating("SwapMaterial", 0f, swapInterval);
    }

    void SwapMaterial()
    {     
        rend.material = materials[n];

        n = n + 1;
 
        if (n >= materials.Length)
        {
            n = 0;
        }
    }
}

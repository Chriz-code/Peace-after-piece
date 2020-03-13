using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve_Burn : MonoBehaviour
{
    public Material newMaterial;
    private Material m_Material;

    Material Material
    {
        get
        {
            if (m_Material == null)
            {
                if (GetComponent<Renderer>())
                    m_Material = GetComponent<Renderer>().material;
                if (GetComponent<UnityEngine.UI.Image>())
                    m_Material = GetComponent<UnityEngine.UI.Image>().material;
            }
            if (newMaterial)
                if (m_Material.shader != newMaterial.shader)
                {
                    //print("Making new");
                    m_Material = new Material(newMaterial)
                    {
                        hideFlags = HideFlags.HideAndDontSave
                    };
                    if (GetComponent<Renderer>())
                        GetComponent<Renderer>().material = m_Material;
                    if (GetComponent<UnityEngine.UI.Image>())
                        GetComponent<UnityEngine.UI.Image>().material = m_Material;
                    return m_Material;
                }

            return m_Material;
        }
    }
    public float DissolveAmount
    {
        get
        {
            return Material.GetFloat("_DissolveAmount");
        }
        set
        {
            dissolveAmount = value;
            Material.SetFloat("_DissolveAmount", value);
        }
    }
    public float burnSpeed = 0.5f;
    public bool burnComplete;

    [Header("Debug")]
    [SerializeField] float dissolveAmount = 0;
    float dissolveGoal = 0;
    private void Awake()
    {
        DissolveAmount = 0;
        dissolveAmount = DissolveAmount;
    }
    private void Update()
    {
        if (!newMaterial)
            return;
        burnComplete = false;

        if (dissolveGoal > 0 && DissolveAmount < dissolveGoal)
        {
            DissolveAmount += burnSpeed * Time.deltaTime;
        }
        else if (dissolveGoal < 1 && DissolveAmount > dissolveGoal)
        {
            DissolveAmount -= burnSpeed * Time.deltaTime;
        }
        else
        {
            DissolveAmount = dissolveGoal;
            burnComplete = true;
        }
    }

    public bool FastApproximately(float a, float b, float threshold)
    {
        return ((a - b) < 0 ? ((a - b) * -1) : (a - b)) <= threshold;
    }
    public void BurnObject(float dissolveGoal)
    {
        burnComplete = false;
        this.dissolveGoal = dissolveGoal;
    }
    public void BurnObject(float dissolveGoal, float burnSpeed)
    {
        burnComplete = false;
        this.dissolveGoal = dissolveGoal;
        this.burnSpeed = burnSpeed;
    }
}

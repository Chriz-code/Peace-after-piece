using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ThisPerspective))]
public class Player : MonoBehaviour
{
    public Perspective GetPerspective
    {
        get
        {
            return GetComponent<ThisPerspective>().perspective;
        }
    }
}

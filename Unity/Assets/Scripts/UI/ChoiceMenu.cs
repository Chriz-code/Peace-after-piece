using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChoiceMenu : MonoBehaviour
{
    public Button yes = null, no = null;

    private void OnValidate()
    {
        if(!yes || !no)
        {
            Debug.LogWarning(this + " Is missing refrences");
        }
    }
}

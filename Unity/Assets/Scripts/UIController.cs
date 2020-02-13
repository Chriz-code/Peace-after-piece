using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    private static UIController get;
    public static UIController Get { get { return get; } }

    public Image interactPopUp = null;

    private void Awake()
    {
        if (get != null && get != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            get = this;
        }
    }


    public void Interact(bool activate)
    {
        interactPopUp.gameObject.SetActive(activate);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    private static UIController get;
    public static UIController Get { get { return get; } }

    [SerializeField] Image interactPopUp = null;
    public DialogController dc = null;

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
        if (interactPopUp)
            interactPopUp.gameObject.SetActive(activate);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    public static UIController Get { get; private set; }

    [SerializeField] Image interactPopUp = null;
    public DialogController dc = null;

    private void Awake()
    {
        if (Get != null && Get != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Get = this;
        }
    }

    public void Interact(bool activate)
    {
        if (interactPopUp)
            interactPopUp.gameObject.SetActive(activate);
    }

    private void OnEnable()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().onChangePerspective += ClearUi;
    }
    private void OnDisable()
    {
        GameController.Get.onChangePerspective -= ClearUi;
    }

    void ClearUi(GameController gc, Perspective perspective)
    {
        dc.StopDialog();
        Interact(false);
    }
}

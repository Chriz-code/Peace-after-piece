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

    public void Interact(GameObject caller, bool active)
    {
        this.caller = caller;
        this.active = active;
    }
    bool active;
    GameObject caller;
    private void FixedUpdate()
    {
        if (interactPopUp && caller)
        {
            interactPopUp.rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main,
                new Vector3(caller.transform.position.x, caller.transform.position.y + caller.transform.localScale.y, caller.transform.position.z));
            interactPopUp.gameObject.SetActive(active);
            if (active == false)
                caller = null;
        }
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
        Interact(gameObject, false);
        dc.CheckPerspective();
    }
}

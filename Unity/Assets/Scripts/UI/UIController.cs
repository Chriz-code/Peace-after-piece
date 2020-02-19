using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    public static UIController Get { get; private set; }

    [SerializeField] Image interactPopUp = null;
    [SerializeField] public InspectController inspectController = null;
    [SerializeField] public DialogController dialogController = null;
    bool active;
    GameObject caller;



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
        else if (active == false)
        {
            interactPopUp.gameObject.SetActive(active);
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
    public void Interact(GameObject caller, bool active)
    {
        //Debug.Log(caller.name + ":" + active);
        this.caller = caller;
        this.active = active;
    }
    void ClearUi(GameController gc, Perspective perspective)
    {
        dialogController.StopDialog();
        Interact(gameObject, false);
        dialogController.CheckPerspective();
    }
}
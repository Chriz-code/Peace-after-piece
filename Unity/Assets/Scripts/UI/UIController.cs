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

    [Header("Debug")]
    [SerializeField] List<GameObject> callers = new List<GameObject>();
    public GameObject Caller
    {
        get
        {
            if (callers.Count > 0)
            {
                active = true;
                return callers[0];
            }
            else
            {
                active = false;
                return null;
            }
        }
    }

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
        if (interactPopUp && Caller)
        {
            interactPopUp.rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main,
                new Vector3(Caller.transform.position.x, Caller.transform.position.y + Caller.transform.localScale.y, Caller.transform.position.z));
            interactPopUp.gameObject.SetActive(active);
        }
        else if (active == false)
        {
            interactPopUp.gameObject.SetActive(active);
        }
        //active = false;
    }
    private void OnEnable()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().onChangePerspective += ClearUi;
    }
    private void OnDisable()
    {
        GameController.Get.onChangePerspective -= ClearUi;
    }
    public void Interact(GameObject caller, bool addCaller)
    {
        //Debug.Log(caller.name + ":" + active);
        if (addCaller)
        {
            callers.Add(caller);
        }
        else if (!addCaller)
        {
            callers.Remove(caller);
        }
    }
    void ClearUi(GameController gc, Perspective perspective)
    {
        dialogController.StopDialog();
        callers.Clear();
        dialogController.CheckPerspective();
    }
}
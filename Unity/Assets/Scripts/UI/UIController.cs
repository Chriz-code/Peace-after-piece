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
    [SerializeField]
    GameObject[] clearUI = null;
    [SerializeField] GameObject[] freezePlayer = null;

    [Header("Debug")]
    [SerializeField] List<GameObject> callers = new List<GameObject>();
    public GameObject Caller
    {
        get
        {
            if (callers.Count > 0)
            {
                active = true;
                for (int i = 0; i < callers.Count; i++)
                {
                    if (callers[i] != null)
                        if (callers[i].GetComponent<Item>())
                            return callers[i];
                }

                return callers[0];
            }
            else
            {
                active = false;
                return null;
            }
        }
    }
    [SerializeField] Vector2 localOffset = Vector2.zero;


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
                new Vector3(Caller.transform.position.x, Caller.transform.position.y + (Caller.transform.localScale.y / 2), Caller.transform.position.z)
                + (Vector3)localOffset);
            interactPopUp.gameObject.SetActive(active);
        }
        else if (active == false)
        {
            interactPopUp.gameObject.SetActive(active);
        }

        for (int i = 0; i < freezePlayer.Length; i++)
        {
            if (freezePlayer[i].activeSelf)
            {
                GameController.Get.FreezePlayer();
                return;
            }
            else if (GameController.Get.PlayerFrozen)
                GameController.Get.UnFreezePlayer();
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
        if (addCaller)
        {
            if (!callers.Contains(caller))
                callers.Add(caller);
        }
        else if (!addCaller)
        {
            if (callers.Contains(caller))
                callers.Remove(caller);
        }
        localOffset = Vector2.zero;
    }
    public void Interact(GameObject caller, Vector2 localOffset, bool addCaller)
    {
        if (addCaller)
        {
            callers.Add(caller);
        }
        else if (!addCaller)
        {
            callers.Remove(caller);
        }

        if (Caller == caller) // Bugfix; E_interact is now on the right place
            this.localOffset = localOffset;
        else
            this.localOffset = Vector2.zero;
    }
    void ClearUi(GameController gc, Perspective perspective)
    {
        dialogController.StopDialog();
        callers.Clear();
        dialogController.CheckPerspective();

        for (int i = 0; i < clearUI.Length; i++)
        {
            clearUI[i].SetActive(false);
        }
    }
}
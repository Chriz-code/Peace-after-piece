using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PerspectiveChange : MonoBehaviour
{
    [Header("None")]
    [SerializeField] public UnityEvent none;
    [Header("Angela")]
    [SerializeField] public UnityEvent angela;
    [Header("Elenor")]
    [SerializeField] public UnityEvent elenor;

    private void OnEnable()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().onChangePerspective += CallEvents;
    }
    private void OnDisable()
    {
        GameController.Get.onChangePerspective -= CallEvents;
    }

    void CallEvents(GameController gc, Perspective perspective)
    {
        switch (perspective)
        {
            case Perspective.None:
                none?.Invoke();
                break;
            case Perspective.Angela:
                angela?.Invoke();
                break;
            case Perspective.Elenor:
                elenor?.Invoke();
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PerspectiveChange : MonoBehaviour
{
    [Header("None")]
    [SerializeField] public InteractEvent none;
    [Header("Angela")]
    [SerializeField] public InteractEvent angela;
    [Header("Elenor")]
    [SerializeField] public InteractEvent elenor;

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
                none?.Invoke(transform);
                break;
            case Perspective.Angela:
                angela?.Invoke(transform);
                break;
            case Perspective.Elenor:
                elenor?.Invoke(transform);
                break;
        }
    }
}

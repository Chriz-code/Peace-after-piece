using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Z_Flip : MonoBehaviour
{
    private void OnEnable()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().
        onChangePerspective += SwitchZ;
    }

    private void OnDisable()
    {
        GameController.Get.onChangePerspective -= SwitchZ;
    }

    private void Start()
    {
        List<Delegate> invokeList = new List<Delegate>(GameController.Get.onChangePerspective.GetInvocationList());
        
    }

    void SwitchZ(GameController gc, Perspective perspective)
    {
        Debug.Log("Switch background");
        ThisPerspective[] perspectives = UnityEngine.Object.FindObjectsOfType<ThisPerspective>();
        for (int i = 0; i < perspectives.Length; i++)
        {
            if (perspectives[i].perspective == perspective)
            {
                perspectives[i].transform.localPosition = new Vector3(perspectives[i].transform.position.x, perspectives[i].transform.position.y, -5);
            }
            else
            {
                perspectives[i].transform.localPosition = new Vector3(perspectives[i].transform.position.x, perspectives[i].transform.position.y, 5);
            }
        }
    }
}

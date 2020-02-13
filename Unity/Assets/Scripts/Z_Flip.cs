using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Z_Flip : MonoBehaviour
{

    private void OnEnable()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().onChangePerspective += SwitchZ;
    }

    private void OnDisable()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().onChangePerspective -= SwitchZ;
    }

    void SwitchZ(GameController gc, Perspective perspective)
    {
        ThisPerspective[] perspectives = Object.FindObjectsOfType<ThisPerspective>();
        for (int i = 0; i < perspectives.Length; i++)
        {
            if (perspectives[i].perspective == perspective)
            {
                Debug.Log("Switch background");
                perspectives[i].transform.localPosition = new Vector3(perspectives[i].transform.position.x, perspectives[i].transform.position.y, -5);
            }
            else
            {
                perspectives[i].transform.localPosition = new Vector3(perspectives[i].transform.position.x, perspectives[i].transform.position.y, 5);
            }
        }
    }
}

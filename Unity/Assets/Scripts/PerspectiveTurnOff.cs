using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveTurnOff : MonoBehaviour
{
    [SerializeField] GameObject[] gameObjects = null;
    Perspective ParentPerspective
    {
        get
        {
            transform.root.TryGetComponent<ThisPerspective>(out ThisPerspective thisPerspective);
            if (thisPerspective)
                return thisPerspective.perspective;
            else
                return Perspective.None;
        }
    }
    [SerializeField] Perspective perspective = Perspective.Angela;
    [SerializeField] bool parentBased = true;

    void OnEnable()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().onChangePerspective += TurnOff;
    }
    private void OnDisable()
    {
        GameController.Get.onChangePerspective -= TurnOff;
    }

    void TurnOff(GameController gc, Perspective perspective)
    {
        if (gameObjects.Length < 1)
            return;

        if ((parentBased ? ParentPerspective : this.perspective) == perspective)
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                if (gameObjects[i] != null)
                    gameObjects[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                //Debug.Log(gameObjects[i].name);
                if (gameObjects[i] != null)
                    gameObjects[i].SetActive(false);
            }
        }
    }
}

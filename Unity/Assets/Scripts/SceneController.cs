using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour
{
    [SerializeField] public Scene setScene;

    public void ChangeScene()
    {
        if (setScene.IsValid())
        {
            SceneManager.LoadScene(setScene.buildIndex);
        }
    }
    public void ChangeScene(int i)
    {
        SceneManager.LoadScene(i);
    }
    public void ChangeScene(Transform transform)
    {
        ChangeScene();
    }

    //Potencially make scene load async because if one character goes through the door but the other one doesn't and then discard the old scene when everyone has gone through
}

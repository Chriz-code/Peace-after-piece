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
        //LoadSceneAsync(i);
    }
    public void ChangeScene(Transform caller, Transform sender)
    {
        ChangeScene();
    }
    public void LoadSceneAsync(int i)
    {
        print("Loaded Async & additive");
        SceneManager.LoadSceneAsync(i, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(i));
    }

    private void OnEnable()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().onChangePerspective += SetActiveScene;
    }
    private void OnDisable()
    {
        GameController.Get.onChangePerspective -= SetActiveScene;
    }

    public void SetActiveScene(GameController gc, Perspective perspective)
    {
        if (SceneManager.sceneCount > 1)
        {
            if (perspective == Perspective.Angela)
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
            }
            else if (perspective == Perspective.Elenor)
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
            }
        }
    }

    //Potencially make scene load async because if one character goes through the door but the other one doesn't and then discard the old scene when everyone has gone through
}

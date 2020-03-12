using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ObjectScene : MonoBehaviour
{
    [SerializeField] int sceneIndex = 0;
    private void Start()
    {
        GameController.Get.onChangePerspective += SetActive;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnDestroy()
    {
        GameController.Get.onChangePerspective -= SetActive;
    }

    public void SetActive(GameController gc, Perspective perspective)
    {
        if(sceneIndex == SceneManager.GetActiveScene().buildIndex)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

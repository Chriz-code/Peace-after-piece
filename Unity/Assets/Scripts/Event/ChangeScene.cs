using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public void ChangeSceneInt(int i)
    {
        GameController.Get.GetComponent<SceneController>().ChangeScene(i);
    }
}

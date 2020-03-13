using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public int overrideIndex = 0;

    public void ChangeSceneInt(int i)
    {
        GameController.Get.GetComponent<SceneController>().ChangeScene(i);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
            ChangeSceneInt(overrideIndex);
    }
}

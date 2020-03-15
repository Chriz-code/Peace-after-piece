using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public int sceneBuildIndex = 0;

    public void ChangeSceneInt(int i)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(i);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
            ChangeSceneInt(sceneBuildIndex);
    }
}

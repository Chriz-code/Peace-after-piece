using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public GameObject camera;
    public int speed = 1;
    public string meny;

    void Update()
    {
        camera.transform.Translate(Vector3.down * Time.deltaTime * speed);

        if(camera.GetComponent<Transform>().position.y == -90)
        {
            SceneManager.LoadScene(meny)
        }
    }
}

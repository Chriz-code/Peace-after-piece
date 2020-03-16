using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public GameObject camera;
    public int speed = 1;
    public string meny;
    public Transform end;
    public Transform start;

    void Update()
    {
        camera.transform.Translate(Vector3.down * Time.deltaTime * speed);

        StartCoroutine(WaitFor());
    }

    IEnumerator WaitFor()
    {
        yield return new WaitForSeconds(20);
        SceneManager.LoadScene(meny);
    }
}

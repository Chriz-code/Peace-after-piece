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
    public UnityEngine.UI.Text logo = null;
    public float fadingSpeed = 3;
    public float timeToFade = 3;
    public float holdupTime = 6;


    void Update()
    {
        camera.transform.Translate(Vector3.down * Time.deltaTime * speed);

        StartCoroutine(HoldUp());
        if (speed == 0)
            StartCoroutine(ChangeScene());

    }

    IEnumerator HoldUp()
    {
        yield return new WaitForSeconds(80);
        speed = 0;
    }

    IEnumerator ChangeScene()
    {
        for (float i = 0; i < holdupTime; i += Time.deltaTime)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            if (i > timeToFade)
            {
                Color color = logo.color;
                color.a -= Time.deltaTime/fadingSpeed;
                logo.color = color;
            }
        }
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(meny);
    }
}

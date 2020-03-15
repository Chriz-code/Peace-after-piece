using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public UnityEngine.UI.Image image = null;
    private void OnEnable()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().onChangePerspective += FadeIn;
    }
    private void OnDisable()
    {
        GameController.Get.onChangePerspective -= FadeIn;
    }

    public void FadeIn(GameController gc, Perspective perspective)
    {
        fadeGoal = 0;
        FadeAmount = 1;
        active = true;
    }

    float fadeGoal = 1;
    float fadeSpeed = 1;
    bool active = false;
    float FadeAmount {
        get
        {
            return image.color.a;
        }
        set
        {
            Color col = image.color;
            col.a = value;
            image.color = col;
        }
    }

    private void FixedUpdate()
    {
        if (!active && image)
            return;

        if (fadeGoal > 0 && FadeAmount < fadeGoal)
        {
            FadeAmount += fadeSpeed * Time.deltaTime;
        }
        else if (fadeGoal < 1 && FadeAmount > fadeGoal)
        {
            FadeAmount -= fadeSpeed * Time.deltaTime;
        }
        else
        {
            FadeAmount = fadeGoal;
            active = false;
        }
    }
}

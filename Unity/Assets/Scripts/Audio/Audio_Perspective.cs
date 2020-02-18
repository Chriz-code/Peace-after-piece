using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Perspective : MonoBehaviour
{
    [SerializeField] AudioClip elenorsTheme = null, angelasTheme = null;
    public AnimationCurve switchCurve;
    public float switchSpeed = 1;

    private void Start()
    {
        AudioController.Get.mainSource.loop = true;
    }

    private void OnEnable()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().
        onChangePerspective += ThemeSwitch;
    }
    private void OnDisable()
    {
        GameController.Get.onChangePerspective -= ThemeSwitch;
    }

    public void ThemeSwitch(GameController gc, Perspective perspective)
    {
        if (perspective == Perspective.Elenor)
            AudioController.Get.ChangeTrack(0, elenorsTheme, switchCurve, switchSpeed);
        else if (perspective == Perspective.Angela)
            AudioController.Get.ChangeTrack(0, angelasTheme, switchCurve, switchSpeed);
    }
}

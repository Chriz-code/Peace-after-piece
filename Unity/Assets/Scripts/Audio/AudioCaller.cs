using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCaller : MonoBehaviour
{
    [SerializeField] AudioClip clip = null;
    [SerializeField] float timeUntilShot = 0f;
    [Range(0, 256)]
    [SerializeField] int priority = 128;
    [Range(0f, 1f)]
    [SerializeField] float volume = 1;
    [Range(0.1f, 3f)]
    [SerializeField] float pitch = 1;
    [Range(-1f, 1f)]
    [SerializeField] float stereoPan = 0;
    [Range(0f, 1f)]
    [SerializeField] float reverbZoneMix = 0;


    public void CallAudio()
    {
        AudioController.Get.OneShot(clip, volume, timeUntilShot, pitch, stereoPan, reverbZoneMix, priority);
    }
    public void CallAudio(Transform caller, Transform sender)
    {
        AudioController.Get.OneShot(clip, volume, timeUntilShot, pitch, stereoPan, reverbZoneMix, priority);
    }
    public void CallAudio(AudioClip clip, float volume = 1, float timeUntilShot = 0, float pitch = 1, float stereoPan = 0, float reverbZoneMix = 0, int priority = 128)
    {
        AudioController.Get.OneShot(clip, volume, timeUntilShot, pitch, stereoPan, reverbZoneMix, priority);
    }
}

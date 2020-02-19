using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCaller : MonoBehaviour
{
    [SerializeField] AudioClip clip = null;
    [SerializeField] float timeUntilShot = 0f;
    [Range(0, 256)]
    [SerializeField] int priority = 128;
    [Range(0, 1)]
    [SerializeField] float volume = 1;
    [Range(-3, 3)]
    [SerializeField] float pitch = 0;
    [Range(0, 1)]
    [SerializeField] float stereoPan = 0;
    [Range(0, 1)]
    [SerializeField] float reverbZoneMix = 0;


    public void CallAudio()
    {
        AudioController.Get.OneShot(clip, volume, timeUntilShot, pitch, stereoPan, reverbZoneMix, priority);
    }
    public void CallAudio(AudioClip clip, float volume = 1, float timeUntilShot = 0, float pitch = 0, float stereoPan = 0, float reverbZoneMix = 0, int priority = 128)
    {
        AudioController.Get.OneShot(clip, volume, timeUntilShot, pitch, stereoPan, reverbZoneMix, priority);
    }
}

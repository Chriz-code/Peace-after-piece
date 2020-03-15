using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioCallType { OneShot, AddSource, AddSourceAndPlay, ChangeTrack }
public class AudioCaller : MonoBehaviour
{
    [SerializeField] AudioCallType audioCallType = AudioCallType.OneShot;
    [SerializeField] int audioSourceID = 0;
    [SerializeField] bool loop = true;
    public AudioCall audioCall = new AudioCall();


    public void CallAudio()
    {
        switch (audioCallType)
        {
            case AudioCallType.OneShot:
                AudioController.Get.OneShot(audioCall);
                break;
            case AudioCallType.AddSource:
                AudioController.Get.AddSource();
                break;
            case AudioCallType.AddSourceAndPlay:
                AudioController.Get.AddSourceAndPlay(audioCall, loop);
                break;
            case AudioCallType.ChangeTrack:
                AudioController.Get.ChangeTrack(audioCall, audioSourceID);
                break;
        }
    }
    public void CallAudio(Transform caller, Transform sender)
    {
        switch (audioCallType)
        {
            case AudioCallType.OneShot:
                AudioController.Get.OneShot(audioCall);
                break;
            case AudioCallType.AddSource:
                AudioController.Get.AddSource();
                break;
            case AudioCallType.AddSourceAndPlay:
                AudioController.Get.AddSourceAndPlay(audioCall, loop);
                break;
            case AudioCallType.ChangeTrack:
                AudioController.Get.ChangeTrack(audioCall, audioSourceID);
                break;
        }
    }
    public void CallAudio(AudioClip clip, float volume = 1, 
        float timeUntilShot = 0, float pitch = 1, 
        float stereoPan = 0, float reverbZoneMix = 0, 
        int priority = 128, AudioCallType audioCallType = AudioCallType.OneShot, 
        int audioSourceID = 0, bool loop = false)
    {
        AudioCall audioCall = new AudioCall(clip, volume, timeUntilShot, priority, pitch, stereoPan, reverbZoneMix, null, 0);
        switch (audioCallType)
        {
            case AudioCallType.OneShot:
                AudioController.Get.OneShot(audioCall);
                break;
            case AudioCallType.AddSource:
                AudioController.Get.AddSource();
                break;
            case AudioCallType.AddSourceAndPlay:
                AudioController.Get.AddSourceAndPlay(audioCall, loop);
                break;
            case AudioCallType.ChangeTrack:
                AudioController.Get.ChangeTrack(audioCall, audioSourceID);
                break;
        }
    }
}
[System.Serializable]
public class AudioCall
{
    [SerializeField] public AudioClip clip = null;
    [Range(0f, 1f)]
    [SerializeField] public float volume = 1;
    [SerializeField] public float timeUntilShot = 0f;

    [Header("Source Values")]
    [SerializeField] public bool changeSourceValues = true;

    [Range(0, 256)]
    [SerializeField] public int priority = 128;
    [Range(0.1f, 3f)]
    [SerializeField] public float pitch = 1;
    [Range(-1f, 1f)]
    [SerializeField] public float stereoPan = 0;
    [Range(0f, 1f)]
    [SerializeField] public float reverbZoneMix = 0;

    [SerializeField] public AnimationCurve volumeCurve = null;
    [SerializeField] public float curveSpeed = 1;

    public AudioCall()
    {

    }
    public AudioCall(AudioClip clip, float volume)
    {
        this.clip = clip;
        this.volume = volume;
    }
    public AudioCall(AudioClip clip, float volume, float timeUntilShot)
    {
        this.clip = clip;
        this.volume = volume;
        this.timeUntilShot = timeUntilShot;
    }
    public AudioCall(AudioClip clip, float volume,
                     float timeUntilShot, int priority,
                     float pitch, float stereoPan,
                     float reverbZoneMix, AnimationCurve audioCurve,
                     float curveSpeed
                    )
    {
        this.clip = clip;
        this.volume = volume;
        this.timeUntilShot = timeUntilShot;
        this.priority = priority;
        this.pitch = pitch;
        this.stereoPan = stereoPan;
        this.reverbZoneMix = reverbZoneMix;
        this.volumeCurve = audioCurve;
        this.curveSpeed = curveSpeed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Get { get; private set; }
    public AnimationCurve defaultCurve = null;
    public AudioSource mainSource = null;
    public AudioSource shotSource = null;
    public List<AudioSource> sources = new List<AudioSource>();
    AudioSource GetSource(int id)
    {
        if (id > sources.Count)
        {
            Debug.LogWarning("Id is outside of arraylist, returning mainSource");
            return mainSource;
        }
        return sources[id];
    }

    private void Awake()
    {
        if (Get != null && Get != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Get = this;
        }
    }
    private void OnValidate()
    {
        if (!mainSource)
        {
            Debug.LogWarning("Missing MainSource");
        }
    }
    private void Start()
    {
        sources.AddRange(GetComponents<AudioSource>());
    }

    public void OneShot(AudioClip clip = null, float volume = 1, float timeUntilShot = 0f, float pitch = 1, float stereoPan = 0, float reverbZoneMix = 0, int priority = 128)
    {
        shotSource.pitch = pitch;
        shotSource.panStereo = stereoPan;
        shotSource.reverbZoneMix = reverbZoneMix;
        shotSource.priority = priority;
        StartCoroutine(PlayOneShot(clip, volume, timeUntilShot));
    }
    public IEnumerator PlayOneShot(AudioClip clip, float volume, float timeUntilShot = 0)
    {
        yield return new WaitForSeconds(timeUntilShot);
        shotSource.PlayOneShot(clip,volume);
        //AudioSource.PlayClipAtPoint(clip, Vector3.zero,volume);
        Debug.Log(clip+" : "+volume);
    }

    void AddSource()
    {
        sources.Add(gameObject.AddComponent<AudioSource>());
    }
    void AddSourceAndPlay(AudioClip clip, float volume = 1, bool loop = false, float timeUntilShot = 0f, float pitch = 0, float stereoPan = 0, float reverbZoneMix = 0, int priority = 128)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();

        source.pitch = pitch;
        source.panStereo = stereoPan;
        source.reverbZoneMix = reverbZoneMix;
        source.priority = priority;
        source.loop = loop;

        StartCoroutine(PlayAudio(source, clip, volume, timeUntilShot));
        sources.Add(source);
    }
    IEnumerator PlayAudio(AudioSource source, AudioClip clip, float volume, float timeUntilShot = 0)
    {
        yield return new WaitForSeconds(timeUntilShot);
        source.volume = volume;
        source.clip = clip;
        source.Play();
    }

    public void ChangeTrack(int audioSourceID = 0, AudioClip clip = null, AnimationCurve volumeCurve = null, float curveSpeed = 2f)
    {
        StartCoroutine(TrackChange(audioSourceID, clip, volumeCurve, curveSpeed));
    }
    bool trackIenumerating = false;
    IEnumerator TrackChange(int audioSourceID = 0, AudioClip clip = null, AnimationCurve volumeCurve = null, float curveSpeed = 2f)
    {
        if (trackIenumerating)
            yield break;
        trackIenumerating = true;

        if (volumeCurve == null)
            volumeCurve = defaultCurve;

        for (float i = 0; i < 1; i += Time.deltaTime * curveSpeed)
        {
            //Debug.Log(i);
            sources[audioSourceID].volume = volumeCurve.Evaluate(i);
            yield return new WaitForSeconds(Time.deltaTime);
            if(volumeCurve.Evaluate(i) <= 0.1f)
            {
                float length = sources[audioSourceID].time;
                sources[audioSourceID].Stop();
                sources[audioSourceID].clip = clip;
                sources[audioSourceID].time = length;
                sources[audioSourceID].Play();
            }
        }
        sources[audioSourceID].volume = 1f;
        trackIenumerating = false;
    }
}
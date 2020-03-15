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
        DontDestroyOnLoad(this);
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

    public void OneShot(AudioCall audioCall)
    {
        if (audioCall.changeSourceValues)
        {
            shotSource.pitch = audioCall.pitch;
            shotSource.panStereo = audioCall.stereoPan;
            shotSource.reverbZoneMix = audioCall.reverbZoneMix;
            shotSource.priority = audioCall.priority;
        }

        print("One shot: " + audioCall.clip.name + "!");
        StartCoroutine(PlayOneShot(audioCall.clip, audioCall.volume, audioCall.timeUntilShot));
    }
    public IEnumerator PlayOneShot(AudioClip clip, float volume, float timeUntilShot = 0)
    {
        yield return new WaitForSeconds(timeUntilShot);
        shotSource.PlayOneShot(clip,volume);
        //AudioSource.PlayClipAtPoint(clip, Vector3.zero,volume);
    }

    public void AddSource()
    {
        sources.Add(mainSource.gameObject.AddComponent<AudioSource>());
    }
    public void AddSourceAndPlay(AudioCall audioCall, bool loop)
    {
        AudioSource source = mainSource.gameObject.AddComponent<AudioSource>();

        source.pitch = audioCall.pitch;
        source.panStereo = audioCall.stereoPan;
        source.reverbZoneMix = audioCall.reverbZoneMix;
        source.priority = audioCall.priority;
        source.loop = loop;
        print("Added Source And Played: " + audioCall.clip.name + "!");
        StartCoroutine(PlayAudio(source, audioCall.clip, audioCall.volume, audioCall.timeUntilShot));
        sources.Add(source);
    }

    IEnumerator PlayAudio(AudioSource source, AudioClip clip, float volume, float timeUntilShot = 0)
    {
        yield return new WaitForSeconds(timeUntilShot);
        source.volume = volume;
        source.clip = clip;
        source.Play();
    }

    public void ChangeTrack(AudioCall audioCall, int audioSourceID = 0)
    {
        if (audioCall.changeSourceValues)
        {
            sources[audioSourceID].volume = audioCall.volume;
            sources[audioSourceID].priority = audioCall.priority;
            sources[audioSourceID].pitch = audioCall.pitch;
            sources[audioSourceID].panStereo = audioCall.stereoPan;
            sources[audioSourceID].reverbZoneMix = audioCall.reverbZoneMix;
        }
        print("Track Changed To: " + audioCall.clip.name + "!");
        StartCoroutine(TrackChange(audioSourceID, audioCall.clip, audioCall.volumeCurve, audioCall.curveSpeed));
    }

    bool trackIenumerating = false;
    IEnumerator TrackChange(int audioSourceID = 0, AudioClip clip = null, AnimationCurve volumeCurve = null, float curveSpeed = 2f)
    {
        if (trackIenumerating)
            yield break;
        trackIenumerating = true;

        if (volumeCurve == null)
            volumeCurve = defaultCurve;

        bool switchSong = false;
        Keyframe lastKey = volumeCurve.keys[volumeCurve.length];
        for (float i = 0; i < lastKey.time; i += Time.deltaTime * curveSpeed)
        {
            //Debug.Log(i);
            sources[audioSourceID].volume = volumeCurve.Evaluate(i);
            yield return new WaitForSeconds(Time.deltaTime);
            if(i >= lastKey.time/2 && !switchSong)
            {
                switchSong = true;
                print("Track Switched!");
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
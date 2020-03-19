using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if(!sources.Contains(mainSource))
        sources.Add(mainSource);
        if (!sources.Contains(shotSource))
            sources.Add(shotSource);
        if (!mainSource)
        {
            Debug.LogWarning("Missing MainSource");
        }
    }
    private void Start()
    {
        OnValidate();
    }

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += StopIfEndCredits;
    }
    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= StopIfEndCredits;
    }

    void StopIfEndCredits(Scene scene, Scene newScene)
    {
        if(newScene == SceneManager.GetSceneByName("Ending"))
        {
            Destroy(AudioController.Get.gameObject);
        }
    }

    #region Public Calls
    public void OneShot(AudioCall audioCall)
    {
        if (audioCall.changeSourceValues)
        {
            shotSource.pitch = audioCall.pitch;
            shotSource.panStereo = audioCall.stereoPan;
            shotSource.reverbZoneMix = audioCall.reverbZoneMix;
            shotSource.priority = audioCall.priority;
        }
        if(audioCall.clip)
        print("One shot: " + audioCall.clip.name + "!");
        StartCoroutine(PlayOneShot(audioCall.clip, audioCall.volume, audioCall.timeUntilShot));
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
        StartCoroutine(TrackChange(audioSourceID, audioCall));
    }
    #endregion


    public IEnumerator PlayOneShot(AudioClip clip, float volume, float timeUntilShot = 0)
    {
        yield return new WaitForSeconds(timeUntilShot);
        shotSource.PlayOneShot(clip, volume);
        //AudioSource.PlayClipAtPoint(clip, Vector3.zero,volume);
    }
    IEnumerator PlayAudio(AudioSource source, AudioClip clip, float volume, float timeUntilShot = 0)
    {
        yield return new WaitForSeconds(timeUntilShot);
        source.volume = volume;
        source.clip = clip;
        source.Play();
    }
    IEnumerator TrackChange(int audioSourceID, AudioCall audioCall)
    {


        if (audioCall.volumeCurve == null)
            audioCall.volumeCurve = defaultCurve;

        if (audioCall.volumeCurve.keys.Length <= 0)
        {
            print("Track Switched!");
            float length = sources[audioSourceID].time;
            sources[audioSourceID].Stop();
            sources[audioSourceID].clip = audioCall.clip;
            sources[audioSourceID].volume = audioCall.volume;
            sources[audioSourceID].time = length;
            sources[audioSourceID].Play();
            yield break;
        }

        bool switchSong = false;
        Keyframe lastKey = audioCall.volumeCurve.keys[audioCall.volumeCurve.length - 1];
        Keyframe firstKey = audioCall.volumeCurve.keys[0];
        firstKey.value = audioCall.volume;
        lastKey.value = audioCall.volume;

        for (float i = 0; i < lastKey.time; i += Time.deltaTime * audioCall.curveSpeed)
        {
            //Debug.Log(i);
            sources[audioSourceID].volume = audioCall.volumeCurve.Evaluate(i);
            yield return new WaitForSeconds(Time.deltaTime);
            if (i >= lastKey.time / 2 && !switchSong)
            {
                switchSong = true;
                print("Track Switched!");
                float length = sources[audioSourceID].time;
                sources[audioSourceID].Stop();
                sources[audioSourceID].clip = audioCall.clip;
                sources[audioSourceID].time = length;
                sources[audioSourceID].Play();
            }
        }

        if(sources[audioSourceID].clip != audioCall.clip)
        {
            float length = sources[audioSourceID].time;
            sources[audioSourceID].Stop();
            sources[audioSourceID].clip = audioCall.clip;
            sources[audioSourceID].time = length;
            sources[audioSourceID].Play();
        }

        sources[audioSourceID].volume = audioCall.volume;
    }
}
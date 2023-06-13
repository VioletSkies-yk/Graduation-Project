using System.Collections.Generic;
using UnityEngine;

public class AudioManager : OsSingletonMono<AudioManager>
{
    private Dictionary<string, AudioSource> audioSources;
    //protected override void Awake()
    //{
    //    base.Awake();

    //}

    private void Start()
    {
        audioSources = new Dictionary<string, AudioSource>();
        AudioSource[] allSources = GetComponentsInChildren<AudioSource>();
        foreach (AudioSource source in allSources)
        {
            audioSources.Add(source.clip.name, source);
        }
    }

    private void OnEnable()
    {
        EventManager.Instance.StartListening<int>(CONST.FinishLoadingSceneProgress, PlayBGM);
        EventManager.Instance.StartListening<string>(CONST.PlayAudio, Play);
        EventManager.Instance.StartListening<string>(CONST.StopAudio, Stop);
        EventManager.Instance.StartListening(CONST.StopAllAudio, StopAllAudio);

    }

    private void OnDisable()
    {
        EventManager.Instance.StopListening<int>(CONST.FinishLoadingSceneProgress, PlayBGM);
        EventManager.Instance.StopListening<string>(CONST.PlayAudio, Play);
        EventManager.Instance.StopListening<string>(CONST.StopAudio, Stop);
        EventManager.Instance.StopListening(CONST.StopAllAudio, StopAllAudio);
    }

    public void Play(string clipName)
    {
        if (audioSources.ContainsKey(clipName))
        {
            if (audioSources[clipName].isPlaying)
                return;
            else
                audioSources[clipName].Play();
        }
        else
        {
            Debug.LogWarning("Audio clip not found: " + clipName);
        }
    }

    public void Stop(string clipName)
    {
        if (audioSources.ContainsKey(clipName))
        {
            audioSources[clipName].Stop();
        }
        else
        {
            Debug.LogWarning("Audio clip not found: " + clipName);
        }
    }

    public void StopAllAudio()
    {
        foreach (var item in audioSources.Values)
        {
            item.Stop();
        }
    }


    public void PlayBGM(int index)
    {
        StopAllAudio();
        string clipName = KaiUtils.GetSceneBGM(index);
        if (clipName == null)
            return;

        if (audioSources.ContainsKey(clipName))
        {
            audioSources[clipName].Play();
        }
        else
        {
            Debug.LogWarning("Audio clip not found: " + clipName);
        }
    }
}
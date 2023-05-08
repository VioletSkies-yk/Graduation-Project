using System.Collections.Generic;
using UnityEngine;

public class AudioManager : OsSingletonMono<AudioManager>
{
    private Dictionary<string, AudioSource> audioSources;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        audioSources = new Dictionary<string, AudioSource>();
        AudioSource[] allSources = GetComponentsInChildren<AudioSource>();
        foreach (AudioSource source in allSources)
        {
            audioSources.Add(source.clip.name, source);
        }

        EventManager.Instance.StartListening<string>(CONST.PlayAudio, Play);
        EventManager.Instance.StartListening<string>(CONST.StopAudio, Stop);
    }

    private void OnDisable()
    {
        EventManager.Instance.StopListening<string>(CONST.PlayAudio, Play);
        EventManager.Instance.StopListening<string>(CONST.StopAudio, Stop);
    }

    public void Play(string clipName)
    {
        if (audioSources.ContainsKey(clipName))
        {
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
}
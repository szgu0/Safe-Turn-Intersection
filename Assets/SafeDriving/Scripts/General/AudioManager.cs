using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    public static AudioManager ins;

    [SerializeField]
    private AudioSource audioSource;

    private AudioClip _queuedClip;
    private Coroutine _waitClipFinishedCoroutine;

    void Awake()
    {
        if (ins)
        {
            Destroy(gameObject);
            return;
        }

        ins = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    public void Play(AudioClip clip)
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
        if (!clip)
            return;

        audioSource.clip = clip;
        audioSource.Play();

        if (_waitClipFinishedCoroutine != null)
            StopCoroutine(_waitClipFinishedCoroutine);

        _queuedClip = null;
        _waitClipFinishedCoroutine = StartCoroutine(WaitForClipFinished(clip.length));
    }

    IEnumerator WaitForClipFinished(float duration)
    {
        yield return new WaitForSeconds(duration);

        if (_queuedClip)
        {
            Play(_queuedClip);
            _queuedClip = null;
        }
    }

    public void QueuedPlay(AudioClip clip)
    {
        if (audioSource.isPlaying)
        {
            _queuedClip = clip;
            return;
        }

        Play(clip);
    }

    void OnSceneUnloaded(Scene unloadScene)
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }
}

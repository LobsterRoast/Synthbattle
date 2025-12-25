using UnityEngine;
using System.Collections;

public class SoundSystem : MonoBehaviour
{
    public static SoundSystem singleton;
    public GameObject disposable_audio_source;

    IEnumerator PlaySoundAndDestroy(AudioClip clip)
    {
        AudioSource audio_source = Instantiate(disposable_audio_source).GetComponent<AudioSource>();
        audio_source.clip = clip;
        audio_source.Play();
        yield return new WaitForSeconds(.5f);
        Destroy(audio_source.gameObject);
    }
    public void PlaySound(AudioClip clip)
    {
        StartCoroutine(PlaySoundAndDestroy(clip));
    }
    void Start()
    {
        singleton = this;
    }
}

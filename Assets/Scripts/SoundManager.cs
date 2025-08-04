using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public Transform gameCameraTransform;
    public AudioClip[] soundFXClips;

    public AudioClip victoryMusic;

    [SerializeField] private AudioSource soundFXobj;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }


    }



    public void PlaySoundFX(int clipIndex)
    {

        AudioSource audioSource = Instantiate(soundFXobj, gameCameraTransform.position, Quaternion.identity);
        audioSource.clip = soundFXClips[clipIndex];
        audioSource.Play();
        //Debug.Log("Cacca");
        float clipLength = soundFXClips[clipIndex].length;
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlaySoundFX(AudioClip clip)
    {
        AudioSource audioSource = Instantiate(soundFXobj, gameCameraTransform.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.Play();
       // Debug.Log("Cacca");
        float clipLength = clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }



    public IEnumerator FadeCore(AudioSource a, float duration, AudioClip clip)
    {
        float startVolume = a.volume;

        while (a.volume > 0)
        {
            a.volume -= startVolume * Time.deltaTime / duration;
            //Debug.Log(a.volume);
            yield return new WaitForEndOfFrame();
        }

        a.Stop();
        a.volume = startVolume;

        StartCoroutine(FadeInCore(a, duration, clip));
    }

    public IEnumerator FadeInCore(AudioSource a, float duration, AudioClip clip)
    {
        a.clip = clip;
        a.Play();
        float startVolume = a.volume;
        a.volume = 0f;

        while (a.volume < 0.6f)
        {
            a.volume += startVolume * Time.deltaTime / duration;

            yield return new WaitForEndOfFrame();
        }

        a.volume = startVolume;
    }

}
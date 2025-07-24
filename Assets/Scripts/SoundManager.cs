using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public Transform gameCameraTransform;
    public AudioClip[] soundFXClips;

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
        float clipLength = soundFXClips[clipIndex].length;
        Destroy(audioSource.gameObject, clipLength);
    }
    
    public void PlaySoundFX(AudioClip clip)
    {
        AudioSource audioSource = Instantiate(soundFXobj, gameCameraTransform.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.Play();
        float clipLength = clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
}
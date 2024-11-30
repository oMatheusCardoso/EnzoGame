using UnityEngine;

public class AudioController : MonoBehaviour
{

    public AudioSource audioSourceBackgroundSounds;
    public AudioClip[] backgroundSounds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int indexBackgroundMusic = Random.Range(0, backgroundSounds.Length);
        AudioClip backgroundMusic = backgroundSounds[indexBackgroundMusic];
        audioSourceBackgroundSounds.clip = backgroundMusic;
        audioSourceBackgroundSounds.loop = true;
        audioSourceBackgroundSounds.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] playlist;
    public AudioSource audioSource;
    private int musicIndex =0;

    public AudioMixerGroup soundEffectMixer;

    public static AudioManager instence;

    // permet d'acceder a AudioManager depuis n'importe qu'elle classe
    private void Awake()
    {
        if (instence != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de AudioManager dans la scène");
            return;
        }

        instence = this;
    }

    void Start()
    {
        audioSource.clip = playlist[0];
        audioSource.Play();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            playNextSong();
        }
    }

        void playNextSong()
        {
        musicIndex = (musicIndex + 1) % playlist.Length;
        audioSource.clip = playlist[musicIndex];
        audioSource.Play();
        }

    public AudioSource PlayClipAt(AudioClip clip, Vector3 pos)
    {
        // cree un empty avec nom TempAudio avec comme nom variable tempGO
        GameObject tempGO = new GameObject("TempAudio");
        // met la position de tempGO a la variable pos
        tempGO.transform.position = pos;
        // Ajout une audioSource a tempGO + variable temporaire de AudioSource
        AudioSource audioSource = tempGO.AddComponent<AudioSource>();
        //met le son choisit dans la méthode
        audioSource.clip = clip;
        //permet de choisir le mixer
        audioSource.outputAudioMixerGroup = soundEffectMixer;
        // joue le son
        audioSource.Play();
        //detruit tempGO
        Destroy(tempGO, clip.length);
        return audioSource;
    }
}

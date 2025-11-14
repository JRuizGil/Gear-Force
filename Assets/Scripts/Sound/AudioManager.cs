using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [Header("Sonidos para botones")]
    public List<AudioClip> buttonSounds;
    public AudioSource audioSource;

    void Awake()
    {
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        
        Button[] allButtons = FindObjectsOfType<Button>();

        foreach (Button btn in allButtons)
        {
            btn.onClick.AddListener(() => PlayRandomButtonSound());
        }
    }

    public void PlayRandomButtonSound()
    {
        if (buttonSounds == null || buttonSounds.Count == 0) return;

        // Elegir sonido aleatorio
        int randomIndex = Random.Range(0, buttonSounds.Count);
        AudioClip randomClip = buttonSounds[randomIndex];

        audioSource.PlayOneShot(randomClip);
    }
}


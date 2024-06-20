using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    [SerializeField] private AudioClip shootingSound;
    [SerializeField] private AudioClip shootingSound2;
    [SerializeField] private AudioClip shootingSound3;
    [SerializeField] private AudioClip[] takingDamageSounds;
    [SerializeField] private AudioClip reloadingSound;
    [SerializeField] private AudioClip emptyClipSound;
    [SerializeField] private AudioClip healthPickupSound;
    [SerializeField] private AudioClip diamondPickupSound;
    [SerializeField] private AudioClip clockTickingSound;
    [SerializeField] private AudioClip[] footstepSounds;


    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayShootingSound() => audioSource.PlayOneShot(shootingSound);
    public void PlayShootingSound2() => audioSource.PlayOneShot(shootingSound2);
    public void PlayShootingSound3() => audioSource.PlayOneShot(shootingSound3);
    public void PlayReloadingSound() => audioSource.PlayOneShot(reloadingSound);
    public void PlayEmptyClipSound() => audioSource.PlayOneShot(emptyClipSound);
    public void PlayTakingDamageSound() 
    {
        AudioClip clipToPlay = takingDamageSounds[Random.Range(0, takingDamageSounds.Length)];
        
        audioSource.PlayOneShot(clipToPlay);
    }
    public void PlayHealthPickupSound() => audioSource.PlayOneShot(healthPickupSound);
    public void PlayDiamondPickupSound() => audioSource.PlayOneShot(diamondPickupSound);
    public void PlayClockTickingSound() => audioSource.PlayOneShot(clockTickingSound);
    public void PlayFootstepSound()
    {
        AudioClip clipToPlay = footstepSounds[Random.Range(0, footstepSounds.Length)];

        audioSource.PlayOneShot(clipToPlay);
    }
}

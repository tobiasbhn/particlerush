using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundScript : MonoBehaviour {

    // INSTANCE
    [HideInInspector] public static SoundScript instance;

    // OBJECT-LINKS
    [SerializeField] private AudioSource beatMain;
    [SerializeField] private AudioSource beatDuplicate;
    [SerializeField] private AudioSource explosionSmall;
    [SerializeField] private AudioClip[] explosionSoundsSmall;
    [SerializeField] private AudioSource explosionLarge;
    [SerializeField] private AudioClip[] explosionSoundsLarge;
    [SerializeField] private AudioSource shoot;
    [SerializeField] private AudioSource shrink;
    [SerializeField] private AudioSource gold;

    // LOW PASS
    [SerializeField] private AudioMixer audioMixer;

    // VARS
    private Coroutine routine;
    private int targetLowPassFactor = ConstantManager.AUDIO_LOW_PASS_MIN_VALUE;
    private int currentLowPassFactor = 10;

    // SCHEDULED BEAT
    private double nextStartTime = 0;
    private double beatDuration = 0;
    private string currentSource = "duplicate";

    void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    void Start() {
        beatDuration = (double)beatMain.clip.samples / beatMain.clip.frequency;
        nextStartTime = AudioSettings.dspTime + 0.1;
        Debug.Log(beatDuration);
    }
    private void Update() {
        // Wait for end of Beat and Schedule new iteration
        if (AudioSettings.dspTime > nextStartTime - 1) {
            if (currentSource == "duplicate") {
                beatMain.PlayScheduled(nextStartTime);
                currentSource = "main";
            } else if (currentSource == "main") {
                beatDuplicate.PlayScheduled(nextStartTime);
                currentSource = "duplicate";
            }
            nextStartTime += beatDuration;
        }

        // Wait for Low Pass Filter to change and start Coroutine
        if (currentLowPassFactor != targetLowPassFactor && routine == null && audioMixer != null) {
            routine = StartCoroutine(setLowPassFactor());
        }
    }

    // VOLUME
    public void SoundOn() {
        if (audioMixer != null) {
            audioMixer.SetFloat("MasterVolume", 0f);
            audioMixer.SetFloat("BeatVolume", 0f);
        }
    }
    public void SoundOff() {
        if (audioMixer != null) {
            audioMixer.SetFloat("MasterVolume", -80f);
            audioMixer.SetFloat("BeatVolume", 0f);
        }
    }
    public void SoundOnlySound() {
        if (audioMixer != null) {
            audioMixer.SetFloat("MasterVolume", 0f);
            audioMixer.SetFloat("BeatVolume", -80f);
        }
    }


    public static void ExplosionSmall() {
        if (instance.explosionSoundsSmall != null) {
            var rand = Random.Range(0, instance.explosionSoundsSmall.Length);
            instance.explosionSmall.PlayOneShot(instance.explosionSoundsSmall[rand]);
        }
    }
    public static void ExplosionLarge() {
        if (instance.explosionSoundsLarge != null) {
            var rand = Random.Range(0, instance.explosionSoundsLarge.Length);
            instance.explosionLarge.PlayOneShot(instance.explosionSoundsLarge[rand]);
        }
    }
    public static void Shoot() {
        instance.shoot.PlayOneShot(instance.shoot.clip);
    }
    public static void Shrink() {
        instance.shrink.PlayOneShot(instance.shrink.clip);
    }
    public static void Gold() {
        instance.gold.PlayOneShot(instance.gold.clip);
    }





    public void EnableLowPass() {
        targetLowPassFactor = ConstantManager.AUDIO_LOW_PASS_MIN_VALUE;
    }
    public void DisableLowPass() {
        targetLowPassFactor = ConstantManager.AUDIO_LOW_PASS_MAX_VALUE;
    }


    private IEnumerator setLowPassFactor() {
        if (audioMixer != null) {
            while (currentLowPassFactor <= ConstantManager.AUDIO_LOW_PASS_MAX_VALUE && currentLowPassFactor >= ConstantManager.AUDIO_LOW_PASS_MIN_VALUE &&
                    Mathf.Abs(targetLowPassFactor - currentLowPassFactor) >= 30) {
                currentLowPassFactor += (targetLowPassFactor - currentLowPassFactor) / 30;
                audioMixer.SetFloat("MainLowpassCutoffFrequency", currentLowPassFactor);
                yield return null;
            }
            currentLowPassFactor = targetLowPassFactor;
            audioMixer.SetFloat("MainLowpassCutoffFrequency", currentLowPassFactor);
        }
        yield return null;
        routine = null;
    }
}

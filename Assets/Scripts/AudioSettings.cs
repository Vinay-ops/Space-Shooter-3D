using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Mixer & UI")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;

    private const string VolumeParam = "MasterVolume";
    private const string PlayerPrefKey = "MasterVolume";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        float savedDb = PlayerPrefs.GetFloat(PlayerPrefKey, 0f);
        audioMixer.SetFloat(VolumeParam, savedDb);

        if (volumeSlider != null)
        {
            volumeSlider.value = Mathf.Pow(10f, savedDb / 20f);
            volumeSlider.onValueChanged.AddListener(SetMasterVolume);
        }
    }

    public void SetMasterVolume(float value)
    {
        float dB = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat(VolumeParam, dB);
        PlayerPrefs.SetFloat(PlayerPrefKey, dB);
    }
}

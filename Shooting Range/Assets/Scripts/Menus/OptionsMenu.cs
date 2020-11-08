using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour {
	public PlayerStats stats;

    public AudioMixer audioMixer;

    public void setSensitivity(float Sensitivity)
    {
        stats.turnSpeed = Sensitivity;
    }

    public void setVolume (float Sound)
    {
        audioMixer.SetFloat("MasterVolume", Sound);
    }
}
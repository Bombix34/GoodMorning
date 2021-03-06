using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
//using UnityStandardAssets.CrossPlatformInput;

[AddComponentMenu("KLabsAudioTools/CharacterAudioManager")]
public class CharacterAudioManager : MonoBehaviour
{
    public AudioMixerGroup m_audioMixer;
    [Range(-48.0f, 3.0f)]
    public float m_volume;
    public bool m_mute = false;

    public playOn m_triggerChoice;
    public enum playOn { fire1, fire2, spacebar, custom }
    public AudioClip[] m_audioClips;
    public string m_customKey;

    public float m_randomPitch;

    //public bool bypassReverb = true;


    [HideInInspector]
    public AudioSource m_audioSource;

    void Start()
    {
        GameObject child = new GameObject("Player");
        child.transform.parent = gameObject.transform;
        m_audioSource = child.AddComponent<AudioSource>();
        //m_audioSource.clip = m_audioClips[0];
        m_audioSource.outputAudioMixerGroup = m_audioMixer;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_audioClips.Length != 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (m_triggerChoice == playOn.fire1)
                {
                    playSound();
                }
            }

            if (Input.GetButtonDown("Fire2"))
            {

                if (m_triggerChoice == playOn.fire2)
                {
                    playSound();
                }
            }

            if (m_customKey != "")
            {
                if (m_triggerChoice == playOn.custom)
                {
                    if (Input.GetKeyDown(m_customKey) || Input.GetButtonDown(m_customKey))
                    {
                        playSound();
                    }
                }
            }

            if (m_triggerChoice == playOn.spacebar)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    playSound();
                }
            }
        }
        else Debug.Log("AudioClip(s) Are Not Assigned");
    }

    void playSound()
    {
        float pitchRand = Random.Range(-1.0f, 1.0f);
        int audioRand = Random.Range(0, m_audioClips.Length);
        m_audioSource.pitch = 1 + (pitchRand * m_randomPitch / 100.0f);
        m_audioSource.PlayOneShot(m_audioClips[audioRand]);
        //m_audioSource.bypassReverbZones = bypassReverb;
        //m_audioSource.bypassEffects = bypassReverb;
        //m_audioSource.bypassListenerEffects = bypassReverb;
        m_audioSource.volume = Mathf.Pow(10, m_volume / 20.0f);
        m_audioSource.mute = m_mute;
    }
}

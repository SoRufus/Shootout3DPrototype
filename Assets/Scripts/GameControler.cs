using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameControler : MonoBehaviour
{
    [SerializeField] private GameObject enemiesObject;
    [SerializeField] private static TextMeshProUGUI scoreText;
    [SerializeField] private static TextMeshProUGUI victoryText;
    [SerializeField] private GameObject textHolder;
    public static float minSfxRandomPitch, maxSfxRandomPitch;
    public static int enemiesCounter;
    public static AudioClip[] clips;
    public static AudioSource sfxSource;

    void Start()
    {
        minSfxRandomPitch = 0.8f;
        maxSfxRandomPitch = 1.2f;

        victoryText = textHolder.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        scoreText = textHolder.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        sfxSource = GetComponent<AudioSource>();

        clips = new AudioClip[2];
        clips[0] = Resources.Load<AudioClip>("death");
        clips[1] = Resources.Load<AudioClip>("gun");

        scoreText.text = enemiesCounter.ToString() + " Reds left";
    }

    public static void SoundPlayer(int clip)
    {
        sfxSource.pitch = Random.Range(minSfxRandomPitch, maxSfxRandomPitch);
        sfxSource.PlayOneShot(clips[clip]);
    }

    public static void ScorePreview()
    {
        if (enemiesCounter == 0)
        {
            victoryText.text = "You won :)";
        }
        scoreText.text = enemiesCounter.ToString() + " Reds left";
    }
}

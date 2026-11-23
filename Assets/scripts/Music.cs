using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music current;

    void Awake()
    {
        current = this;
    }


    [SerializeField] AudioClip gameover;

    public void PlayGameOver()
    {
        AudioSource source = GetComponent<AudioSource>();
        source.loop = false;
        source.clip = gameover;
        source.Play();
    }
}

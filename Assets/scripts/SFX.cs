using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public static SFX current;

    private void Awake()
    {
        current = this;
    }



    [SerializeField] private AudioClip newSeed;
    [SerializeField] private AudioClip getSeed;
    [SerializeField] private AudioClip[] getPath;
    [SerializeField] private AudioClip clic;
    [SerializeField] private AudioClip buildPath;
    [SerializeField] private AudioClip toOutside;
    [SerializeField] private AudioClip toUnderground;
    [SerializeField] private AudioClip rotate;
    [SerializeField] private AudioClip buildMushroom;
    [SerializeField] private AudioClip treeGrown;
    [SerializeField] private AudioClip treeDie;
    [SerializeField] private AudioClip treeDead;
    [SerializeField] private AudioClip animal;
    [SerializeField] private AudioClip eating;
    [SerializeField] private AudioClip dial;


    public void Play(string id)
    {
        List<AudioSource> sources = GetComponents<AudioSource>().ToList();
        AudioSource source = sources.Find(s => !s.isPlaying);
        if (source == null)
        {
            source = sources[0];
        }
        if (source != null)
        {
            source.volume = 1f;
            switch (id)
            {
                case "TREE_GROWN": source.clip = treeGrown; break;
                case "TO_OUTSIDE": source.volume = 0.25f;  source.clip = toOutside; break;
                case "TO_UNDERGROUND": source.volume = 0.25f; source.clip = toUnderground; break;
                case "CLICK": source.clip = clic; break;
                case "BUILD_PATH": source.clip = buildPath; break;
                case "GET_SEED": source.clip = getSeed; break;
                case "GET_PATH": source.clip = getPath[Random.Range(0, getPath.Length)]; break;
                case "ROTATE": source.clip = rotate; break;
                case "BUILD_MUSHROOM": source.clip = buildMushroom; break;
                case "TREE_DIE": source.clip = treeDie; break;
                case "TREE_DEAD": source.clip = treeDead; break;
                case "DIAL": source.volume = 0.25f; source.clip = dial; break;
                case "ANIMAL": source.clip = animal; break;
                case "EATING": source.clip = eating; break;
                default: source.volume = 0.25f; source.clip = newSeed; break;
            }
            source.Play();
        }

    }
}

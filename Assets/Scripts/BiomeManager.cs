using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class BiomeManager : MonoBehaviour
{
    public static BiomeManager Instance;
    [SerializeField] private List<GameObject> biomesPrefab = new List<GameObject>();
    [SerializeField] private List<GameObject> levelsPrefab = new List<GameObject>();
    private GameObject biome;
    private GameObject level;
    private int lastBiome;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this) Destroy(gameObject);

    }

    void Start()
    {
        biome = Instantiate(biomesPrefab[0]);
    }

    public void RandomBiome()
    {
        Destroy(biome);
        if (level != null) Destroy(level);
        int random, random2;
        do
        {
            random = Random.Range(1, 4);
            random2 = Random.Range(0, 3);
        } while (random == lastBiome);
        biome = Instantiate(biomesPrefab[random]);
        level = Instantiate(levelsPrefab[random2 + 3 * (random  - 1)], biome.transform);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class BiomeManager : MonoBehaviour
{
    public static BiomeManager Instance;
    [SerializeField] private List<GameObject> biomesPrefab = new List<GameObject>();
    private GameObject biome;
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
        int random;
        do
        {
            random = Random.Range(1, 4);
        } while (random == lastBiome);
        biome = Instantiate(biomesPrefab[random]);
    }
}

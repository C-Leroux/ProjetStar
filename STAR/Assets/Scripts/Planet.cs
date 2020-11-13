using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public enum Biome
    {
        forest,
        water,
        desert,
        fire
    }

    public enum Size
    {
        small,
        medium,
        big
    }

    private Biome biome;
    private Size size;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(Biome b, Size s)
    {
        biome = b;
        size = s;
    }

    public static Planet CreatePlanet(Biome biome, Size size)
    {
        GameObject obj = new GameObject();
        Planet planet = obj.AddComponent<Planet>();
        planet.Init(biome, size);
        return planet;
    }
}

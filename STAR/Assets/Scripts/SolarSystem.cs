using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    // List of all the planets in the solar system. The solar system is made up of 6 levels (from the start to the end), and each level of 1 to 4 planets.
    // Each planet is linked to one, two or three planets of the previous level, and of the next level.
    Planet[][] solar_system;

    // Start is called before the first frame update
    void Start()
    {
        // Here should be called the random generator for the planets of the solar system. For now, planets are created manually.
        // There are two generators : one for the solar system and its composition, and one for each planet individually.

        Planet planet1 = Planet.CreatePlanet(Planet.Biome.fire, Planet.Size.small);
        Planet planet2 = Planet.CreatePlanet(Planet.Biome.forest, Planet.Size.medium);
        Planet planet3 = Planet.CreatePlanet(Planet.Biome.desert, Planet.Size.big);
        Planet planet4 = Planet.CreatePlanet(Planet.Biome.desert, Planet.Size.small);
        Planet planet5 = Planet.CreatePlanet(Planet.Biome.water, Planet.Size.medium);
        Planet planet6 = Planet.CreatePlanet(Planet.Biome.fire, Planet.Size.medium);
        Planet planet7 = Planet.CreatePlanet(Planet.Biome.forest, Planet.Size.small);
        Planet planet8 = Planet.CreatePlanet(Planet.Biome.water, Planet.Size.big);
        Planet planet9 = Planet.CreatePlanet(Planet.Biome.fire, Planet.Size.small);
        Planet planet10 = Planet.CreatePlanet(Planet.Biome.water, Planet.Size.small);
        Planet planet11 = Planet.CreatePlanet(Planet.Biome.desert, Planet.Size.big);
        Planet planet12 = Planet.CreatePlanet(Planet.Biome.fire, Planet.Size.small);
        Planet planet13 = Planet.CreatePlanet(Planet.Biome.forest, Planet.Size.big);
        Planet planet14 = Planet.CreatePlanet(Planet.Biome.desert, Planet.Size.medium);
        Planet planet15 = Planet.CreatePlanet(Planet.Biome.water, Planet.Size.big);

        solar_system = new Planet[6][];

        solar_system[0][0] = planet1;
        solar_system[0][1] = planet2;
        solar_system[1][0] = planet3;
        solar_system[1][2] = planet4;
        solar_system[1][3] = planet5;
        solar_system[2][0] = planet6;
        solar_system[2][1] = planet7;
        solar_system[2][2] = planet8;
        solar_system[2][3] = planet9;
        solar_system[3][0] = planet10;
        solar_system[3][1] = planet11;
        solar_system[3][2] = planet12;
        solar_system[4][0] = planet13;
        solar_system[4][1] = planet14;
        solar_system[5][0] = planet15;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

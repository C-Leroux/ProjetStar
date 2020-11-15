using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class Planet : SpaceObject
    {
        public enum Biome
        {
            forest,
            ice,
            desert,
            fire
        }

        public enum Size
        {
            small,
            medium,
            big
        }

        public Biome biome;
        public Size size;
        private SpriteRenderer render;

        static private Array valuesBiome = Enum.GetValues(typeof(Biome));
        static private Array valuesSize  = Enum.GetValues(typeof(Size));
        static private System.Random random = new System.Random();

        void Awake()
        {
            render = gameObject.AddComponent<SpriteRenderer>();
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
            GameObject obj = new GameObject("Planet");
            Planet planet = obj.AddComponent<Planet>();
            planet.Init(biome, size);

            planet.SetSprite();

            return planet;
        }
        public static Planet CreateRandomPlanet()
        {
            Biome rbiome = (Biome)valuesBiome.GetValue(random.Next(valuesBiome.Length));
            Size rsize = (Size)valuesSize.GetValue(random.Next(valuesSize.Length));
            return CreatePlanet(rbiome, rsize);
        }

        public void SetSprite()
        {
            string strbiome = "";

            switch(biome)
            {
                case Biome.forest:
                    strbiome = "forest";
                    break;
                case Biome.ice:
                    strbiome = "ice";
                    break;
                case Biome.desert:
                    strbiome = "desert";
                    break;
                case Biome.fire:
                    strbiome = "volcanic";
                    break;
            }

            render.sprite = Resources.Load<Sprite>("Sprites/Solar_system/planet_" + strbiome + ".png");
        }
    }
}

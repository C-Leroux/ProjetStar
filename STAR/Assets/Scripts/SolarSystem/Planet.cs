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

        public Biome biome { get; set; }
        public Size  size { get; set; }
        private SpriteRenderer render;

        static private Array valuesBiome = Enum.GetValues(typeof(Biome));
        static private Array valuesSize  = Enum.GetValues(typeof(Size));
        static private System.Random random = new System.Random();


        void Awake()
        {
            render = gameObject.AddComponent<SpriteRenderer>();
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
            // Load the correct sprite based on the biome
            string strbiome = "forest";
            switch(biome)
            {
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

            string path = "Sprites/Solar_system/planet_" + strbiome;
            render.sprite = Resources.Load<Sprite>(path);
            render.sortingLayerName = "Sprites";

            // Set the correct size
            float scale = 0.1f;
            switch(size)
            {
                case Size.small:
                    scale = 0.07f;
                    break;
                case Size.big:
                    scale = 0.13f;
                    break;
            }
            gameObject.transform.localScale = new Vector3(scale, scale, 0);
            initialScale = transform.localScale;
            hoverScale = transform.localScale * scaleMult;
            gameObject.AddComponent<BoxCollider2D>();
        }
    }
}

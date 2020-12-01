using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Case
    {
        private bool isClickable;
        private List<GameObject> listObject;
        private GameObject m_terrain;
        private GameObject m_object;
        private Turret m_turret;
        private GameObject m_turretObject;
        private TurretBehaviour m_turretBehaviour;
        private int x;
        private int y;
        private bool isDepart;
        private bool isVillage;
        private Transform boardHolder;

        public Case(int x, int y, Transform boardHolder)
        {
            isClickable = true;
            listObject = new List<GameObject>();
            this.x = x;
            this.y = y;
            isDepart = false;
            isVillage = false;
            this.boardHolder = boardHolder;
        }
        
        public void AddObject(GameObject new_object)
        {
            listObject.Add(new_object);
            switch(new_object.tag)
            {
                case "Path":
                    isClickable = false;
                    SetTerrain(new_object);
                    break;

                case "Floor":
                    SetTerrain(new_object);
                    break;
                
                case "Trunk":
                    isClickable = false;
                    SetObject(new_object);
                    break;

                case "Foliage":
                    isClickable = false;
                    SetObject(new_object);
                    break;

                case "Object":
                    isClickable = false;
                    SetObject(new_object);
                    break;
                case "Base":
                    isClickable = false;
                    SetObject(new_object);
                    this.m_object.transform.localScale = new Vector3(7, 7, 7);
                    break;
            }

        }

        public void SetTerrain(GameObject terrain)
        {
            this.m_terrain = terrain;
        }

        public void SetObject(GameObject new_object)
        {
            this.m_object = new_object;
        } 
        
        public void SetTurret(Turret turret)
        {
            this.m_turret = turret;
        }

        public Turret GetTurret()
        {
            return m_turret;
        }
        
        public void SetTurretObject(GameObject turretObject)
        {
            this.m_turretObject = turretObject;
        }
        
        public void SetTurretBehaviour(TurretBehaviour turretBehaviour)
        {
            this.m_turretBehaviour = turretBehaviour;
        }

        public TurretBehaviour GetTurretBehaviour()
        {
            return m_turretBehaviour;
        }

        public GameObject GetTurretObject()
        {
            return m_turretObject;
        }
        
        public GameObject GetObject()
        {
            return m_object;
        }

        public GameObject GetTerrain()
        {
            return m_terrain;
        }

        public bool IsClickable()
        {
            return isClickable;
        }

        public bool IsDepart()
        {
            return isDepart;
        }

        public bool IsVillage()
        {
            return isVillage;
        }

        public void SetDepart()
        {
            isDepart = true;
            isClickable = false;
        }

        public void SetVillage()
        {
            isVillage = true;
            isClickable = false;
        }

        public float GetX()
        {
            return x * 2.56f;
        }

        public float GetY()
        {
            return y * 2.56f;
        }
    }
}
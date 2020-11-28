using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Case //: MonoBehaviour
    {
        private bool isClickable;
        private List<GameObject> listObject;
        private GameObject m_terrain;
        private GameObject m_object;
        private Turret m_turret;
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
                
                case "Three":
                    isClickable = false;
                    SetObject(new_object);
                    break;

                case "Foliage":
                    isClickable = false;
                    SetObject(new_object);
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
        
        public GameObject GetObject()
        {
            return m_object;
        }

        public GameObject GetTerrain()
        {
            return m_terrain;
        }
        /*
        public void AddObject(GameObject new_object, bool isClickable)
        {
            this.isClickable = isClickable;
            listObject.Add(new_object);
            if (new_object.tag == "Floor")
            {
                SetTerrain(new_object);
            }
        }*/

       /* public void PrintAllObject()
        {
            for (int i = 0; i < listObject.Count; i++)
            {
                PrintObject(listObject[i], i);
            }
        }

        public void PrintObject(GameObject m_object, int z)
        {
            GameObject gO;
            if (m_object.tag == "Foliage")
            {
                z++;
                gO = Instantiate(m_object, new Vector3(x * 2.56f, -(y + 1 )* 2.56f, -z * 0.1f), Quaternion.identity) as GameObject;
            }
            else if(m_object.tag == "Trunk")
            {
                gO = Instantiate(m_object, new Vector3(x * 2.56f, -(float)(y + 0.8 ) * 2.56f, -z * 0.1f), Quaternion.identity) as GameObject;
            }
            else
            {
                gO = Instantiate(m_object, new Vector3(x * 2.56f, -y * 2.56f, -z * 0.1f), Quaternion.identity) as GameObject;
            }
            gO.transform.SetParent(boardHolder);
        }*/

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

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
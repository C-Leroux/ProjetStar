using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player
    {

        private static Player instance = null;// SINGLETON
        public turretDisplay turetDisp;
        private List<Turret> turrets;

        public Player()
        {
            turrets = new List<Turret>();
            turrets.Add(turetDisp.GetTurret("STAR"));
            turrets.Add(turetDisp.GetTurret("Grenadiere"));
        }

        public void AddTurret(string nameTurret)
        {
            turrets.Add(turetDisp.GetTurret(nameTurret));
        }

        public List<Turret> GetTurrets()
        {
            return turrets;
        }

        public static Player Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Player();
                }
                return instance;
            }
        }
    }
}

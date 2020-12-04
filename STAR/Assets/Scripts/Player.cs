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
        }

        public void AddTurret(string nameTurret)
        {
            turrets.Add(turetDisp.GetTurret(nameTurret));
        }

        public List<Turret> GetTurrets()
        {
            return turrets;
        }

        public void SetDisplayTurret(turretDisplay turretDisp)
        {
            this.turetDisp = turretDisp;
            turrets.Add(turetDisp.GetTurret("STAR"));
            turrets.Add(turetDisp.GetTurret("Grenadiere"));
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

        public string GetInfos()
        {
            string s_return = "";
            for(int i = 0; i < turrets.Count; i++)
            {
                s_return = s_return + turrets[i].name + "\n";
            }
            return s_return;
        }
    }
}

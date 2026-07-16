using System.Collections.Generic;
using UnityEngine;



namespace Crab
{
    public class CrabStructure
    {
        protected int max_rss_carried_;
        protected List<RessourceType> rss_carried;
        protected int walking_speed_;
        protected int attack_;
        protected float attack_speed_;
        protected float health_;
        protected float area_vision_;

        protected (int, int) coordinates_;
        protected (int, int) coordinates_target_;

        protected bool is_triggered_;
        
        public RessourcePoint actual_point;
        

        public CrabStructure(int max_rss, int walking_speed, int attack, float attack_speed, float health,
            float area_vision, (int, int) coordinates)
        {
            max_rss_carried_ = max_rss;
            rss_carried = new List<RessourceType>();
            walking_speed_ = walking_speed;
            attack_ = attack;
            attack_speed_ = attack_speed;
            health_ = health;
            area_vision_ = area_vision;
            coordinates_ = coordinates;
            coordinates_target_ = (0,0);
            actual_point = null;
            is_triggered_ = false;
        }

        public List<RessourceType> getRssCarried() { return rss_carried; }
        
        public void setMaxRss(int new_max) { max_rss_carried_ = new_max; }

        public int getWalkingSpeed() { return walking_speed_; }
        public void setWalkingSpeed(int new_speed) { walking_speed_ = new_speed; }

        public int getAttack() { return attack_; }
        public void setAttack(int new_attack) { attack_ = new_attack; }

        public float getAttackSpeed() { return attack_speed_; }
        public void setAttackSpeed(float new_attack_speed) { attack_speed_ = new_attack_speed; }

        public float getHealth() { return health_; }
        public void setHealth(float new_health) { health_ = new_health; }

        public void takeDamage(int damage)
        {
            health_ -= damage;
            if (health_ < 0)
            {
                health_ = 0;
            }
        }

        public float getAreaVision() { return area_vision_; }
        public void setMaxAreaVision(float new_max) { area_vision_ = new_max; }

        public (int, int) getCoordinates() { return coordinates_; }
        public void setCoordinates(int x, int y) { coordinates_ = (x, y); }
        
        public (int, int) getCoordinatesTarget() { return coordinates_target_; }
        public void setCoordinatesTarget(int x, int y) { coordinates_target_ = (x, y); }
        
        public void addRessource()
        {
            for (int i = 0; i < max_rss_carried_; i++)
            {
                List<RessourceType> rss_gathered = actual_point.GetRandomRessource();
                
                rss_carried.Add(rss_gathered[0]);
                if (rss_gathered.Count > 1)
                {
                    rss_carried.Add(rss_gathered[1]);
                }
            }
        }

        public void changeActualPoint(RessourcePoint new_actual_point)
        {
            actual_point = new_actual_point;
        }

        public void moveToTarget(Transform target)
        {
            //do the movement behavior
            //Vector3.MoveTowards( ... , target.position, walking_speed_ * Time.deltaTime); //?
        }

        public void trigger()
        {
            is_triggered_ = true;
        }

        public void selectTarget()
        {
            //need to retrieve the clic of the user, to assign a target
        }
    }
}
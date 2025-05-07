using System.Collections.Generic;
using _Project.Scripts.City;

namespace _Project.Scripts.Card
{
    public static class AttackUtils
    {
        public static bool TryApplyAreaDamage(int damage, Player.Player ownerPlayer)
        {
            List<ConstructionData> attackConstructions = new List<ConstructionData>();
            List<ConstructionData> allConstructions = new List<ConstructionData>();

            foreach (var buildPlace in ownerPlayer.BuildPlaces)
            {
                allConstructions.Add(buildPlace.ConstructionData);
            }

            for (int i = 0; i < 3; i++)
            {
                if (allConstructions[i].Health > 0)
                {
                    attackConstructions.Add(allConstructions[i]);
                }
                else if (allConstructions[i + 3].Health > 0)
                {
                    attackConstructions.Add(allConstructions[i + 3]);
                }
                else
                {
                    attackConstructions.Add(ownerPlayer.MainBuildPlace.ConstructionData);
                }
            }

            foreach (var data in attackConstructions)
            {
                data.TakeDamage(damage / 3);
            }
            
            return true;
        }
        
        public static bool ApplyAccurateDamage()
        {
            return false;
        }
    }
}
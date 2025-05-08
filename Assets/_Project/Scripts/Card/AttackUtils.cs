using System.Collections.Generic;
using _Project.Scripts.City;
using UnityEngine;

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
        
        public static void VisualiseAreaDamage(Player.Player ownerPlayer)
        {
            List<BuildPlace> attackPlaces = new List<BuildPlace>();
            BuildPlace[] allPlaces = ownerPlayer.BuildPlaces;

            for (int i = 0; i < 3; i++)
            {
                if (allPlaces[i].ConstructionData.Health > 0)
                {
                    attackPlaces.Add(allPlaces[i]);
                }
                else if (allPlaces[i + 3].ConstructionData.Health > 0)
                {
                    attackPlaces.Add(allPlaces[i + 3]);
                }
                else
                {
                    attackPlaces.Add(ownerPlayer.MainBuildPlace);
                }
            }

            foreach (var place in attackPlaces)
            {
                place.SetOutlineState(true);
            }
        }
        
        public static bool TryApplyAccurateDamage(int damage, BuildPlace target, Player.Player ownerPlayer)
        {  
            if(target.ConstructionData.Health <= 0) return false;
            
            List<ConstructionData> firstLine = new List<ConstructionData>();
            List<ConstructionData> secondLine = new List<ConstructionData>();

            bool isNotFirstFull = false;
            bool isNotSecondFull = false;
            
            for (int i = 0; i < ownerPlayer.BuildPlaces.Length; i++)
            {
                if (target.Index == ownerPlayer.BuildPlaces[i].Index) continue;
                if (i < 3)
                {
                    if (ownerPlayer.BuildPlaces[i].ConstructionData.Health > 0)
                    {
                        firstLine.Add(ownerPlayer.BuildPlaces[i].ConstructionData);
                    }
                    else
                    {
                        isNotFirstFull = true;
                    }
                }
                else
                {
                    if (ownerPlayer.BuildPlaces[i].ConstructionData.Health > 0)
                    {
                        secondLine.Add(ownerPlayer.BuildPlaces[i].ConstructionData);
                    }
                    else
                    {
                        isNotSecondFull = true;
                    }
                }
            }

            if (target.Index < 3)
            {
                target.ConstructionData.TakeDamage(damage / 2);
                
                if (firstLine.Count > 0)
                {
                    foreach (var place in firstLine)
                    {
                        place.TakeDamage(damage / (firstLine.Count * 2));
                    }
                    
                    return true;
                }
                else
                {
                    foreach (var place in secondLine)
                    {
                        place.TakeDamage(damage / (secondLine.Count * 2));
                    }
                    
                    return true;
                }
            }
            else
            {
                if (isNotFirstFull)
                {
                    target.ConstructionData.TakeDamage(damage / 2);

                    if (firstLine.Count > 0)
                    {
                        foreach (var place in firstLine)
                        {
                            place.TakeDamage(damage / firstLine.Count);
                        }

                        return true;
                    }
                    else
                    {
                        foreach (var place in secondLine)
                        {
                            place.TakeDamage(damage / secondLine.Count);
                        }
                        
                        return true;
                    }
                }
            }
            
            return false;
        }
        
        public static void VisualiseAccurateDamage(BuildPlace target, Player.Player ownerPlayer)
        {
            if(target.ConstructionData.Health <= 0) return;
            
            List<BuildPlace> attackPlaces = new List<BuildPlace>();
            List<BuildPlace> firstLine = new List<BuildPlace>();
            List<BuildPlace> secondLine = new List<BuildPlace>();

            bool isNotFirstFull = false;
            bool isNotSecondFull = false;
            
            for (int i = 0; i < ownerPlayer.BuildPlaces.Length; i++)
            {
                if (target.Index == ownerPlayer.BuildPlaces[i].Index) continue;
                if (i < 3)
                {
                    if (ownerPlayer.BuildPlaces[i].ConstructionData.Health > 0)
                    {
                        firstLine.Add(ownerPlayer.BuildPlaces[i]);
                    }
                    else
                    {
                        isNotFirstFull = true;
                    }
                }
                else
                {
                    if (ownerPlayer.BuildPlaces[i].ConstructionData.Health > 0)
                    {
                        secondLine.Add(ownerPlayer.BuildPlaces[i]);
                    }
                    else
                    {
                        isNotSecondFull = true;
                    }
                }
            }

            if (target.Index < 3)
            {
                attackPlaces.Add(target);
                
                if (firstLine.Count > 0)
                {
                    foreach (var place in firstLine)
                    {
                        attackPlaces.Add(place);
                    }
                }
                else
                {
                    foreach (var place in secondLine)
                    {
                        attackPlaces.Add(place);
                    }
                }
            }
            else
            {
                if (ownerPlayer.BuildPlaces[target.Index - 3].ConstructionData.Health <= 0)
                {
                    attackPlaces.Add(target);

                    if (firstLine.Count > 0)
                    {
                        foreach (var place in firstLine)
                        {
                            attackPlaces.Add(place);
                        }
                    }
                    else
                    {
                        foreach (var place in secondLine)
                        {
                            attackPlaces.Add(place);
                        }
                    }
                }
            }
            
            foreach (var place in attackPlaces)
            {
                place.SetOutlineState(true);
            }
        }
    }
}
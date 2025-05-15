using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.City;

namespace _Project.Scripts.Card
{
    public static class AttackUtils
    {
        public static bool TryApplyAreaDamage(int damage, Player ownerPlayer, bool isVisual)
        {
            List<BuildPlace> attackPlaces = new List<BuildPlace>();
            int lineLenght = ownerPlayer.BuildLines[0].Places.Length;
            
            for (int i = 0; i < lineLenght; i++)
            {
                attackPlaces.Add(GetFirstWholeInHorizontal(ownerPlayer.BuildLines, i));
            }

            if (isVisual)
            {
                foreach (var place in attackPlaces)
                {
                    place.SetOutlineState(true);
                }
            }
            else
            {
                foreach (var place in attackPlaces)
                {
                    place.ConstructionData.TakeDamage(damage / attackPlaces.Count);
                }
            }
            
            return true;
        }
        
        public static bool TryApplyAccurateDamage(int damage, BuildPlace target, Player ownerPlayer, bool isVisual)
        {  
            if(target.ConstructionData.Health <= 0) return false;

            List<BuildPlace> attackPlaces = new List<BuildPlace>();
            int targetHorizontal = 0;

            foreach (var line in ownerPlayer.BuildLines)
            {
                for (var i = 0; i < line.Places.Length; i++)
                {
                    if (line.Places[i].Index == target.Index)
                    {
                        targetHorizontal = i;
                        break;
                    }
                }
            }

            if (GetFirstWholeInHorizontal(ownerPlayer.BuildLines, targetHorizontal).Index == target.Index)
            {
                attackPlaces.Add(target);

                var attackLine = GetAllWholeInLine(GetFirstWholeLine(ownerPlayer.BuildLines, target.Index), target.Index);
                attackPlaces.AddRange(attackLine);
            }
            
            if (isVisual)
            {
                foreach (var place in attackPlaces)
                {
                    place.SetOutlineState(true);
                }
                
                return false;
            }

            foreach (var place in attackPlaces)
            {
                if (place.Index == target.Index)
                {
                    place.ConstructionData.TakeDamage(damage / 2);
                }
                else
                {
                    place.ConstructionData.TakeDamage(damage / ((attackPlaces.Count - 1) * 2));
                }
            }
                
            return true;
        }

        public static bool TryApplyLayerDamage(int damage, BuildPlace target, Player ownerPlayer, bool isVisual)
        {
            if (target.ConstructionData.Health <= 0) return false;
            List<BuildPlace> attackPlaces = new List<BuildPlace>();
            int targetLine = 0;
            int targetHorizontal = 0;

            for (var i = 0; i < ownerPlayer.BuildLines.Length; i++)
            {
                for (var j = 0; j < ownerPlayer.BuildLines[i].Places.Length; j++)
                {
                    if (ownerPlayer.BuildLines[i].Places[j].Index == target.Index)
                    {
                        targetLine = i;
                        targetHorizontal = j;
                        break;
                    }
                }
            }

            if (GetFirstWholeInHorizontal(ownerPlayer.BuildLines, targetHorizontal).Index == target.Index)
            {
                attackPlaces.AddRange(ownerPlayer.BuildLines[targetLine].Places);
            }

            if (isVisual)
            {
                foreach (var place in attackPlaces)
                {
                    place.SetOutlineState(true);
                }
            }
            else
            {
                foreach (var place in attackPlaces)
                {
                    place.ConstructionData.TakeDamage(damage / attackPlaces.Count);
                }
            }
            
            return true;
        }
        
        private static BuildPlace GetFirstWholeInHorizontal(Player.BuildLine[] lines, int index)
        {
            for (var i = 0; i < lines.Length; i++)
            {
                if (i == lines.Length)
                {
                    return lines[i].Places[0];
                }
                
                if (lines[i].Places[index].ConstructionData.Health > 0)
                {
                    return lines[i].Places[index];
                }
            }

            return null;
        }

        private static List<BuildPlace> GetAllWholeInLine(Player.BuildLine line, int ignore = -1)
        {
            return line.Places.Where(place => place.ConstructionData.Health > 0 && place.Index != ignore).ToList();
        }
        
        private static Player.BuildLine GetFirstWholeLine(Player.BuildLine[] lines, int ignore = -1)
        {
            foreach (var line in lines)
            {
                foreach (var place in line.Places)
                {
                    if (place.Index != ignore && place.ConstructionData.Health > 0)
                    {
                        return line;
                    }
                }
            }

            return null;
        }
    }
}
using System;

namespace _Project.Scripts.City
{
    [Serializable]
    public struct CityResources
    {
        public int Wood;
        public int Stone;
        public int Metal;
        
        public CityResources(int wood, int stone, int metal)
        {
            Wood = wood;
            Stone = stone;
            Metal = metal;
        }

        public static CityResources operator +(CityResources a, CityResources b)
        {
            return new CityResources(a.Wood + b.Wood, a.Stone + b.Stone, a.Metal + b.Metal);
        }
        
        public static CityResources operator -(CityResources a, CityResources b)
        {
            return new CityResources(a.Wood - b.Wood, a.Stone - b.Stone, a.Metal - b.Metal);
        }
    }
}
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class RessourcePoint : MonoBehaviour
{
    public RessourcePointData data;

    public List<RessourceType> GetRandomRessource()
    {
        List<RessourceType> list = new List<RessourceType>();
        list.Add(data.basicRessource);
        
        if (CriticalChance() == true)
        {
            Debug.Log("Critical Chance !");
            list.Add(data.specialRessource);
        }
        return list;
    }

    public bool CriticalChance()
    {
        float random = Random.Range(0f,1f);
        return random < data.specialDropChance;
    }
}

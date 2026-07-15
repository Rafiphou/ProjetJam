using UnityEngine;

[CreateAssetMenu(fileName = "RessourcePoint", menuName = "Scriptable Objects/RessourcePoint")]
public class RessourcePointData : ScriptableObject
{
    public string ressourcePointName;
    public RessourceType basicRessource;
    public RessourceType specialRessource;
    public float specialDropChance;
}

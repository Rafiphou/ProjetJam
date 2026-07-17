using UnityEngine;

public class DefensePoint : MonoBehaviour
{
    public GameObject defensePointBase;
    public GameObject defensePointActive;

    public bool isOccupied = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defensePointBase.SetActive(true);
        defensePointActive.SetActive(false);
    }

    private void OnMouseOver()
    {
        defensePointBase.SetActive(false);
        defensePointActive.SetActive(true);
    }

    private void OnMouseExit()
    {
        defensePointBase.SetActive(true);
        defensePointActive.SetActive(false);
    }
}

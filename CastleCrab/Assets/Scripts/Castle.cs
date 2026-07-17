using UnityEngine;

public class Castle : MonoBehaviour
{
    public Canvas castleCanvas;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        castleCanvas.enabled = false;
    }

    void OnMouseDown()
    {
        castleCanvas.enabled = !castleCanvas.enabled;
    }

    public void CloseCanvas()
    {
        castleCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

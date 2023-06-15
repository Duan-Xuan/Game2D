using UnityEngine;

public class LoadScrens : MonoBehaviour
{
    public void MenuScrens()
    {
        Application.LoadLevel("Menu");
    }
    
    public void StartScrens()
    {
        Application.LoadLevel("Play1");
    }
    
    public void QuitScrens()
    {
        Application.Quit();
    }
}
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    private AudioSource music;
    private bool isShow, isMuSic;
    private string Mute = "Music: Turn on";
    public GUIStyle btnPlSt, btnMusic ,btnExit;
    public GUISkin skin;

    void Start()
    {
        music = GetComponent<AudioSource>();
        Time.timeScale = 1;
    }

    void OnGUI()
    {
        GUI.skin = skin;
        if (GUI.Button(new Rect(20, 20, 50, 50),""))
        {
            isShow = !isShow;
            Time.timeScale = 1;
        }

        if (isShow)
        {
            Time.timeScale = 0;
            GUI.Box(new Rect((Screen.width - 350) / 2, (Screen.height - 350) / 2, 350, 350), "MENU");
            if (GUI.Button(new Rect((Screen.width-230)/2, (Screen.height-50)/2, 150, 50), Mute, btnMusic))
            {
                if (isMuSic)
                {
                    isMuSic = !isMuSic;
                    music.mute = false;
                    Mute = "Music: Turn on";
                }
                else
                {
                    isMuSic = !isMuSic;
                    music.mute = true;
                    Mute = "Music: Turn off";
                }
            }
            if (GUI.Button(new Rect((Screen.width+240)/2, (Screen.height-100)/2, 30, 30), "", btnPlSt))
            {
                Time.timeScale = 1;
                isShow = !isShow;
            }
            if (GUI.Button(new Rect((Screen.width+240)/2, (Screen.height+50)/2, 30, 30), "", btnExit))
            {
                Application.LoadLevel("Menu");
            }
        }
    }
}
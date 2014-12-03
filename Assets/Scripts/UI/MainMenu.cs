using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{

	public void startGame()
    {
        Application.LoadLevel("World");
    }
    public void exit()
    {
        Application.Quit();
    }
}

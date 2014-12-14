using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class InfoWindow : MonoBehaviour 
{
    public GameObject infoWindowPanel;
    public Text titleField;
    public Text textField;
    public Image icon;

	void Start ()
    {
        infoWindowPanel.SetActive(false);
	}
	
	void Update () 
    {
        
	}

    public void showInfo(string title, string textContent,Sprite icon)
    {
        titleField.text = title;
        textField.text = textContent;
        this.icon.sprite = icon;
        infoWindowPanel.SetActive(true);
        GameManager.getInstance().stopTheTime = true;
    }
}

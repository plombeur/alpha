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
        hideInfoPanel();
	}
	
	void Update () 
    {
        
	}

    public void showInfo(string title, string textContent,Sprite icon)
    {
        titleField.text = title;
        textField.text = textContent;
        this.icon.sprite = icon;
       // showInfoPanel();
        GameManager.getInstance().stopTheTime = true;
    }

    public void hideInfoPanel()
    {
        infoWindowPanel.SetActive(false);
    }
    public void showInfoPanel()
    {
       // infoWindowPanel.SetActive(true);
    }
}

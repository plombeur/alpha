using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ObjectifWindow : MonoBehaviour 
{
    public GameObject objectifWindowPanel;
    public GameObject objectifMiniWindow;
    public Text titleField;
    public Text textField;
    public Text resumeTextField;

    public Text miniWindowTitleField;
    public Text miniWindowResumeField;

	void Start ()
    {
        hideObjectifWindow();
        hideObjectifMiniWindow();
	}
	
	void Update () 
    {
       
	}

    public void setObjectif(string title, string textContent,string resume)
    {
        if (title == null || textContent == null || resume == null)
        {
            objectifMiniWindow.SetActive(false);
            objectifWindowPanel.SetActive(false);
            GameManager.getInstance().stopTheTime = false;
            return;
        }
        titleField.text = title;
        resumeTextField.text = resume;
        textField.text = textContent;

        miniWindowTitleField.text = title;
        miniWindowResumeField.text = resume;
        showObjectifWindow();
        showObjectifMiniWindow();
    }
    public void showObjectifWindow()
    {
        objectifWindowPanel.SetActive(true);
        GameManager.getInstance().stopTheTime = true;
    }
    public void hideObjectifWindow()
    {
        objectifWindowPanel.SetActive(false);
        GameManager.getInstance().stopTheTime = false;
    }
    public void showObjectifMiniWindow()
    {
        objectifMiniWindow.SetActive(true);
    }
    public void hideObjectifMiniWindow()
    {
        objectifMiniWindow.SetActive(false);
    }
}

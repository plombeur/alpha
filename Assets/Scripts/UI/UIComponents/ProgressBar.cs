using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public class ProgressBar : MonoBehaviour 
{
    public RectTransform objectBar;
    public float progress = 100;
    public float padLeft = 5, padRight = 5;
    public float padTop = 5, padBottom = 5;
	
	void Update () 
    {
        updateProgressBar();
	}

    public void updateProgressBar()
    {
        RectTransform thisRect = GetComponent<RectTransform>();
        RectTransform rect = objectBar.GetComponent<RectTransform>();

        Vector2 size;
        size.x = (thisRect.sizeDelta.x- (padRight+padLeft)) * Mathf.Clamp(progress / 100, 0, 1);
        size.y = thisRect.sizeDelta.y - (padTop+padBottom);
        rect.sizeDelta = size;
        rect.localPosition = new Vector2(padLeft, padBottom);
    }
    
}

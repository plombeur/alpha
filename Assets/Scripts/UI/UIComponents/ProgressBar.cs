using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public class ProgressBar : MonoBehaviour 
{
    public RectTransform objectBar;
    public float progress = 100;
    public float padWidth = 5, padHeight = 5;
	
	void Update () 
    {
        updateProgressBar();
	}

    public void updateProgressBar()
    {
        RectTransform thisRect = GetComponent<RectTransform>();
        RectTransform rect = objectBar.GetComponent<RectTransform>();

        Vector2 size;
        size.x = (thisRect.sizeDelta.x- (2 * padWidth)) * Mathf.Clamp(progress / 100, 0, 1);
        size.y = thisRect.sizeDelta.y - (2 * padHeight);
        rect.sizeDelta = size;
        rect.localPosition = new Vector2(padWidth, padHeight);
    }
    
}

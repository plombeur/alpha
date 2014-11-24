using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public class ProgressBar : MonoBehaviour 
{
    public GameObject objectBar;
    public float progress = 100;
    public float width = 200, height = 50;
    public float padWidth = 5, padHeight = 5;
	
	void Update () 
    {
        updateProgressBar();
	}

    public void updateProgressBar()
    {
        RectTransform rect = objectBar.GetComponent<RectTransform>();

        Vector2 size;
        size.x = (width-(2*padWidth)) * Mathf.Clamp(progress / 100, 0, 1);
        size.y = height - (2 * padHeight);
        rect.sizeDelta = size;
        rect.localPosition = new Vector2(padWidth, padHeight);
    }
    
}

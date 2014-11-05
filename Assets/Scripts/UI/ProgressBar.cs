using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour 
{
    public GameObject objectBar;
    public float progress;
    private float originalWidth;

	void Start () 
    {
        this.originalWidth = objectBar.GetComponent<RectTransform>().rect.width;
	}
	
	void Update () 
    {
        Vector2 size = objectBar.GetComponent<RectTransform>().sizeDelta;
        size.x = originalWidth * Mathf.Clamp(progress / 100, 0, 1);
        objectBar.GetComponent<RectTransform>().sizeDelta = size;
	}
}

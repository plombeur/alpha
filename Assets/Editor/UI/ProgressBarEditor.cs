using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[CustomEditor(typeof(ProgressBar))]
public class ProgressBarEditor : Editor
{

    public override void OnInspectorGUI()
    {
        GUI.changed = false;

        ProgressBar myTarget = (ProgressBar)target;

        myTarget.objectBar = EditorGUILayout.ObjectField("BarObject",myTarget.objectBar, typeof(RectTransform)) as RectTransform;

        if (myTarget.GetComponent<Image>() == null)
            myTarget.gameObject.AddComponent<Image>().color = new Color(0.2f,0.2f,0.2f,0.3f);

        if (myTarget.objectBar == null)
        {
            GameObject objectBar = new GameObject("ProgressPart");
            RectTransform rectObjectBar = objectBar.AddComponent<RectTransform>();
            myTarget.objectBar = rectObjectBar;
            myTarget.objectBar.transform.parent = myTarget.transform;
            rectObjectBar.pivot = new Vector2(0, 0);
            rectObjectBar.anchorMin = new Vector2();
            rectObjectBar.anchorMax = new Vector2();
            objectBar.AddComponent<Image>();

        }

        RectTransform rect = myTarget.GetComponent<RectTransform>();

        myTarget.progress = Mathf.Clamp(EditorGUILayout.FloatField("Progress", myTarget.progress),0,100);
        myTarget.padLeft = Mathf.Clamp(EditorGUILayout.FloatField("Pad Left", myTarget.padLeft), 0, rect.sizeDelta.x / 2);
        myTarget.padRight = Mathf.Clamp(EditorGUILayout.FloatField("Pad Right", myTarget.padRight), 0, rect.sizeDelta.x / 2);
        myTarget.padTop = Mathf.Clamp(EditorGUILayout.FloatField("Pad Top", myTarget.padTop), 0, rect.sizeDelta.y / 2);
        myTarget.padBottom = Mathf.Clamp(EditorGUILayout.FloatField("Pad Bottom", myTarget.padBottom), 0, rect.sizeDelta.y / 2);
        //myTarget.width = Mathf.Max(EditorGUILayout.FloatField("Width", myTarget.width), myTarget.padWidth * 2);
        //myTarget.height = Mathf.Max(EditorGUILayout.FloatField("Height", myTarget.height), myTarget.padHeight * 2);

       

        myTarget.updateProgressBar();

        if (GUI.changed)
            EditorUtility.SetDirty(myTarget);
    }
    [MenuItem("GameObject/UI/ProgressBar")]
    public static void createProgressBarObject()
    {
        GameObject progressBar = new GameObject("ProgressBar");
        progressBar.AddComponent<ProgressBar>();
    }
}

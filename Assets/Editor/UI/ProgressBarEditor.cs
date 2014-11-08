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

        if (myTarget.GetComponent<Image>() == null)
            myTarget.gameObject.AddComponent<Image>().color = new Color(0.2f,0.2f,0.2f,0.3f);

        if (myTarget.objectBar == null)
        {
            myTarget.objectBar = new GameObject("ProgressPart");
            myTarget.objectBar.transform.parent = myTarget.transform;
            RectTransform rectObjectBar = myTarget.objectBar.AddComponent<RectTransform>();
            rectObjectBar.pivot = new Vector2(0, 0);
            rectObjectBar.anchorMin = new Vector2();
            rectObjectBar.anchorMax = new Vector2();
            myTarget.objectBar.AddComponent<Image>();

        }


        myTarget.progress = Mathf.Clamp(EditorGUILayout.FloatField("Progress", myTarget.progress),0,100);
        myTarget.padWidth = Mathf.Clamp(EditorGUILayout.FloatField("Pad Width", myTarget.padWidth), 0,myTarget.width/2);
        myTarget.padHeight = Mathf.Clamp(EditorGUILayout.FloatField("Pad Height", myTarget.padHeight), 0, myTarget.height / 2);
        myTarget.width = Mathf.Max(EditorGUILayout.FloatField("Width", myTarget.width), myTarget.padWidth * 2);
        myTarget.height = Mathf.Max(EditorGUILayout.FloatField("Height", myTarget.height), myTarget.padHeight * 2);

        RectTransform rect = myTarget.GetComponent<RectTransform>();
        if (rect.pivot.x != 0 || rect.pivot.y != 0)
            rect.pivot = new Vector2();
        rect.sizeDelta = new Vector2(myTarget.width, myTarget.height);

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

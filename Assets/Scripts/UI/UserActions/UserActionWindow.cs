using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserActionWindow : MonoBehaviour 
{
    public void showUserActionWindow(UserAction[] actions,Vector3 screenPosition)
    {
        while (transform.childCount > 0)
            Destroy(transform.GetChild(0));

        foreach (UserAction a in actions)
        {
            GameObject userActionButton = new GameObject(a.getActionLabel());
            userActionButton.AddComponent<Button>().onClick.AddListener(() =>
            {
                a.executeAction();
                gameObject.SetActive(false);
            });
            userActionButton.AddComponent<Text>().text = a.getActionLabel();
        }
    }
}

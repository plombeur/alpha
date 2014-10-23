using UnityEngine;
using System.Collections;

public class Emoticon : MonoBehaviour {

    public float duration = 3;
    public float longueurParcourue = 1.5f;
    private float timeDisplay = 0;
    private float timeTotal = 0;
    public bool staticEmoticon = false;
    private EmoticonSystem emoticonSys;

	// Use this for initialization
    void Start()
    {
        Vector3 position = emoticonSys.GetComponent<Transform>().position;
        position.y += emoticonSys.hauteurAffichageEmoticon;
        GetComponent<Transform>().position = position;
	}
	
    public void setEmoticonSystem(EmoticonSystem emoSys)
    {
        emoticonSys = emoSys;
    }

	// Update is called once per frame
	void Update () {

        timeDisplay += Time.deltaTime;

        if (staticEmoticon)
        {
            while (timeDisplay >= 0.04f)
            {
                timeDisplay -= 0.04f;
                Vector3 position = emoticonSys.GetComponent<Transform>().position;
                position.y += emoticonSys.hauteurAffichageEmoticon;
                GetComponent<Transform>().position = position;
            }
        }
        else
        {
            timeTotal += Time.deltaTime;

            if (timeTotal >= duration)
            {
                GameObject.Destroy(this.gameObject);
            }
            else if (timeDisplay >= 0.04f)
            {
                timeDisplay -= 0.04f;
                Vector3 position = emoticonSys.GetComponent<Transform>().position;
                position.y += emoticonSys.hauteurAffichageEmoticon + longueurParcourue * (timeTotal / duration);
                GetComponent<Transform>().position = position;
                Color color = GetComponent<SpriteRenderer>().color;
                color.a = 1 - timeTotal / duration;
                GetComponent<SpriteRenderer>().color = color;
            }
        }
	}
}

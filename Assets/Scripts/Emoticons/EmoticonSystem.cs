using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmoticonSystem : MonoBehaviour {

    public GameObject prefabEmoticon;
    public float hauteurAffichageEmoticon = 5;

    private List<Sprite> list = new List<Sprite>();
    private Sprite emoticonFixe;
    private float durationFixe;
    private GameObject tmpEmoticonFixe;
    private Vector2 tmpPosition;

	// Use this for initialization
	void Start () {
	
	}
	
    public void setAnimal(Animal animal)
    {
    }

	// Update is called once per frame
	void Update () {
	    if(list.Count > 0)
        {
            GameObject obj = (GameObject)Instantiate(prefabEmoticon, GetComponent<Transform>().position, Quaternion.identity);
            obj.GetComponent<SpriteRenderer>().sprite = list[0];
            obj.GetComponent<Emoticon>().setEmoticonSystem(this);
            list.RemoveAt(0);
        }
        if(emoticonFixe!=null)
        {
            GameObject obj = (GameObject)Instantiate(prefabEmoticon, GetComponent<Transform>().position, Quaternion.identity);
            obj.GetComponent<SpriteRenderer>().sprite = emoticonFixe;
            obj.GetComponent<Emoticon>().staticEmoticon = true;
            obj.GetComponent<Emoticon>().setEmoticonSystem(this);
            emoticonFixe = null;
            tmpEmoticonFixe = obj;
        }
	}

    public void displayAnimatedEmoticon(Sprite sprite)
    {
        list.Add(sprite);
    }

    public void displayStaticEmoticon(Sprite sprite)
    {
        GameObject.Destroy(tmpEmoticonFixe);
        emoticonFixe = sprite;
    }

    public void hideStaticEmoticon()
    {
        GameObject.Destroy(tmpEmoticonFixe);
    }
}

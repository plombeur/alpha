using UnityEngine;
using System.Collections;

public class VectorDisplayer : MonoBehaviour {

    public GameObject red;
    public GameObject blue;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void setBluePosition(Vector2 position)
    {
        blue.transform.position = position + (Vector2)transform.position;
    }

    public void setRedPosition(Vector2 position)
    {
        red.GetComponent<SpriteRenderer>().enabled = true;
        red.transform.position = position + (Vector2)transform.position;
    }

    public void hideRedVector()
    {
        red.GetComponent<SpriteRenderer>().enabled = false;
    }
}

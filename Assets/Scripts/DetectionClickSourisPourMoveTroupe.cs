using UnityEngine;
using System.Collections;

public class DetectionClickSourisPourMoveTroupe : MonoBehaviour {
    
    /*
     * 
     * 
     *    SCRIPT DE TEST PAS DU TOUT OPTIMISE ET TOUT PAS BEAU !!!!! (!) (!)
     *    
     */

    private LoupAlpha alpha;

	// Use this for initialization
	void Start () {
        alpha = GameObject.Find("LoupAlpha").GetComponent<LoupAlpha>();
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Coucou!");
        if(Input.GetMouseButtonDown(1))
            ((MindLoupAlpha)alpha.mind).addActionUserAction(new AU_MoveTo(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
	}
}

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
    public float vitesseScrollBorderMap = 1;

	// Use this for initialization
	void Start () {
        alpha = GameObject.Find("LoupAlpha").GetComponent<LoupAlpha>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 positionSourisScreen = (Vector2)Input.mousePosition - new Vector2(Screen.width / 2, Screen.height / 2);
        if(Mathf.Abs(positionSourisScreen.x) > Screen.width / 2 || Mathf.Abs(positionSourisScreen.y) > Screen.height / 2 )
        {
            Vector3 positionCamera = transform.position;
            if (positionSourisScreen.x < -Screen.width / 2)
                positionCamera.x -= Time.deltaTime * vitesseScrollBorderMap;
            else if (positionSourisScreen.x > Screen.width / 2)
                positionCamera.x += Time.deltaTime * vitesseScrollBorderMap;
            if (positionSourisScreen.y < -Screen.height / 2)
                positionCamera.y -= Time.deltaTime * vitesseScrollBorderMap;
            else if(positionSourisScreen.y > Screen.height / 2)
                positionCamera.y += Time.deltaTime * vitesseScrollBorderMap;
            transform.position = positionCamera;
        }

        if(Input.GetMouseButtonDown(1))
            ((MindLoupAlpha)alpha.mind).addActionUserAction(new AU_MoveTo(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
	}
}

using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {

    public string nomDeLaCible;
    private GameObject alphaWolf;
    private float time = 0;

    // Use this for initialization
    void Start()
    {
        alphaWolf = GameObject.Find(nomDeLaCible);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        while (time >= 0.04f)
        {
            Vector3 positionAlpha = alphaWolf.GetComponent<Transform>().position;
            Transform transf = GetComponent<Transform>();
            positionAlpha.z -= 10;
            transf.position = positionAlpha;
            time -= 0.04f;
        }
    }
}

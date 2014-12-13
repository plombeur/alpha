using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {

    public GameObject target;
    private float time = 0;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            time += Time.deltaTime;
            while (time >= 0.04f)
            {
                Vector3 position = target.GetComponent<Transform>().position;
                Transform transf = GetComponent<Transform>();
                position.z -= 10;
                transf.position = position;
                time -= 0.04f;
            }
        }
    }
}

using UnityEngine;
using System.Collections;

public class TipTest : ToolTip {
    float timer;

    // Use this for initialization
    void Start()
    {
        timer = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        checkTrigger();
    }

    protected override void checkTrigger()
    {
        if (timer <= 0)
        {
            display();
            enabled = false;
        }
    }
}

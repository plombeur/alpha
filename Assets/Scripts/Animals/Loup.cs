using UnityEngine;
using System.Collections;

public class Loup : Animal {

    protected override void onCreate()
    {
        if (Living.DEBUG)
            Debug.Log("Loup.Start");
        MindLoup mind = new MindLoup(this);
        base.construct(mind,100);
	}
}
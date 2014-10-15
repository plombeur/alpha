using UnityEngine;
using System.Collections;

public class Vegetal : Living {
	
    void Start()
    {
        MindVegetal mind = new MindVegetal();
        base.construct(mind);
    }

}
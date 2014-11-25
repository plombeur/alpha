using UnityEngine;
using System.Collections;

public class Terre : Patch
{
    // Use this for initialization
    protected void Start()
    {
        maxNutriments = 50;
        nutriments = maxNutriments;
        temperature = 15;
        passabilite = 1;
        evaporateSpeed = 1;
    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();
    }
}

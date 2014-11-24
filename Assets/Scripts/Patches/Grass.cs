using UnityEngine;
using System.Collections;

public class Grass : Patch
{
    // Use this for initialization
    protected void Start()
    {
        maxNutriments = 50;
        nutriments = maxNutriments;
        temperature = 15;
        passabilite = 1;
        evaporateSpeed = 0.5f;
    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();
    }
}

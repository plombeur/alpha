using UnityEngine;
using System.Collections;

public abstract class Plant : Living
{
    protected int vie;
    public void construct(MindPlant mind, int vie)
    {
        if (Living.DEBUG)
            Debug.Log("Plant.construct");
        this.vie = vie;
        base.construct(mind);
    }

    public void grow()
    {

    }
}

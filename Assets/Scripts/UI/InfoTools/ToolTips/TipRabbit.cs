using UnityEngine;
using System.Collections;

public class TipRabbit : ToolTip
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void checkTrigger()
    {

    }

    protected override void checkMemoryModificationTrigger(MemoryBloc bloc)
    {
        Animal lapin = bloc.getEntity() as Animal;
        if (lapin != null)
        {
            display();
        }
    }
}

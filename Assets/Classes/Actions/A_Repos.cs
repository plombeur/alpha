using UnityEngine;
using System.Collections;

public class A_Repos : Action {

    private float duree;
    private float time = 0;

    public A_Repos(float duree) : base("A_Repos")
    {
        this.duree = duree;
    }

    public override float getPriority()
    {
        return .1f;
    }

    protected override bool onUpdate(float deltaTime)
    {
        if (Living.DEBUG)
            Debug.Log("Repos " + time);
        time += deltaTime;
        if(time >= duree)
        {
            getAnimal().reveil();
            return false;
        }
        getAnimal().dors();
        return true;
    }

    public override bool Equals(System.Object obj)
    {
        if (obj == null)
        {
            return false;
        }

        A_Promenade action = obj as A_Promenade;
        return action != null;
    }
}

using UnityEngine;
using System.Collections;

public abstract class A_ActionUser : Action
{
    public A_ActionUser(string name)
        : base(name)
    {}

    public override float getPriority()
    {
        return 100f;
    }

}

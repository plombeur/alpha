using UnityEngine;
using System.Collections;

public abstract class Mind {

    protected Living agent;
    public Mind(Living agent)
    {
        this.agent = agent;
    }
    public abstract void vivre();
}
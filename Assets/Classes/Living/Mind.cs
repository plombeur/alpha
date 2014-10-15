using UnityEngine;
using System.Collections;

public abstract class Mind {

    private Living agent;
    public Mind(Living agent)
    {
        this.agent = agent;
    }
    public abstract void vivre();
}
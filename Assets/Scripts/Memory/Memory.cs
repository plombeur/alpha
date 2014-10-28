using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Memory : MonoBehaviourAdapter
{
    private PerceptView perceptView;
    private Dictionary<Identity, MemoryBloc> memoryBlocs;

    protected override void Start()
    {
        memoryBlocs = new Dictionary<Identity, MemoryBloc>();
        perceptView = GetComponentInChildren<PerceptView>();
    }

    protected override void Update()
    {
        foreach (MemoryBloc bloc in memoryBlocs.Values)
            bloc.update(Time.deltaTime);

        if (perceptView != null)
        {
            foreach (Living living in perceptView.getLiving())
            {
                if (memoryBlocs.ContainsKey(living.getIdentity()))
                {
                    memoryBlocs[living.getIdentity()].updatePosition(living.transform.position);
                }
                else
                    memoryBlocs.Add(living.getIdentity(), new MemoryBloc(living.getIdentity()));
            }
        }
    }
    public MemoryBloc getMemoryForIdentity(Identity identity)
    {
        MemoryBloc bloc;
        memoryBlocs.TryGetValue(identity, out bloc);
        return bloc;
    }
    public Dictionary<Identity, MemoryBloc>.ValueCollection getMemoyBlocs()
    {
        return memoryBlocs.Values;
    }
}

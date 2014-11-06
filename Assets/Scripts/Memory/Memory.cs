using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Memory : MonoBehaviourAdapter
{
    private PerceptView perceptView;
    private Dictionary<Identity, MemoryBloc> memoryBlocs;
    private List<MemoryListener> listeners;

    public Memory()
    {
        listeners = new List<MemoryListener>();
    }
    public void addMemoryListener(MemoryListener listener)
    {
        listeners.Add(listener);
    }
    public void removeMemoryListener(MemoryListener listener)
    {
        listeners.Remove(listener);
    }
    public void addMemoryBloc(MemoryBloc bloc)
    {
        memoryBlocs.Add(bloc.getIdentity(), bloc);
        foreach (MemoryListener listener in listeners)
            listener.onMemoryAdd(this, bloc);
    }
    public void removeMemoryBloc(MemoryBloc bloc)
    {
        if (memoryBlocs.Remove(bloc.getIdentity()))
            foreach (MemoryListener listener in listeners)
                listener.onMemoryRemove(this, bloc);
    }
    public bool containIdentity(Identity identity)
    {
        return memoryBlocs.ContainsKey(identity);   
    }
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
                if (containIdentity(living.getIdentity()))
                    memoryBlocs[living.getIdentity()].updatePosition(living.transform.position);
                else
                    addMemoryBloc(new MemoryBloc(living.getIdentity()));
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

public interface MemoryListener
{
    public void onMemoryAdd(Memory memory, MemoryBloc bloc);
    public void onMemoryRemove(Memory memory, MemoryBloc bloc);
}
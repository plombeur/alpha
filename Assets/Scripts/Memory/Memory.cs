using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Memory : MonoBehaviourAdapter
{
    public const float MEMORY_CHECK_INTERVAL = 1;

    private PerceptView perceptView;
    private Dictionary<Identity, MemoryBloc> memoryBlocs;
    private List<MemoryListener> listeners;
    private float timerMemoryCheck = MEMORY_CHECK_INTERVAL;

    private List<MemoryListener> addListListener = new List<MemoryListener>(), removeListListener = new List<MemoryListener>();

    public Memory()
    {
        listeners = new List<MemoryListener>();
    }
    public void addMemoryListener(MemoryListener listener)
    {
        addListListener.Add(listener);
    }
    public void removeMemoryListener(MemoryListener listener)
    {
        removeListListener.Add(listener);
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
    private void memoryCheck()
    {
        if (perceptView != null)
        {
            List<MemoryBloc> blocsToRemove = new List<MemoryBloc>();
            foreach (MemoryBloc bloc in memoryBlocs.Values)
            {
                if (perceptView.isInFieldOfView(bloc.getLastPosition()))
                {
                    if (bloc.getEntity() == null)
                        blocsToRemove.Add(bloc);
                    else
                    {
                        Living living = bloc.getEntity() as Living;
                        if (!perceptView.getLiving().Contains(living))
                            blocsToRemove.Add(bloc);
                    }
                }
            }
            foreach (MemoryBloc bloc in blocsToRemove)
                removeMemoryBloc(bloc);
        }
    }
    protected override void Start()
    {
        memoryBlocs = new Dictionary<Identity, MemoryBloc>();
        perceptView = GetComponentInChildren<PerceptView>();
    }

    protected override void Update()
    {
        foreach (MemoryListener listener in addListListener)
            listeners.Add(listener);
        foreach (MemoryListener listener in removeListListener)
            listeners.Remove(listener);

        addListListener.Clear();
        removeListListener.Clear();

        timerMemoryCheck -= Time.deltaTime;
        if (timerMemoryCheck <= 0)
        {
            memoryCheck();
            timerMemoryCheck = MEMORY_CHECK_INTERVAL;
        }

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
    void onMemoryAdd(Memory memory, MemoryBloc bloc);
    void onMemoryRemove(Memory memory, MemoryBloc bloc);
}
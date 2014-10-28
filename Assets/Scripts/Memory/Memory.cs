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
      /*  if (perceptView != null)
        {
            foreach (Living living in perceptView.getLiving())
            {
                if (memoryBlocs.ContainsKey(living.getIdentity()))
                {
                }
            }
        }*/
    }
}

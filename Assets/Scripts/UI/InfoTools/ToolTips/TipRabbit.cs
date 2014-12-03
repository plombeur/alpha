using UnityEngine;
using System.Collections;

public class TipRabbit : ToolTip
{
    void Start()
    {
        Memory memoryAlpha = mManager.Alpha.GetComponent<Memory>();
        if (memoryAlpha == null)
        {
            Debug.Log("pas de mémoire du loup alpha");
            Destroy(this.gameObject);
        }
        memoryAlpha.addMemoryListener(this);
    }
    protected override void checkTrigger()
    {
        
    }

    protected override void checkMemoryModificationTrigger(MemoryBloc bloc)
    {
        Animal lapin = bloc.getEntity() as Animal;
        if (lapin != null && (lapin as Loup) == null)
        {
            display();
        }
    }
}

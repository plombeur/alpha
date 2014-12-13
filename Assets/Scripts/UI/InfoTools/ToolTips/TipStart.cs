using UnityEngine;
using System.Collections;

public class TipStart : ToolTip {

    protected override void checkTrigger()
    {
        display();
    }

    protected override void checkMemoryModificationTrigger(MemoryBloc bloc)
    {

    }
}

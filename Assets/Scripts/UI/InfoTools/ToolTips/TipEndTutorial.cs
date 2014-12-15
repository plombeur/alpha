using UnityEngine;
using System.Collections;

public class TipEndTutorial : ToolTip {
    // Use this for initialization
    void Awake()
    {
        isActivatedElsewhere = true;
        title = "Fin du tutoriel.";
        description = "Félicitation ! Tu as fini le tutoriel d'Alpha !\n\nTu peux maintenant essayer de faire survivre ta meute par toi même !\nPense à lire les informations qui te seront fournies, elles te seront utiles lorsque tu découvriras de nouvelles espèces.\n\nBon courage !\n\n~L'équipe d'Alpha.";
    }

    // Update is called once per frame
    void Update()
    {
        checkTrigger();
    }

    protected override void checkTrigger()
    {
        display();
    }
}

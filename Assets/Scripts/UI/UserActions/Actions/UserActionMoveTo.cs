using UnityEngine;
using System.Collections;

public class UserActionMoveTo : UserAction
{
    public LoupAlpha alphaWolf;
    public Vector2 position;

    protected override UserActionResult onExecuteAction()
    {
        Debug.Log("CHASSE a modifier par MOVETO");
       ((MindLoupAlpha) alphaWolf.mind).addActionUserAction(new AU_Chasse(position));
       return UserActionResult.SUCESS;
    }
    public override string getActionLabel()
    {
        return "Move to position";
    }
    public override bool isCancelable()
    {
        return true;
    }
}

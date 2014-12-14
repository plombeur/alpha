using UnityEngine;
using System.Collections;

public class UserActionSlap : UserAction
{
    public LoupAlpha alphaWolf;
    public LoupBeta betaWolf;

    protected override UserActionResult onExecuteAction()
    {
        ((MindLoupAlpha)LoupInferieur.alpha.mind).addActionUserAction(new AU_CalmerBeta(betaWolf));
       return UserActionResult.SUCESS;
    }
    public override string getActionLabel()
    {
        return "Calmer loup beta";
    }
    public override bool isCancelable()
    {
        return true;
    }
}

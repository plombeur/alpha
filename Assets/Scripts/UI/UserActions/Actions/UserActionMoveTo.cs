﻿using UnityEngine;
using System.Collections;

public class UserActionMoveTo : UserAction
{
    public LoupAlpha alphaWolf;
    public Vector2 position;

    protected override UserActionResult onExecuteAction()
    {
       ((MindLoupAlpha) alphaWolf.mind).addActionUserAction(new AU_MoveTo(position));
       return UserActionResult.SUCESS;
    }
    public override string getActionLabel()
    {
        return "Deplacement vers position";
    }
    public override bool isCancelable()
    {
        return true;
    }
}

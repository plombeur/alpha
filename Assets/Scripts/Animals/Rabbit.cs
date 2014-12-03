using UnityEngine;
using System.Collections;

public class Rabbit : Animal 
{
	void Start () 
    {
	
	}
	
	void Update () 
    {
	
	}

    protected override void onCreate()
    {
       
    }

    public override void construct(Mind mind)
    {
        base.construct(new MindRabbit(this));
    }
}

using UnityEngine;
using System.Collections;

public class Patch : Entity {
	public float passabilite;
	// Use this for initialization
	protected override void Start () {
	}

	// Update is called once per frame
	protected override void Update () {
	}

	public float getPassabilite(){
		return this.passabilite;
	}
}
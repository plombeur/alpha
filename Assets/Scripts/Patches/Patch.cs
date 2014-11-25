using UnityEngine;
using System.Collections;

public class Patch : Entity {
	protected float nutriments;
	protected float maxNutriments;
	protected float passabilite;
	protected float temperature;
	protected float evaporateSpeed;
	// Use this for initialization
	protected override void Start () {
		maxNutriments = 10;
		nutriments = maxNutriments;
		temperature = 10;
		passabilite = 1;
		evaporateSpeed = 1;
	}

	// Update is called once per frame
	protected override void Update () {
		evaporate ();
		checkValues ();
	}
	
	private void checkValues()
	{
		if (nutriments > maxNutriments)
			nutriments = maxNutriments;

        if (nutriments < 0)
        {
            nutriments = 0;
            //Destroy(this.gameObject);
        }
	}

	public float getPassabilite(){
		return this.passabilite;
	}

	public float getTemperature(){
		return this.temperature;
	}

	public float getNutriments(){
		return nutriments;
	}

	public void addNutriments(float value){
		this.nutriments += value;
	}
	protected void evaporate(){
		float evaporateValue = evaporateSpeed * Time.deltaTime;
		nutriments -= evaporateValue;
	}
}
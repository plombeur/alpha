using UnityEngine;
using System.Collections;

public class Patch : Entity {
	public float nutriments;
	public float maxNutriments;
	public float passabilite;
	public float temperature;
	public float evaporateSpeed;
	// Use this for initialization
	protected override void Start () {
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
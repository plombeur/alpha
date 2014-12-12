using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class MindRabbit : MindAnimal
{
    private float tempsAvantRentrerTerrier = Random.Range(0, 150);

    public MindRabbit(Rabbit rabbit) : base(rabbit)
    {
    
    }

    public override void vivre()
    {
        Rabbit rabbit = (Rabbit) agent;

        if(actionList.size() == 1)
            tempsAvantRentrerTerrier -= Time.deltaTime;

        if (rabbit.dansLeTerrier())
            rabbit.peutAvoirPeur = false;
        else
            rabbit.peutAvoirPeur = true;
        if(rabbit.perceptHearing != null)
        {
            List<SoundPercepted> sounds = rabbit.perceptHearing.getSounds();
            if (sounds.Count > 0)
            {
                Debug.Log("Sons percus : ");
                for (int i = 0; i < sounds.Count; ++i)
                {
                    Debug.Log(sounds[i].soundInformation);
                }
                Debug.Log("Finis.");
            }
        }

        if (tempsAvantRentrerTerrier <= 0)
        {
            actionList.addAction(new A_RentrerTerrier(Random.Range(10, 35)));
            tempsAvantRentrerTerrier = Random.Range(0, 150) - tempsAvantRentrerTerrier;
        }

        actionList.addAction(new A_Promenade(((Animal)agent).vitesse));
        base.vivre();
    }
}

using UnityEngine;
using System.Collections;

public class MindLoup : MindAnimal {

    private float compteurActiviteAleatoire = Random.Range(0, 10);

    public MindLoup(Loup agent) : base(agent)
    { }
	public override void vivre()
    {
        base.vivre();
        Loup loup = ((Loup)agent);
        if (loup.estMort())
            return;
        actionList.addAction(new A_Promenade(((Animal)agent).vitesse));
        if (actionList.size() == 1)  //Si l'agent ne fait que se promener et qu'il s'embête ... ajout d'une action d'occupation aléatoire spécifiée par randomAction()
        {
            compteurActiviteAleatoire -= Time.deltaTime;
            if (compteurActiviteAleatoire <= 0)
            {
                randomAction();
                compteurActiviteAleatoire = Random.Range(10, 50);
            }
        }
    }

    protected virtual void randomAction()
    {
        actionList.addAction(new A_Repos(Random.Range(10, 50)));
    }

}
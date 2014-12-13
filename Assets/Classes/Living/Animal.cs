using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public abstract class Animal : Living {

    public float distanceDeSecurite = 2.5f;
    public PerceptView perceptView;
    public PerceptHearing perceptHearing;
    public EmoticonSystem emoticonSystem;
    public GameObject vectorDisplayer;

    public int VIE_MAX;
    public float vie;
    public float direction;
    public float vitesse = 1;

    //Sounds
    public GameObject prefabSoundWalk;
    private GameObject sound;

    //Sprites ..
    public Sprite normalSprite;
    public Sprite sleepSprite;
    public Sprite rugirSprite;
    //Emoticon sprites
    public Sprite sleepEmoticonSprite;
    public Sprite questionEmoticonSprite;
    public Sprite exclamationEmoticonSprite;
    public Sprite heartEmoticonSprite;
    public Sprite hungryEmoticonSprite;

    //Variable qui définit si le systeme de réflexe fuite doit être activé
    public bool peutAvoirPeur = true;

    //Variables privées utilisées pour le déplacement avec évitement de foule
    private Animal agentToDontDodge;

    public void displayAnimatedEmoticon(Sprite sprite)
    {
        if (emoticonSystem != null)
            emoticonSystem.displayAnimatedEmoticon(sprite);
    }

    public void displayStaticEmoticon(Sprite sprite)
    {
        if (emoticonSystem != null)
            emoticonSystem.displayStaticEmoticon(sprite);
    }

    public void hideStaticEmoticon()
    {
        if (emoticonSystem != null)
            emoticonSystem.hideStaticEmoticon();
    }

    public override void construct(Mind mind)
    {
        if (emoticonSystem != null)
            emoticonSystem.setAnimal(this);
        direction = 0;
        base.construct(mind);
    }

    public void fd()
    {
        fd(vitesse);
    }

    public void fd(float pas, bool smoothRotation = true, bool evitementDesAutresAgents = true)
    {
        if ( prefabSoundWalk != null )
        {
            if (sound == null)
            {
                sound = (GameObject)Instantiate(prefabSoundWalk);
            }
            else
            {
                if (!sound.GetComponent<SoundSimple>().isStarted())
                {
                    //sound.GetComponent<SoundSimple>().play(getIdentity());
                }
            }
        }

        float finalDirection = direction;
        if (evitementDesAutresAgents)
        {
            //Liste des vecteurs générés pour éviter/fuir les obstacles et leurs poids
            List<Vector2> evitements = new List<Vector2>();
            List<float> poidsEvitements = new List<float>();
            List<Vector2> repulsions = new List<Vector2>();
            List<float> poidsRepulsions = new List<float>();

            Vector2 vectorDirection = Utils.vectorFromAngle(direction, 1f);

            Animal currentAnimal;
            List<MemoryBloc> memoryBlocs = new List<MemoryBloc>(GetComponent<Memory>().getMemoyBlocs());
            MemoryBloc currentBloc;
            for (int i = 0; i < memoryBlocs.Count; ++i)
            {
                currentBloc = memoryBlocs[i];
                currentAnimal = currentBloc.getEntity() as Animal;
                if (currentAnimal != null && currentAnimal != agentToDontDodge)
                {
                    float distance = Vector2.Distance(currentBloc.getLastPosition(), transform.position);
                    Vector2 vectorFaceToLastPosition = Utils.vectorFromAngle(getFaceToDirection(currentBloc.getLastPosition()));
                    float angle = Vector2.Angle(vectorDirection, vectorFaceToLastPosition);

                    //Ajout d'un vecteur pour éviter et passer sur le côté de l'obstacle
                    if (angle <= 90 && distance < 1.5f)
                    {
                        //Calcul de la force ( priorité ) de l'évitement afin de pondérer les différents vecteur
                        float force = (distance <= 1) ? 1f : (1f - (distance - 1f) / 4f);

                        Vector2 vectorDirectionCurrentLoup = Utils.vectorFromAngle(currentAnimal.direction);
                        if (distance < .75f || Vector2.Angle(vectorDirection, vectorDirectionCurrentLoup) > 18)
                        {
                            Vector2 subVector = vectorDirection - vectorDirectionCurrentLoup;
                            List<Vector2> orthogonaux = Utils.getOrthogonalsVectors(vectorFaceToLastPosition);
                            if (Vector2.Angle(orthogonaux[1], subVector) < Vector2.Angle(orthogonaux[0], subVector))
                                evitements.Add(orthogonaux[1]);
                            else
                                evitements.Add(orthogonaux[0]);

                            poidsEvitements.Add(force);
                        }

                        //Ajout d'un vecteur pour reculer par rapport à l'obstacle
                        if (force > .5f)
                        {
                            repulsions.Add(Utils.vectorFromAngle(getFaceToDirection(currentBloc.getLastPosition()) + 180));
                            poidsRepulsions.Add((force - .5f) / .5f);
                        }
                    }
                }
            }

            Vector2 sommeEvitements = new Vector2(0, 0);
            for (int i = 0; i < evitements.Count; ++i)
            {
                sommeEvitements += evitements[i] * poidsEvitements[i];
            }
            Vector2 sommeRepulsions = new Vector2(0, 0);
            for(int i=0; i < repulsions.Count; ++i)
            {
                sommeRepulsions += repulsions[i] * poidsRepulsions[i];
            }

            //Affichage des vecteurs si un vectorDisplayer est assigné au script
            if (vectorDisplayer != null)
            {
                VectorDisplayer displayer = vectorDisplayer.GetComponent<VectorDisplayer>();
                displayer.transform.position = transform.position;
                displayer.setBluePosition(vectorDirection * 6);
                if (sommeEvitements != Vector2.zero)
                {
                    displayer.setRedPosition(sommeEvitements * 6);
                }
                else
                    displayer.hideRedVector();
                displayer.transform.Rotate(new Vector3(0, 0, 1), 90);
            }
            sommeEvitements.Normalize();
            sommeRepulsions.Normalize();
            finalDirection = Utils.angleFromVector(vectorDirection + .8f * sommeEvitements + sommeRepulsions);
        }

        float currentDirection = direction;
        if (smoothRotation)
        {
            //Limitation du degré de rotation à parcourir à chaque tick
            currentDirection = GetComponent<Rigidbody2D>().rotation;
            float rotateValue = 4;
            Vector2 vectorCurrentDirection = Utils.vectorFromAngle(currentDirection);
            Vector2 vectorFinalDirection = Utils.vectorFromAngle(finalDirection);
            if (Vector2.Angle(vectorCurrentDirection, vectorFinalDirection) <= rotateValue)
            {
                currentDirection = finalDirection;
            }
            else
            {
                float determinant = vectorCurrentDirection.x * vectorFinalDirection.y - vectorCurrentDirection.y * vectorFinalDirection.x;
                if (determinant > 0)
                    currentDirection += rotateValue;
                else
                    currentDirection -= rotateValue;
            }
        }

        GetComponent<Rigidbody2D>().rotation = currentDirection;
        GetComponent<Rigidbody2D>().velocity = transform.up * pas;
    }

    public void wiggle(float pas, float wiggleValue)
    {
        lt(Random.Range(0, wiggleValue));
        rt(Random.Range(0, wiggleValue));
        fd(pas);
    }

    public void rt(float pas)
    {
        direction -= pas;
    }

    public void lt(float pas)
    {
        direction += pas;
    }

    public bool estMort()
    {
        return vie <= 0;
    }

    public void dors()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    public void faceTo(Living agent)
    {
        faceTo(agent.GetComponent<Transform>().position);
    }

    public void faceTo(Vector2 positionToLook)
    {
        Vector2 up = new Vector2(0, 1);
        Vector2 pointToLook = positionToLook - new Vector2(transform.position.x,transform.position.y);
        direction = Vector2.Angle(up, pointToLook);
        float determinant = up.x * pointToLook.y - up.y * pointToLook.x;
        if (determinant < 0)
            direction *= -1;
    }

    public float getFaceToDirection(Vector2 positionToLook)
    {
        Vector2 up = new Vector2(0, 1);
        Vector2 pointToLook = positionToLook - new Vector2(transform.position.x, transform.position.y);
        float result = Vector2.Angle(up, pointToLook);
        float determinant = up.x * pointToLook.y - up.y * pointToLook.x;
        if (determinant < 0)
            result *= -1;
        return result;
    }

    public LoupOmega randomLoupOmegaSeen()
    {
        List<Living> percepts = perceptView.getLiving();
        int nbOmega = 0;
        for (int i = 0; i < percepts.Count; ++i)
            if (percepts[i] as LoupOmega != null)
                nbOmega++;
        int indiceOmega = Random.Range(1, nbOmega);
        LoupOmega result = null;
        if (nbOmega > 0)
        {
            for (int i = 0; i < percepts.Count; ++i)
            {
                result = percepts[i] as LoupOmega;
                if (result != null)
                {
                    indiceOmega--;
                    if (indiceOmega == 0)
                        break;
                }
            }
        }
        return result;
    }

    public Loup randomLoupSeen()
    {
        List<Living> percepts = perceptView.getLiving();
        int nbLoups = 0;
        for (int i = 0; i < percepts.Count; ++i)
            if (percepts[i] as Loup != null)
                nbLoups++;
        int indiceLoup = Random.Range(1, nbLoups);
        Loup result = null;
        if (nbLoups > 0)
        {
            for (int i = 0; i < percepts.Count; ++i)
            {
                result = percepts[i] as Loup;
                if (result != null)
                {
                    indiceLoup--;
                    if (indiceLoup == 0)
                        break;
                }
            }
        }
        return result;
    }

    public Action getCurrentAction()
    {
        return ((MindAnimal)mind).getCurrentAction();
    }

    public void resetAgentToDontDodge()
    {
        agentToDontDodge = null;
    }

    public void setAgentToDontDodge(Animal agent)
    {
        agentToDontDodge = agent;
    }

    public abstract float getDirectionFuite();
    public abstract bool besoinDeFuir();

    public virtual bool targetable()
    {
        return true;
    }

    public abstract List<SoundInformation> getSonsInterpellant();

}
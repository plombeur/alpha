using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Animal : Living {
    public PerceptView perceptView;
    public EmoticonSystem emoticonSystem;
    public GameObject vectorDisplayer;

    public int VIE_MAX;
    public float vie;
    public float direction;
    public int FAIM_MAX;
    public float faim;
    public float vitesse = 1;

    //Sprites ..
    public Sprite normalSprite;
    public Sprite sleepSprite;
    public Sprite rugirSprite;
    //Emoticon sprites
    public Sprite sleepEmoticonSprite;
    public Sprite questionEmoticonSprite;
    public Sprite heartEmoticonSprite;

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

    public void construct(MindAnimal mind)
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

    public void fd(float pas)
    {
        //Liste des vecteurs générés pour éviter/fuir les obstacles et leurs poids
        List<Vector2> evitements = new List<Vector2>();
        List<float> poids = new List<float>();

        Vector2 vectorDirection = Utils.vectorFromAngle(direction, .8f);

        Animal currentLoup;
        List<MemoryBloc> memoryBlocs = new List<MemoryBloc>(GetComponent<Memory>().getMemoyBlocs());
        MemoryBloc currentBloc;
        for (int i = 0; i < memoryBlocs.Count; ++i)
        {
            currentBloc = memoryBlocs[i];
            currentLoup = currentBloc.getEntity() as Loup;
            if (currentLoup != null && currentLoup != agentToDontDodge)
            {
                float distance = Vector2.Distance(currentBloc.getLastPosition(), transform.position);
                Vector2 vectorFaceToLastPosition = Utils.vectorFromAngle(getFaceToDirection(currentBloc.getLastPosition()));
                float angle = Vector2.Angle(vectorDirection, vectorFaceToLastPosition);
                
                //Ajout d'un vecteur pour éviter et passer sur le côté de l'obstacle
                if (angle <= 90 && distance < 15)
                {
                    //Calcul de la force ( priorité ) de l'évitement afin de pondérer les différents vecteur
                    float force = (distance <= 6) ? 1f : (1f - (distance - 6f) / 9f);

                    Vector2 vectorDirectionCurrentLoup = Utils.vectorFromAngle(currentLoup.direction);
                    if (distance < 6 || Vector2.Angle(vectorDirection,vectorDirectionCurrentLoup) > 18)
                    {
                        Vector2 subVector = vectorDirection - vectorDirectionCurrentLoup;
                        List<Vector2> orthogonaux = Utils.getOrthogonalsVectors(vectorFaceToLastPosition);
                        if (Vector2.Angle(orthogonaux[1], subVector) < Vector2.Angle(orthogonaux[0], subVector))
                            evitements.Add(orthogonaux[1]);
                        else
                            evitements.Add(orthogonaux[0]);
                        
                        poids.Add(force);
                    }
                    
                    //Ajout d'un vecteur pour reculer par rapport à l'obstacle
                    if (force > .5f)
                    {
                        evitements.Add(Utils.vectorFromAngle(getFaceToDirection(currentBloc.getLastPosition()) + 180));
                        poids.Add((force - .5f) / .5f);
                    }
                }
            }
        }

        Vector2 sommeVecteurs = new Vector2(0, 0);
        for (int i = 0; i < evitements.Count; ++i)
        {
            sommeVecteurs += evitements[i] * poids[i];
        }
        float finalDirection;

        //Affichage des vecteurs si un vectorDisplayer est assigné au script
        if (vectorDisplayer != null)
        {
            VectorDisplayer displayer = vectorDisplayer.GetComponent<VectorDisplayer>();
            displayer.transform.position = transform.position;
            displayer.setBluePosition(vectorDirection * 6);
            if (sommeVecteurs != Vector2.zero)
            {
                displayer.setRedPosition(sommeVecteurs * 6);
            }
            else
                displayer.hideRedVector();
            displayer.transform.Rotate(new Vector3(0, 0, 1), 90);
        }

        finalDirection = Utils.angleFromVector(vectorDirection + sommeVecteurs);
        
        //Limitation du degré de rotation à parcourir à chaque tick
        float currentDirection = GetComponent<Rigidbody2D>().rotation;
        float rotateValue = 2;
        Vector2 vectorCurrentDirection = Utils.vectorFromAngle(currentDirection);
        Vector2 vectorFinalDirection = Utils.vectorFromAngle(finalDirection);
        if(Vector2.Angle(vectorCurrentDirection,vectorFinalDirection) <= rotateValue)
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
}
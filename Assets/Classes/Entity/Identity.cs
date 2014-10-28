using System.Collections.Generic;

public class Identity
{
    private static readonly object _locker = new object();
    private static int generatorID = 0;

    private int ID;
    private Entity entity;
    private bool alive = true;

    public Identity(Entity entity)
    {
        lock (_locker)
        {
            this.ID = generatorID++;
        }
        this.entity = entity;
    }
    public int getID()
    {
        return ID;
    }
    public Entity getEntity()
    {
        return entity;
    }
    public void setAlive(bool alive)
    {
        this.alive = alive;
    }
    public bool isEntityAlive()
    {
        return alive;
    }

    public override bool Equals(System.Object obj)
    {
        if (obj == null)
        {
            return false;
        }

        Identity id = obj as Identity;
        return id != null && this.ID == id.ID && id.entity == entity;
    }
}

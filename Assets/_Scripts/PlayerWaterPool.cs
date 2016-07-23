using UnityEngine;
using System.Collections;

public class PlayerWaterPool : MonoBehaviour
{
    public int amount;
    public int poolCapacity;

    public void Add(int amount)
    {
        if(this.amount + amount > poolCapacity)
        {
            this.amount = poolCapacity;
        }
        else
        {
            this.amount += amount;
        }
    }

    public void Remove(int amount)
    {
        if (this.amount - amount < 0)
        {
            this.amount = 0;
        }
        else
        {
            this.amount -= amount;
        }
    }
}

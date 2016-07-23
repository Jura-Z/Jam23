using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerWaterPool waterPool;

    public PlayerWaterPool WaterPool
    {
        get
        {
            return waterPool;
        }
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
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

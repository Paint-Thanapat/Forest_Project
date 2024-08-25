using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using PhotonHashTable = ExitGames.Client.Photon.Hashtable;

public class NetworkMapManager : NetworkSceneSingleton<NetworkMapManager>
{
    public static void SendSelectMainObstacle(int gridID, int obsIndex)
    {
        Instance.PV.RPC("RPC_SendSelectMainObstacle", RpcTarget.AllBuffered, gridID, obsIndex);
    }

    [PunRPC]
    public void RPC_SendSelectMainObstacle(int gridID, int obsIndex)
    {
        MapGridManager.Instance.SelectGridMainObstacle(gridID, obsIndex);
    }


    public static void SendEnableMapGridObstacle(Vector2Int gridIndex)
    {
        Instance.PV.RPC("RPC_SendEnableMapGridObstacle", RpcTarget.AllBuffered, gridIndex.x, gridIndex.y);
    }

    [PunRPC]
    public void RPC_SendEnableMapGridObstacle(int gridX, int gridY)
    {
        MapGridManager.Instance.EnableMapGridObstacle(new Vector2Int(gridX, gridY));
    }
}

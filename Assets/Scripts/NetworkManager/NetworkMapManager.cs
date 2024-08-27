using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using PhotonHashTable = ExitGames.Client.Photon.Hashtable;

public class NetworkMapManager : NetworkSceneSingleton<NetworkMapManager>
{
    public static void SendSelectMainObstacle(int gridIndex, int obsIndex)
    {
        Instance.PV.RPC("RPC_SendSelectMainObstacle", RpcTarget.AllBuffered, gridIndex, obsIndex);
    }

    [PunRPC]
    public void RPC_SendSelectMainObstacle(int gridIndex, int obsIndex)
    {
        MapGridManager.Instance.SelectGridMainObstacle(gridIndex, obsIndex);
    }


    public static void SendEnableMapGridObstacle(int gridIndex)
    {
        Instance.PV.RPC("RPC_SendEnableMapGridObstacle", RpcTarget.AllBuffered, gridIndex);
    }

    [PunRPC]
    public void RPC_SendEnableMapGridObstacle(int gridIndex)
    {
        MapGridManager.Instance.EnableMapGridObstacle(gridIndex);
    }
}

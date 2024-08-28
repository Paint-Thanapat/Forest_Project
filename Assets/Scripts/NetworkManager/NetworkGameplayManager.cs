using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using PhotonHashTable = ExitGames.Client.Photon.Hashtable;

public class NetworkGameplayManager : NetworkSceneSingleton<NetworkGameplayManager>
{
    public static Player player;
    public static int LocalID = -1;

    public static void SendForceStopState(int playerID)
    {
        Instance.PV.RPC("RPC_SendForceStopState", RpcTarget.AllBuffered, playerID);
    }

    [PunRPC]
    public void RPC_SendForceStopState(int playerID)
    {
        if (NetworkGameplayManager.LocalID == playerID)
        {
            player.ChangeState(player.movementStateMachine.stopState);
        }
    }
}
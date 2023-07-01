using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

namespace Photon.CreateJoin
{
    public class PunLogger : MonoBehaviour, IConnectionCallbacks
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void OnConnected()
        {
            Debug.Log("Client is connected to the server");
        }

        public void OnConnectedToMaster()
        {
            Debug.Log("Connected to the Master Server");
        }

        public void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log(cause);
        }

        public void OnRegionListReceived(RegionHandler regionHandler)
        {
            
        }

        public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
        {

        }

        public void OnCustomAuthenticationFailed(string debugMessage)
        {

        }
    }
}
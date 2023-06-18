using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Defense.Network
{
    public enum ServerType
    {
        None,
        Server1,
        Development
    }

    [System.Serializable]
    public class BackendServerEventInfo
    {
        public string uuid;
    }

    [System.Serializable]
    public class BackendServerInfo
    {
        [Header("ServerInfo")]
        public ServerType type;
        public string serverName;
        public string clientAppId;
        public string signatureKey;

        [Header("RankInfo")]
        public string rankUuid;

        [Header("EventInfo")]

        [Header("Config")]
        public bool showAd;
        public bool testAd;
    }

    [CreateAssetMenu(fileName = "BackendServerSettings", menuName = "Server/BackendServerSettings")]
    public class BackendServerSettings : ScriptableObject
    {
        [SerializeField] List<BackendServerInfo> _serverInfos;

        public BackendServerInfo GetServerInfo(ServerType type)
        {
            return _serverInfos.Find(x => x.type == type);
        }
    }
}

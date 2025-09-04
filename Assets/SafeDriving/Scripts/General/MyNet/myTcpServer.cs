using System;
using System.Net;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TcpServer = Netly.TcpServer;
using UdpServer = Netly.UdpServer;
using TcpClient = Netly.TcpClient;
using UdpClient = Netly.UdpClient;
using Netly.Core;


public class myTcpServer : MonoBehaviour
{
    [System.Serializable]
    public class OnErrorEvent : UnityEvent<Exception> { }

    [System.Serializable]
    public class OnDataEvent : UnityEvent<TcpClient, string> { }

    [System.Serializable]
    public class OnEventEvent : UnityEvent<TcpClient, string, string> { }

    [System.Serializable]
    public class OnEnterEvent : UnityEvent<TcpClient> { }

    [System.Serializable]
    public class OnExitEvent : UnityEvent<TcpClient> { }
    [Header("是否使用Net Discovery：")]
    public bool UseNetDiscovery = false;
    [Header("是否自行指定 IP：")]
    public bool UseSpecifyIP = false;
    [Header("Server IP address：")]
    public string ipAddress = "127.0.0.1";
    [Header("Server port number：")]
    public int port = 8052;

    [SerializeField]
    public List<TcpClient> ClientsList = new List<TcpClient>();
    public List<string> ClientIDs = new List<string>();
    public List<string> ClientUUIDs = new List<string>();

    public bool IsOpened { get { return server.IsOpened; } }

    private NetDiscovery myNetDiscovery;

    Host host = new Host("0.0.0.0", 3000);
    TcpServer server = new TcpServer();

    [Header("Event:")]
    [SerializeField]
    public UnityEvent OnOpen;
    [SerializeField]
    public UnityEvent OnClose;
    [SerializeField]
    public OnDataEvent OnData;
    [SerializeField]
    public OnErrorEvent OnError;
    [SerializeField]
    public OnEventEvent OnEvent;
    [SerializeField]
    public OnEnterEvent OnEnter;
    [SerializeField]
    public OnExitEvent OnExit;

    
    public static myTcpServer Instance { private set; get; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Initial();
            DontDestroyOnLoad(this);
            return;
        }
        else if (Instance == this)
        {
            return;
        }
        Destroy(gameObject);
        return;
    }

    private void Start()
    {
        if (UseNetDiscovery)
        {
            myNetDiscovery = gameObject.GetComponent<NetDiscovery>();
            myNetDiscovery.StartNetDiscovery();
        }
    }


    private void Initial()
    {
        NE.Default = NE.Mode.UTF8;

        if (!UseSpecifyIP)
        {
            ipAddress = GetLocalIPv4(); //抓取本機之IP
        }

        host = new Host(ipAddress, port);
        server.Open(host);
        server.OnOpen(() => { OnOpen.Invoke(); });
        server.OnClose(() => { OnClose.Invoke(); });
        server.OnError((e) => { OnError.Invoke(e); });
        server.OnData((client, data) =>
        {
            string mydata = NE.GetString(data);
            OnData.Invoke(client, mydata);
        });

        server.OnEvent((client, name, data) =>
        {
            //加入Client Name
            if (name == "newclient")
            {
                for (int i = 0; i < ClientsList.Count; i++)
                {
                    TcpClient p = ClientsList[i];
                    if (p.UUID == client.UUID)
                    {
                        ClientIDs.Insert(i, NE.GetString(data));
                        break;
                    }
                }
            }
            string mydata = NE.GetString(data);
            //string mydata = System.Text.Encoding.UTF8.GetString(data);
            Debug.Log(name);
            OnEvent.Invoke(client, name, mydata);
        });
        server.OnEnter((client) =>
        {
            //加入Client
            ClientsList.Add(client);
            ClientUUIDs.Add(client.UUID);

            OnEnter.Invoke(client);
        });
        server.OnExit((client) =>
        {
            //移除Client
            for (int i = 0; i < ClientsList.Count; i++)
            {
                TcpClient p = ClientsList[i];
                if (p.UUID == client.UUID)
                {
                    ClientsList.Remove(p);
                    ClientUUIDs.Remove(p.UUID);
                    ClientIDs.RemoveAt(i);
                    return;
                }
            }
            //ClientsList.Remove(client);
            OnExit.Invoke(client);
        });
    }

    public void Update()
    {
        if (ClientsList.Count > 0)
        {
            //Debug.Log(ClientsList[0].IsOpened);
        }
    }

    //送資料給所有Cilent
    public void SendDataToAllClient(string Data)
    {
        if (!server.IsOpened) return;
        server.ToData(Data);
    }

    //送資料給所有Cilent，外加事件名稱
    public void SendEventToAllClient(string name, string Data)
    {
        if (!server.IsOpened) return;
        server.ToEvent(name, Data);
    }

    //送資料給特定Cilent
    public void SendDataToClient(TcpClient tcpClient, string Data)
    {
        if (!tcpClient.IsOpened) return;
        tcpClient.ToData(Data);
    }

    //送資料給特定Cilent，外加事件名稱
    public void SendEventToClient(TcpClient tcpClient, string name, string Data)
    {
        if (!tcpClient.IsOpened) return;
        tcpClient.ToEvent(name, Data);
    }

    //送資料給特定Cilent
    public void SendDataToClient(string ID, string Data)
    {
        for (int i = 0; i < ClientIDs.Count; i++)
        {
            if (ID == ClientIDs[i])
            {
                TcpClient tcpClient = ClientsList[i];
                if (!tcpClient.IsOpened) return;
                tcpClient.ToData(Data);
                return;
            }
        }
    }

    //送資料給特定Cilent，外加事件名稱
    public void SendEventToClient(string ID, string name, string Data)
    {
        for (int i = 0; i < ClientIDs.Count; i++)
        {
            if (ID == ClientIDs[i])
            {
                TcpClient tcpClient = ClientsList[i];
                if (!tcpClient.IsOpened) return;
                tcpClient.ToEvent(name, Data);
                return;
            }
        }
    }


    public string GetLocalIPv4()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        for (int i = host.AddressList.Length - 1; i > 0; i++)
        {
            IPAddress ip = host.AddressList[i];
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("No network adapters with an IPv4 address in the system!");
    }

    public void OnDestroy()
    {
        if (!server.IsOpened) return;
        server.Close();
    }

}

using System;
using UnityEngine;
using UnityEngine.Events;
using TcpClient = Netly.TcpClient;
using UdpClient = Netly.UdpClient;
using Netly.Core;
using System.Collections;

public class myTcpClient : MonoBehaviour
{
    [System.Serializable]
    public class OnErrorEvent : UnityEvent<Exception> { }

    [System.Serializable]
    public class OnDataEvent : UnityEvent<string> { }

    [System.Serializable]
    public class OnEventEvent : UnityEvent<string, string> { }

    [Header("是否使用Net Discovery：")]
    public bool UseNetDiscovery = false;
    [Header("Client Name：")]
    public string ID = "Client 1";
    [Header("Server IP address：")]
    public string ipAddress = "127.0.0.1";
    [Header("Server port number：")]
    public int port = 8052;

    public bool IsOpened { get { return client.IsOpened; } }

    private NetDiscovery myNetDiscovery;

    Host host = new Host("0.0.0.0", 3000);
    TcpClient client = new TcpClient();

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


    public static myTcpClient Instance { private set; get; }

    private void Awake()
    {
        if (Instance == null)
        {
            /*
            if (UseNetDiscovery)
            {
                myNetDiscovery = gameObject.GetComponent<NetDiscovery>();
                myNetDiscovery.StartNetDiscovery();
            }
            else
            {
                Initial();
            }
            */
            Instance = this;
            //Initial();
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
        else
        {
            Initial();
        }
    }

    private void Initial()
    {
        NE.Default = NE.Mode.UTF8;

        host = new Host(ipAddress, port);
        client.Open(host);
        client.OnOpen(() =>
        {
            //送出ID
            SendEventToServer("newclient", ID);
            OnOpen.Invoke();

        });
        client.OnClose(() => { OnClose.Invoke(); });
        client.OnError((e) =>
        {
            Debug.Log(e);
            OnError.Invoke(e);
            Invoke("ToOpen", 0);
        });
        client.OnData((data) =>
        {
            string mydata = NE.GetString(data);
            OnData.Invoke(mydata);
        });
        client.OnEvent((name, data) =>
        {
            Debug.Log(name);
            string mydata = NE.GetString(data);
            OnEvent.Invoke(name, mydata);
        });

        InvokeRepeating("CheckNetState", 2, 2);
    }

    public void NetDiscoveryIP(string ip)
    {
        ipAddress = ip;
        Debug.Log("discovery："+ipAddress);
        Initial();
    }

    public void CheckNetState()
    {
        //Debug.Log(IsOpened);
        if (!IsOpened)
        {
            ToOpen();
        }
    }

    public void ToOpen()
    {
        host = new Host(ipAddress, port);
        client.Open(host);
    }

    //送資料給Server
    public void SendDataToServer(string Data)
    {
        if (!client.IsOpened) return;
        client.ToData(Data);
    }

    //送資料給Server，外加事件名稱
    public void SendEventToServer(string name, string Data)
    {
        if (!client.IsOpened) return;
        client.ToEvent(name, Data);
    }

    public void OnDestroy()
    {
        if (!client.IsOpened) return;
        client.Close();
    }
}

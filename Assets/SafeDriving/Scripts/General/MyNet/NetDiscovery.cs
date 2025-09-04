using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System;
using System.Text;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;
using System.Net.NetworkInformation;

public class NetDiscovery : MonoBehaviour
{
    public enum NetworkType { Server, Client }

    [Header("設定Server/Client")]
    public NetworkType NetType;
    [Header("設定Port")]
    public int serverPort = 6540;
    //public Text textip;

    [System.Serializable]
    public class OnGetIPEvent : UnityEvent<string> { }

    [SerializeField]
    [Header("[Client]收到Server的IP")]
    public OnGetIPEvent OnGetIP;

    //public string serverIP = "";
    public string clientIP = "";

    bool getServerIP = false;
    private string _serverIP = "";
    public string serverIP
    {
        get
        {
            if (_serverIP == "")
            {

            }
            return this._serverIP;
        }
    }

    private Thread listenThread;
    private UdpClient myUdpClient;

    void Start()
    {
        //StartNetDiscovery();
    }

    private void Update()
    {
        if (getServerIP)
        {
            OnGetIP.Invoke(_serverIP);
            getServerIP = false;
        }
    }

    public void StartNetDiscovery()
    {
        if (NetType == NetworkType.Server)
        {
            StartServer();
        }
        else
        {
            StartClient();
            InvokeRepeating("CheckNetState", 2, 2);
        }
    }

    void OnApplicationQuit()
    {
        DestroyConnection();
    }

    private void OnDestroy()
    {
        DestroyConnection();
    }
    private void DestroyConnection()
    {
        //if (NetType == NetworkType.Server)
        {
            if (this.listenThread != null && this.listenThread.IsAlive)
            {
                this.listenThread.Abort();
            }
        }

        if (this.myUdpClient != null)
        {
            this.myUdpClient.Close();
            this.myUdpClient = null;
        }
    }

    private void CheckNetState()
    {
        if (NetType == NetworkType.Client)
        {
            if (_serverIP == "")
            {
                byte[] RequestData = Encoding.ASCII.GetBytes(LocalIPAddress());
                this.myUdpClient.Send(RequestData, RequestData.Length, new IPEndPoint(IPAddress.Broadcast, serverPort));
            }
            else
            {
                DestroyConnection();
            }
        }

    }

    private void StartClient()
    {
        /*
        try
        {
            this.myUdpClient = new UdpClient();
            this.myUdpClient.EnableBroadcast = true;

            byte[] RequestData = Encoding.ASCII.GetBytes(LocalIPAddress());
            this.myUdpClient.Send(RequestData, RequestData.Length, new IPEndPoint(IPAddress.Broadcast, serverPort));
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
        */

        this.listenThread = new Thread(new ThreadStart(ClientListen));
        this.listenThread.IsBackground = true;
        this.listenThread.Start();
    }

    private void ClientListen()
    {
        this.myUdpClient = new UdpClient();
        myUdpClient.Client.SendTimeout = 200;
        myUdpClient.Client.ReceiveTimeout = 500;
        this.myUdpClient.EnableBroadcast = true;
        clientIP = LocalIPAddress();

        while (this.myUdpClient != null)
        {
            try
            {
                byte[] RequestData = Encoding.ASCII.GetBytes(LocalIPAddress());

                //測試
                //string BroadcastIP = clientIP.Remove(clientIP.LastIndexOf(".") + 1) + "255";
                //this.myUdpClient.Send(RequestData, RequestData.Length, new IPEndPoint(IPAddress.Parse(BroadcastIP), serverPort));

                this.myUdpClient.Send(RequestData, RequestData.Length, new IPEndPoint(IPAddress.Broadcast, serverPort));


                IPEndPoint ServerEp = new IPEndPoint(IPAddress.Any, serverPort);
                byte[] ServerResponseData = this.myUdpClient.Receive(ref ServerEp);
                string ServerResponse = Encoding.ASCII.GetString(ServerResponseData);
                _serverIP = ServerResponse;
                Debug.Log(ServerResponse);

                //OnGetIP.Invoke(_serverIP);
                
                getServerIP = true;

                myUdpClient.Close();
                myUdpClient = null;

            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
        }
    }

    /*
    private void StartClient()
    {
        StartCoroutine(ClientListen());
    }

    IEnumerator ClientListen()
    {
        this.myUdpClient = new UdpClient();
        myUdpClient.Client.SendTimeout = 200;
        myUdpClient.Client.ReceiveTimeout = 500;
        this.myUdpClient.EnableBroadcast = true;
        clientIP = LocalIPAddress();

        while (this.myUdpClient != null)
        {
            try
            {
                byte[] RequestData = Encoding.ASCII.GetBytes(LocalIPAddress());

                //測試
                //string BroadcastIP = clientIP.Remove(clientIP.LastIndexOf(".") + 1) + "255";
                //this.myUdpClient.Send(RequestData, RequestData.Length, new IPEndPoint(IPAddress.Parse(BroadcastIP), serverPort));

                this.myUdpClient.Send(RequestData, RequestData.Length, new IPEndPoint(IPAddress.Broadcast, serverPort));
                
                //Debug.Log(BroadcastIP);

                IPEndPoint ServerEp = new IPEndPoint(IPAddress.Any, serverPort);
                byte[] ServerResponseData = this.myUdpClient.Receive(ref ServerEp);
                string ServerResponse = Encoding.ASCII.GetString(ServerResponseData);
                _serverIP = ServerResponse;
                Debug.Log(ServerResponse);

                OnGetIP.Invoke(_serverIP);

                myUdpClient.Close();
                myUdpClient = null;
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
    */

    // ########################### server parts ###############################################################################################
    private void StartServer()
    {
        
        this.listenThread = new Thread(new ThreadStart(ServerListen));
        this.listenThread.IsBackground = true;
        this.listenThread.Start();
        
        //StartCoroutine(ServerListen());
    }

    private void ServerListen()
    {
        this.myUdpClient = new UdpClient(this.serverPort);
        //myUdpClient.Client.SendTimeout = 200;
        //myUdpClient.Client.ReceiveTimeout = 3000;

        myUdpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

        while (this.myUdpClient != null) // continue to receive data as long its existing
        {
            try
            {
                IPEndPoint ServerEp = new IPEndPoint(IPAddress.Any, serverPort);
                byte[] ClientRequestData = this.myUdpClient.Receive(ref ServerEp);
                string ClientRequest = Encoding.ASCII.GetString(ClientRequestData);
                Debug.Log(ClientRequest);

                byte[] ServerSendData = Encoding.ASCII.GetBytes(LocalIPAddress());
                this.myUdpClient.Send(ServerSendData, ServerSendData.Length, ServerEp);
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
            //yield return new WaitForSeconds(1.0f);
        }

    }
    
    /*
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
    */

    public string LocalIPAddress()
    {
        string localIP = "0.0.0.0";
        //IPHostEntry host;
        //host = Dns.GetHostEntry(Dns.GetHostName());

        foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
        {
            //if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
            //commented above condition, as it may not work on Android, found issues on Google Pixel Phones
            {
                foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        if (ip.IsDnsEligible)
                        {
                            try
                            {
                                if (ip.AddressValidLifetime / 2 != int.MaxValue)
                                {
                                    localIP = ip.Address.ToString();
                                    break;
                                }
                                else
                                {
                                    //if didn't find any yet, this is the only one
                                    if (localIP == "0.0.0.0") localIP = ip.Address.ToString();
                                }
                            }
                            catch
                            {
                                localIP = ip.Address.ToString();
                                //Debug.Log("LocalIPAddress(): " + e.ToString());
                                break;
                            }
                        }
                    }
                }
            }
        }
        return localIP;
    }
}

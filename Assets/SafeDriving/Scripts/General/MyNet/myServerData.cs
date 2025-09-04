using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TcpClient = Netly.TcpClient;

public class myServerData : MonoBehaviour
{
    public Text IPtext;
    public Text mytext;
    public InputField myinput;

    //public myTcpServer myserver;
    //myTcpClient myclient;

    // Start is called before the first frame update
    void Start()
    {
        //myserver = this.GetComponent<myTcpServer>();
        myTcpServer.Instance.OnData.AddListener(mydata);

    }

    // Update is called once per frame
    void Update()
    {
        if (myTcpServer.Instance.IsOpened)
        {
            IPtext.text = myTcpServer.Instance.ipAddress;
        }
    }

    public void senddata()
    {
        //myTcpServer.Instance.SendDataToAllClient(myinput.text);
        //myserver.ClientToData("t1", myinput.text);
        myTcpServer.Instance.SendEventToClient(myTcpServer.Instance.ClientsList[0], "echo", "0");
    }

    public void mydata(TcpClient client, string a)
    {
        mytext.text = a;
        Debug.Log(a);

    }

    public void myevent(TcpClient client, string a,string b)
    {
        mytext.text = a+":"+b;
        Debug.Log(a);

    }

    public void myevent(string s, string a)
    {
        //string b = System.Text.Encoding.ASCII.GetString(a);
        //Debug.Log(s+":"+b);

    }
}

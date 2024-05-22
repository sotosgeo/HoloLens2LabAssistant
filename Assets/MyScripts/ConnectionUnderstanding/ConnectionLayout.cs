using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionLayout 
{


    public string ConnectionLayoutName;
    public List<Connection> ConnectionsInLayout { get; private set; }

    
    public ConnectionLayout(string connectionLayoutName, List<Connection> connectionsInLayout)
    {
        ConnectionLayoutName = connectionLayoutName;
        ConnectionsInLayout = connectionsInLayout;
    }
}

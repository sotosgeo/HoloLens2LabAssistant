using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionLayout 
{


    public string ConnectionLayoutName;
    public List<Connection> ConnectionsInLayout { get; private set;}

    public ConnectionLayout(string name, List<Connection> connections) {
        ConnectionLayoutName = name;
        ConnectionsInLayout = connections;
    }

}

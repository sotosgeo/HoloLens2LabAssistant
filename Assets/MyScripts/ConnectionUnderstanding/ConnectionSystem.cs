using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionSystem 
{


    public string ConnectionSystemName;
    public List<Connection> ConnectionsInLayout { get; private set; }

    
    public ConnectionSystem(string Name, List<Connection> connectionsInSystem)
    {
        ConnectionSystemName = Name;
        ConnectionsInLayout = connectionsInSystem;
    }
}



public class ConnectionSystemComponent : MonoBehaviour
{

}

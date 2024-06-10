using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ConnectionSystem
{

    public List<PinConnection> connections;
    
    public void ClearSystem()
    {
        connections.Clear();
    }

    public ConnectionSystem(List<PinConnection> connections)
    {
        this.connections = connections;
    }

}





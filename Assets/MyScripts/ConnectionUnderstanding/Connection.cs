using HeftyConnections;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Connection : IEquatable<Connection>
{

    public Pin PinA { get; private set; }
    public Pin PinB { get; private set; }
    public int ConnectingCable { get; private set; }


    public Connection(Pin pinA, Pin pinB, int connectingCable)
    {
        PinA = pinA;
        PinB = pinB;
        ConnectingCable = connectingCable;
    }

    

    public override string ToString()
    {
        return PinA.ToString() + " - > " + PinB.ToString() + " via cable " + ConnectingCable.ToString();
    }


    public bool Equals(Connection other)
    {
        return other != null && GetType() == other.GetType() && ((PinA == other.PinA && PinB == other.PinB) || (PinA == other.PinB && PinB == other.PinA));
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as Connection);
    }

    public override int GetHashCode()
    {
        return PinA.GetHashCode() ^ PinB.GetHashCode();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

[Serializable]
public class PinConnection : IEquatable<PinConnection>
{
    public Pin PinA;
    public Pin PinB;

    public Cable Cable;

    //public Connection Connection { get; private set; }
    public PinConnection(Pin pinA, Pin pinB, Cable cable)
    {
        this.PinA = pinA;
        this.PinB = pinB;
        this.Cable = cable;


    }



    public bool Equals(PinConnection other)
    {
        return other != null && GetType() == other.GetType() && ((PinA == other.PinA && PinB == other.PinB) || (PinA == other.PinB && PinB == other.PinA)) && (Cable == other.Cable);
    }

    public override int GetHashCode()
    {
        return PinA.GetHashCode() ^ PinB.GetHashCode() ^ Cable.GetHashCode();
    }

    public override string ToString()
    {
        return $"{PinA.FullTag} -> {PinB.FullTag} via cable {Cable.cableId}";
    }
}


public class PinConnectionComparer : IEqualityComparer<PinConnection>
{
    public bool Equals(PinConnection x, PinConnection y)
    {
       

        return (x.PinA == y.PinA && x.PinB == y.PinB) || (x.PinA == y.PinB && x.PinB == y.PinA);
    }

    public int GetHashCode(PinConnection obj)
    {
        return obj.PinA.GetHashCode() ^ obj.PinB.GetHashCode();
    }
}

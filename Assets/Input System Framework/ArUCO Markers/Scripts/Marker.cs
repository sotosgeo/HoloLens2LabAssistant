using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Marker
{
    public static Marker Identity => new Marker(Vector3.zero, Quaternion.identity);

    public int id;
    public Vector3 position;
    public Quaternion rotation;

    public Marker(Vector3 position, Quaternion rotation)
    {
        this.id = 0;
        this.position = position;
        this.rotation = rotation;
    }

    public Marker(int id, Vector3 position, Quaternion rotation)
    {
        this.id = id;
        this.position = position;
        this.rotation = rotation;
    }
}

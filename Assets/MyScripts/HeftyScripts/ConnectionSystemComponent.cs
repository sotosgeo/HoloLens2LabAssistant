using UnityEngine;
using System.Collections.Generic;
using System;
using AYellowpaper.SerializedCollections;
using Unity.VisualScripting;
using System.Net.NetworkInformation;
using UnityEngine.VFX;

// This is an gameobject representing a port
namespace HeftyConnections
{
    // Not a gameobject
    [Serializable]
    public class Connection: ISerializationCallbackReceiver
    {
        // in the form of node1.p1
        [field: SerializeField] public string TagOne { get; private set; } = " ";
        // in the form of node2.p1
        [field: SerializeField] public string TagTwo { get; private set; } = " ";
        public int UniqueId { get; private set; }

        public Connection() { Initialize(TagOne, TagTwo, new List<char>(TagTwo.Length + TagOne.Length)); }
        public Connection(string tagOne, string tagTwo, List<char> forSorting = null) => Initialize(tagOne, tagTwo, forSorting);

        //Sets up this object. Must be called before the object is usable!!
        public void Initialize(string tagOne, string tagTwo, List<char> forSorting = null)
        {
            TagOne = tagOne;
            TagTwo = tagTwo;

            // A list was provided in the constructor to avoid
            // garbage collection if the user needs it

            if (forSorting == null)
                forSorting = new List<char>(TagOne.Length + TagTwo.Length);
            else if (forSorting.Capacity < TagOne.Length + TagTwo.Length)
                forSorting.Capacity = TagOne.Length + TagTwo.Length;

            while (forSorting.Count < forSorting.Capacity)
                forSorting.Add('c');

            // Create spans to read the strings
            for (int i = 0; i < TagOne.Length; i++)
                forSorting[i] = TagOne[i];
            for (int i = TagOne.Length; i < TagTwo.Length + TagOne.Length; i++)
                forSorting[i] = TagTwo[i - TagOne.Length];

            // Sort the list to get a unique string
            forSorting.Sort();
            UniqueId = new string(forSorting.ToArray()).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Connection other = obj as Connection;
            if (other == null)
                return false;
            return UniqueId == other.UniqueId;
        }

        public override int GetHashCode()
        {
            return UniqueId;
        }

        public void OnBeforeSerialize()
        {

        }

        public void OnAfterDeserialize()
        {
            Initialize(TagOne, TagTwo, new List<char>(TagOne.Length + TagTwo.Length));
        }

        public override string ToString()
        {
            return $"{UniqueId} {TagOne} {TagTwo}";
        }
    }

    // Represents a connection between two ports and keeps
    // the underlying connection
    public class PortConnection 
    {
        public Port portA;
        public Port portB;

        

        public Connection Connection { get; private set; }
        public PortConnection(Port portA, Port portB)
        {
            this.portA = portA;
            this.portB = portB;
            
            Connection = new Connection(portA.FullTag, portB.FullTag);
           
        }

        //public bool Equals(PortConnection other)
        //{
        //    return other != null && GetType() == other.GetType() && ((portA == other.portA && portB == other.portB) || (portA == other.portB && portB == other.portA));
        //}
        //public override bool Equals(object obj)
        //{
        //    return Equals(obj as Connection);
        //}

        //public override int GetHashCode()
        //{
        //    return portA.GetHashCode() ^ portB.GetHashCode();
        //}

        public override string ToString()
        {
            return $"{portA.FullTag} + {portB.FullTag}";
        }
    }

    // Not a gameobject. This class has:
    // connections: keeps track of how many times a connection has been noticed
    // parentTags: keeps track of how many times a parent has been used
    // usedParents: using Connect and Disconnect functions, the usedParents updates
    // the parentTags to connect and disconnect ports at runtime

    [Serializable]
    public class ConnectionSystem
    {
        public static bool CheckValidSystem(ConnectionSystem validSystem, ConnectionSystem systemToValidate)
        {

            // They must have the same number of connections
            if (validSystem.connections.Count != systemToValidate.connections.Count)
            {
                
                return false;
            }


            // They must have the same number of parent tags to parents
            if (validSystem.parentTags.Count != systemToValidate.parentTags.Count)
            {
               
                return false;
            }
            foreach (var pair in validSystem.connections)
            {
                var connection = pair.Key;
                var times = pair.Value;
                // if we go in this if, it means the systemToValidate has a connection that
                // the valid system doesn't have
                if (!systemToValidate.connections.TryGetValue(connection, out int validTimes))
                {
                   
                    return false;
                }
                // if a specific connection wasn't found as many times as we wanted
                if (times != validTimes)
                {
                    
                    return false;
                }
            }

            foreach (var pair in validSystem.parentTags)
            {
                var parentTag = pair.Key;
                var parentTagNums = pair.Value;

                // If the valid system doesn't have that parent tag used or
                // it doesn't have it used the same amount of ties
                if (!systemToValidate.parentTags.TryGetValue(parentTag, out int numUsed) || parentTagNums != numUsed)
                {
                    
                    return false;
                }
            }

            return true;
        }

        public static void TestShowObjects(ConnectionSystem systemToShow)
        {
            var ports = systemToShow.usedPorts;

            foreach(var port in ports)
            {
                port.portA.GameObject().GetComponentInChildren<MeshRenderer>().material.color = Color.red;
                port.portB.GameObject().GetComponentInChildren<MeshRenderer>().material.color = Color.red;
            }


                     
        }

      


        



        // stores how many times a connection has been made, where
        // connection is e.g node1.p1 <=> node2.p3
        public SerializedDictionary<Connection, int> connections = new SerializedDictionary<Connection, int>();
        // stores how many different parents with the same tag have been used
        public SerializedDictionary<string, int> parentTags = new SerializedDictionary<string, int>();

        // stores how many times a unique Gameobject (the parent) with a tag has been used
        // this is only useful when using the PortConnection class for connecting at runtime

        
        private  Dictionary<string, Dictionary<Parent, int>> usedParents = new Dictionary<string, Dictionary<Parent, int>>();

        private Dictionary<string, List<Parent>> testUsedParents = new Dictionary<string, List<Parent>>();


        private HashSet<PortConnection> usedPorts = new HashSet<PortConnection>();

        public bool Connect(Port portA, Port portB, int cableId) => Connect(new PortConnection(portA, portB));
        
        public bool Connect(PortConnection portConnection)
        {
            // Add the connection to the overall connections if it isnt there
            if (!usedPorts.Add(portConnection))
                return false;
            var connection = portConnection.Connection;
            if (!connections.ContainsKey(connection))
                connections[connection] = 0;

            connections[connection] += 1;
            // Ensure you keep the parents updated as well
            AddParent(portConnection.portA.parent);
            AddParent(portConnection.portB.parent);
            return true;
        }

        private void AddParent(Parent parent)
        {
            if (!usedParents.ContainsKey(parent.parentTag))
            {
                var parentDict = usedParents[parent.parentTag] = new Dictionary<Parent, int>();
                parentDict[parent] = 0;
            }

            // tag => parent => how many times a parent with that tag has been connected
            usedParents[parent.parentTag][parent] += 1;
            RefreshParentTagByUsedParents(parent.parentTag);


            if (!testUsedParents.ContainsKey(parent.parentTag))
            {
                var parentList = testUsedParents[parent.parentTag] = new List<Parent>();
                parentList.Add(parent);

                
            }
            parent.timesUsed += 1;
            

        }

        public bool Disconnect(Port portA, Port portB, int cableid) => Disconnect(new PortConnection(portA, portB));

        public bool Disconnect(PortConnection portConnection)
        {
            if (!usedPorts.Remove(portConnection))
                return false;

            var connection = portConnection.Connection;
            var numConn = connections[connection] -= 1;
            if (numConn == 0)
                connections.Remove(connection);

            RemoveParent(portConnection.portA.parent);
            RemoveParent(portConnection.portB.parent);
            return true;
        }

        private void RemoveParent(Parent parent)
        {
            if (!usedParents.TryGetValue(parent.parentTag, out var usedParent))
                return;

            var numUsedParent = usedParent[parent] -= 1;
            if (numUsedParent == 0)
                usedParent.Remove(parent);

            if (usedParent.Count == 0)
                usedParents.Remove(parent.parentTag);

            RefreshParentTagByUsedParents(parent.parentTag);
        }

        private void RefreshParentTagByUsedParents(string parentTag)
        {
            //var totalParents = 0;
            if (!usedParents.TryGetValue(parentTag, out var usedParent))
            {
                if (parentTags.ContainsKey(parentTag))
                    parentTags.Remove(parentTag);
                return;
            }

            var totalParents = usedParent.Count;
           
            parentTags[parentTag] = totalParents;
        }
    }

    // This displays a connection system in the editor
    public class ConnectionSystemComponent : MonoBehaviour
    {
        [field: SerializeField] public ConnectionSystem ConnectionSystem { get; private set; } = new ConnectionSystem();

        [SerializeField]
        ConnectionSystemComponent validSystemTest;

        [ContextMenu("Test Valid System")]
        private void TestValidSystem()
        {
           Debug.Log(ConnectionSystem.CheckValidSystem(validSystemTest.ConnectionSystem, ConnectionSystem));
        }

        [ContextMenu("Show Ports Used")]
        private void TestShowPorts()
        {
            ConnectionSystem.TestShowObjects(ConnectionSystem);
        }

        [ContextMenu("Print System")]
        private void PrintConnectionSystem()
        {
            
            var result = "CONNECTIONS:\n";
            foreach (var pair in ConnectionSystem.connections)
            {
                result += $"{pair.Value} {pair.Key}\n";
            }

            result += "USED PARENTS COUNT:\n";
            result += $"{ConnectionSystem.parentTags.Count}\n";

            result += "PARENTS:\n";
            foreach (var pair in ConnectionSystem.parentTags)
                result += $"{pair.Value} {pair.Key}\n";

            Debug.Log(result);

        }
    }
}
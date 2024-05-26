using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HeftyConnections
{
    public class Port : MonoBehaviour
    {
        public string portTag;
        public Parent parent;
        private string fullTag;

        public string FullTag
        {
            get
            {
                if (parent == null)
                    return $"none.{portTag}";
                return fullTag;
            }
        }

        private void Awake()
        {
            fullTag = $"{parent.parentTag}.{portTag}";
        }

    }
}
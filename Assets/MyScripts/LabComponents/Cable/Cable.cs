using UnityEngine;


public class Cable : MonoBehaviour
{
    public int cableId = 0;


    [SerializeField] CablePin myCableStart;
    [SerializeField] CablePin myCableEnd;
    [SerializeField] ConnectionManager myConnectionManager;




    private bool cableStartConnected = false;
    private bool cableEndConnected = false;

    public Pin cableStartConnectedTo = null;
    public Pin cableEndConnectedTo = null;

    private bool _cableConnected = false;


    private void OnCableStartConnected(Pin pinConnectedTo)
    {
        cableStartConnectedTo = pinConnectedTo;
        cableStartConnected = true;
        UpdateConnectionStatus();
    }

    private void OnCableEndConnected(Pin pinConnectedTo)
    {
        cableEndConnectedTo = pinConnectedTo;
        cableEndConnected = true;
        UpdateConnectionStatus();
    }

    private void OnCableStartDisconnected(Pin pinConnectedTo)
    {
        cableStartConnected = false;
        UpdateConnectionStatus();
        cableStartConnectedTo = null;
    }

    private void OnCableEndDisonnected(Pin   pinConnectedTo)
    {
        cableEndConnected = false;
        UpdateConnectionStatus();
        cableEndConnectedTo = null;
    }


    private void OnEnable()
    {
        myCableStart.OnConnectionMade += OnCableStartConnected;
        myCableEnd.OnConnectionMade += OnCableEndConnected;

        myCableStart.OnConnectionRemoved += OnCableStartDisconnected;
        myCableEnd.OnConnectionRemoved += OnCableEndDisonnected;

    }


    private void OnDisable()
    {
        myCableStart.OnConnectionMade -= OnCableStartConnected;
        myCableEnd.OnConnectionMade  -= OnCableEndConnected;

        myCableStart.OnConnectionRemoved -= OnCableStartDisconnected;
        myCableEnd.OnConnectionRemoved -= OnCableEndDisonnected;

    }


    private void UpdateConnectionStatus()
    {

        if (cableStartConnected & cableEndConnected)
        {
            // Debug.Log("Connection Detected Between " + cableStartConnectedTo.ToString() + " and " + cableEndConnectedTo.ToString() + "  via cable " + cableId.ToString());

            if (myConnectionManager != null)
            {
                myConnectionManager.OnConnectionMade(cableStartConnectedTo, cableEndConnectedTo, this);
                _cableConnected = true;
            }


            
        }


        if ((cableStartConnected == false | cableEndConnected == false) & _cableConnected)
        {
            // Debug.Log("Connection removed between" + cableStartConnectedTo.ToString() + " and " + cableEndConnectedTo.ToString() + "  via cable " + cableId.ToString());

            if (myConnectionManager != null)
            {
                myConnectionManager.OnConnectionRemoved(cableStartConnectedTo, cableEndConnectedTo, this);
                _cableConnected = false;
            }

            
        }
    }
}

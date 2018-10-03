using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NetworkClientUI : MonoBehaviour
{
    /// <summary>
    /// Lo necesitamos para crear la conexión con el servidor
    /// </summary>
    private static NetworkClient client;

    public Text IpAdressText;
    public Text ClientConnected;

    void Start()
    {
        client = new NetworkClient();
    }

    void Update()
    {
        //Obtenemos la IP en la que se encuentra el ordenador
        string ip = Network.player.ipAddress;

        this.IpAdressText.text = ip;
        this.ClientConnected.text = "Status:" + client.isConnected;
    }

    /// <summary>
    /// TODO: HAY QUE PONER LA IP CORRESPONDIENTE
    /// </summary>
    public void Connect()
    {
        if (!client.isConnected)
            client.Connect("192.168.1.131", 25000);
    }

    /// <summary>
    /// Se llama a esta función desde el código del Joystick
    /// Crea un mensaje con la información del Axis para mandarlo al servidor
    /// </summary>
    /// <param name="hDelta"></param>
    /// <param name="vDelta"></param>
    static public void SendJoystickInfo(float hDelta, float vDelta)
    {
        if (client.isConnected)
        {
            StringMessage msg = new StringMessage();
            msg.value = hDelta + "|" + vDelta;
            client.Send(888, msg);
        }
    }
}
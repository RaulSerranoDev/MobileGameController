using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityStandardAssets.CrossPlatformInput;
using System;

/// <summary>
/// Muestra el estado actual del servidor
/// </summary>
public class NetworkServerUI : MonoBehaviour
{
    //Axis virtuales en los que guardamos la información recibida del móvil
    CrossPlatformInputManager.VirtualAxis m_HVAxis;
    CrossPlatformInputManager.VirtualAxis m_VVAxis;

    // Tiene que tener los mismos nombres que el input del cliente
    string horizontalAxisName = "Horizontal";
    string verticalAxisName = "Vertical";

    //TODO: Eliminar
    public static float translation;
    public static float rotation;

    public Text IpAdressText;
    public Text NetworkActive;
    public Text NetworkConnections;

    void Start()
    {
        //Inicializamos los Axis
        m_HVAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
        CrossPlatformInputManager.RegisterVirtualAxis(m_HVAxis);
        m_VVAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
        CrossPlatformInputManager.RegisterVirtualAxis(m_VVAxis);

        //Le pasamos el puerto que tiene que escuchar
        NetworkServer.Listen(25000);

        //Registramos como listener del servidor a la función que recibe los mensajes
        NetworkServer.RegisterHandler(888, ServerReceiveMessage);
    }

    void Update()
    {
        //Obtenemos la IP en la que se encuentra el ordenador
        string ip = Network.player.ipAddress;

        this.IpAdressText.text = ip;
        this.NetworkActive.text = "Status:" + NetworkServer.active;
        this.NetworkConnections.text = "Connected:" + NetworkServer.connections.Count;
    }

    //El servidor necesita recibir los mensajes
    private void ServerReceiveMessage(NetworkMessage message)
    {
        //Estructura para guardar mensajes. Utilizada en network.
        StringMessage msg = new StringMessage();

        //Guardamos el mensaje recibido en un atributo de la estructura
        msg.value = message.ReadMessage<StringMessage>().value;

        //Los valores de axis vienen: "5.43|-0.12"
        string[] deltas = msg.value.Split('|');
        //Debug.Log(m_HVAxis.GetValue + " " + m_VVAxis.GetValue);

        translation = Convert.ToSingle(deltas[0]);
        rotation = Convert.ToSingle(deltas[1]);

        //m_HVAxis.Update(Convert.ToSingle(deltas[0]));
        //m_VVAxis.Update(Convert.ToSingle(deltas[1]));
    }   
}
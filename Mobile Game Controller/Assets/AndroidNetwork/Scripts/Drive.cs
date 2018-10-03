using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Drive : MonoBehaviour {

    #region Inspector
    /// <summary>
    /// Velocidad de movimiento del coche
    /// </summary>
    public float Speed = 10.0f;

    /// <summary>
    /// Velocidad de rotación del coche
    /// </summary>
    public float RotationSpeed = 100.0f;
    #endregion

    void Update () {
        //Obtenemos el input del mobile Controller
        //float translation = CrossPlatformInputManager.GetAxis("Vertical") * Speed;
        //float rotation = CrossPlatformInputManager.GetAxis("Horizontal") * RotationSpeed;

        //TODO: Eliminar
        float translation = NetworkServerUI.translation * Speed;
        float rotation = NetworkServerUI.rotation * RotationSpeed;

        //Avanza en el tiempo los valores
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        //Mueve el coche
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);
    }
}

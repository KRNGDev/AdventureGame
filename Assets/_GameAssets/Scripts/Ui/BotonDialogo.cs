using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonDialogo : MonoBehaviour
{

    public void OnDialogButtonPressed()
    {
        if (ControlPlayer.Instance != null)

        {
            print("Pulsa Boton");
            ControlPlayer.Instance.TryStartDialogWithNPC();
        }
    }
}

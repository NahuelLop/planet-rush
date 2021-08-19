using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpBox : MonoBehaviour
{
    public void ClosePopUp()
    {
        Destroy(gameObject);
    }
}

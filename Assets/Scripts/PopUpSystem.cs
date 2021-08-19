using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpSystem : MonoBehaviour
{
    [SerializeField] GameObject popUpBox;
    [SerializeField] string popUpText;

    public void ShowPopUp(string text)
    {
        var popUp = Instantiate(popUpBox, transform) as GameObject;
        popUp.transform.parent = transform;
        popUp.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }
}

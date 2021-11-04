using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningGoBack : MonoBehaviour
{
    public GameObject warning;

    public void BringItOn()
    {
        warning.SetActive(false);
    }
}

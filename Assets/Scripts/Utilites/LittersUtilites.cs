using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LittersUtilites : MonoBehaviour
{
    public static void EncodeTMPtext(TextMeshProUGUI _tmp, char _encodeSymbol)
    {
        _tmp.text = new string(_encodeSymbol, _tmp.text.Length);
    }

}

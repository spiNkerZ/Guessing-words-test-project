using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeUtilites : MonoBehaviour
{
    public static void OffsetIndexInArray<ArrayType>(ArrayType[] _array, ArrayType _oldElement) where ArrayType : MonoBehaviour
    {
        for (int j = 0; j < _array.Length - 1; j++)
        {
            _array[j] = _array[j + 1];
        }

        _array[_array.Length - 1] = _oldElement;
    }
}

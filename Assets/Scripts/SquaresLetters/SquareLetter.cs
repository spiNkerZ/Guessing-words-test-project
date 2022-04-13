using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SquareLetter : MonoBehaviour
{
    public bool isOpen = false;
    [SerializeField] TextMeshProUGUI textUI;
    [SerializeField] [Range(0, 1)] float speedOpen;

    public void SetLetter(string _letter)
    {
        textUI.text = _letter;
    }

    public void CloseLetter()
    {
        transform.localRotation = Quaternion.Euler(0, 180, 0);
    }

    public void OpenLetter()
    {
        StartCoroutine(OpenCorutine());

        IEnumerator OpenCorutine()
        {
            while (transform.localRotation.y > 0)
            {
                transform.Rotate(0, speedOpen, 0,Space.Self);

                yield return null;
            }
        }

        isOpen = true;
    }

    public void ActiveLetter()
    {
        gameObject.SetActive(true);
    }
    public void DeactiveLetter()
    {
        gameObject.SetActive(false);
    }
}

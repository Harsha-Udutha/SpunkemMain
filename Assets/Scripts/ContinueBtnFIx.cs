using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueBtnFIx : MonoBehaviour
{
    public void ShowFakeContinueBtn()
    {
        transform.gameObject.SetActive(true);
    }

    public void DontShowFakeContinueBtn()
    {
        transform.gameObject.SetActive(false);
    }
}

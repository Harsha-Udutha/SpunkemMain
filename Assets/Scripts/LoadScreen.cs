using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour
{
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        yield return new WaitForSeconds(1.5f);
        image.color=new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(image.color.a,1f,1f));
    }
}

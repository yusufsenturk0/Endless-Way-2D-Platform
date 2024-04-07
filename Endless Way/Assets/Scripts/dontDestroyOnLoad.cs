using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontDestroyOnLoad : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        // Bu nesneyi yok etme.
        DontDestroyOnLoad(this.gameObject);
    }
}

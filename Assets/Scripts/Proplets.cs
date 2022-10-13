using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proplets : MonoBehaviour
{
    public int ID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        gameObject.layer = 6;
    }
}

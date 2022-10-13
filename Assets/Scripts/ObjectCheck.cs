using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCheck : MonoBehaviour
{
    public int IDRequired;
    public int level;
    public int GoodToGo = 0;
    public GameObject GreenLight;
    private RaycastHit Ray;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position,transform.forward,out Ray,2,~6))
        {
                if (Ray.collider.GetComponent<Proplets>().ID == IDRequired)
                {
                    GoodToGo = 1;
                    GreenLight.SetActive(true);
            }
        }
        if (!Physics.Raycast(transform.position, transform.forward, out Ray, 2, ~6))
        {
            GoodToGo = 0;
            GreenLight.SetActive(false);
        }
    }
}

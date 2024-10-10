using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPS : MonoBehaviour
{
     [SerializeField]private GameObject waterPS;

    private void Start()
    {
        Instantiate(waterPS, new Vector2(-133.5f, 9.99f),Quaternion.identity);
        Instantiate(waterPS, new Vector2(-109.55f, 6.53f), Quaternion.identity);
        Instantiate(waterPS, new Vector2(-95.52f, -1.37f), Quaternion.identity);
        Instantiate(waterPS, new Vector2(-75.5f, -3.44f), Quaternion.identity);
        Instantiate(waterPS, new Vector2(-91.5f, 13.59f), Quaternion.identity);
       
    }
    private void Update()
    {
        
    }

}

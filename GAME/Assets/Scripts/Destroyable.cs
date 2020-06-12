using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : Shootable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Die()
    {
        if (objectType == (int)objectTypeEnum.delete)
        {
            Destroy(gameObject);
        }
    }
}

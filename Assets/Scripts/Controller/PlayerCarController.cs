using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : SimpleCarController
{
    void Start()
    {
        this.m_horizontalInput = 0.0f;
        this.m_verticalInput = 1.0f;
    }

    void Update()
    {
        
    }
}

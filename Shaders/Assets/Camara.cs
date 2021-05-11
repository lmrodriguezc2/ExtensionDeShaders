using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    [Range(-2.0f, 2.0f)]
    public float uno;
    [Range(-2.0f, 0.0f)]
    public float dos;

    // Start is called before the first frame update
    void Start()
    {
		Matrix4x4 trs = Camera.main.projectionMatrix;
        trs[0, 2] = uno;
        trs[1, 2] = dos;
		
		trs[0, 3] = -2f;
		trs[1, 3] = -2f;
		trs[2, 3] = -2f;
        Camera.main.projectionMatrix = trs.transpose;
    }
	
	
    // Update is called once per frame
    void Update()
    {

        
    }
}


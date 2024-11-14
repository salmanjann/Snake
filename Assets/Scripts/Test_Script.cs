using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Script : MonoBehaviour
{
    public Transform A;
    public Transform B;
    public Transform C;
    public float translationX; // X offset
    public float translationY; // Y offset

    private void Start() {
        Matrix4x4 transformationMatrix = Matrix4x4.identity;
        transformationMatrix.m03 = translationX;
        transformationMatrix.m13 = translationY;

        A.position =  transformationMatrix.MultiplyPoint(A.position);
        B.position =  transformationMatrix.MultiplyPoint(B.position);
        C.position =  transformationMatrix.MultiplyPoint(C.position);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(A.position, B.position);
        Gizmos.DrawLine(B.position, C.position);
        Gizmos.DrawLine(A.position, C.position);
    }

}

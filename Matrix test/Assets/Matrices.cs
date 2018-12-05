using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrices : MonoBehaviour {

    // Use this for initialization
    void Start() {

        //matricesTransformations();

        Vector2 start = new Vector2(0.7f, 0.8f);
        Vector2 end = new Vector2(-0.7f, -0.8f);
        if (line_clip(ref start, ref end))
        {
            print("The point " + start.ToString() + "," + end.ToString() + " was Accepted");
        }

        start = new Vector2(-2f, 1.5f);
        end = new Vector2(.8f, 1.5f);

        if (!line_clip(ref start, ref end))
        {
            print("The point " + start.ToString() + "," + end.ToString() + " was rejected");
        }

        start = new Vector2(0f, 1.5f);
        end = new Vector2(1.5f, 0f);

        if (line_clip(ref start, ref end))
        {
            print("The point " + start.ToString() + "," + end.ToString() + " was accepted");
        }
    }

    public static void matricesTransformations()
    {
        Vector3 cameraPosition = new Vector3(10, 1, 48);
        Vector3 cameraUp = new Vector3(-1, -2, 8);
        Vector3 cameraLookAt = new Vector3(-2, 8, 2);
        Vector3 cameraForward = (cameraLookAt - cameraPosition).normalized;
        cameraUp.Normalize();

        Vector3[] vertices = new Vector3[8];
        vertices[0] = new Vector3(1, 1, 1);
        vertices[1] = new Vector3(1, -1, 1);
        vertices[2] = new Vector3(-1, -1, 1);
        vertices[3] = new Vector3(-1, 1, 1);
        vertices[4] = new Vector3(-1, 1, -1);
        vertices[5] = new Vector3(-1, -1, -1);
        vertices[6] = new Vector3(1, -1, -1);
        vertices[7] = new Vector3(1, 1, -1);

        print("ORIGINAL VERTICES + \n");
        printVector3Array(vertices);

        Vector3 startingAxis = new Vector3(8, -2, -2);
        startingAxis.Normalize();

        Matrix4x4 rotationMatrix = Matrix4x4.Rotate(Quaternion.AngleAxis(-45, startingAxis));

        print("ROTATION MATRIX");
        print(rotationMatrix.ToString());

        Vector3[] verticesAfterRotation = transformVertices(vertices, rotationMatrix);

        print("VERTICES AFTER ROTATION");

        printVector3Array(verticesAfterRotation);

        Matrix4x4 scalingMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(8, 4, 2));

        print("SCALING MATRIX");
        print(scalingMatrix.ToString());

        Vector3[] verticesAfterScale = transformVertices(verticesAfterRotation, scalingMatrix);

        print("VERTICES AFTER SCALING");
        printVector3Array(verticesAfterScale);

        Matrix4x4 translationMatrix = Matrix4x4.TRS(new Vector3(-2, -4, -1), Quaternion.identity, Vector3.one);

        print("translation matrix");
        print(translationMatrix.ToString());

        Vector3[] verticesAfterTranslation = transformVertices(verticesAfterScale, translationMatrix);

        print("VERTICES AFTER TRANSLATION");
        printVector3Array(verticesAfterTranslation);

        Matrix4x4 singleMatrixofTransformations = translationMatrix * scalingMatrix * rotationMatrix;

        print("MULTIPLICATION OF MATRICES (T * S * R");
        print(singleMatrixofTransformations.ToString());

        Vector3[] finalSingleMatrix = transformVertices(vertices, singleMatrixofTransformations);

        print("POINTS AFTER T*S*R");
        printVector3Array(finalSingleMatrix);

        Quaternion cameraRotation = Quaternion.LookRotation(cameraForward, cameraUp);

        Matrix4x4 viewingMatrix = Matrix4x4.TRS(-cameraPosition, cameraRotation, Vector3.one);

        print("VIEWING MATRIX");
        print(viewingMatrix.ToString());

        Vector3[] verticesAfterViewingMatrix = transformVertices(verticesAfterTranslation, viewingMatrix);

        print("VERTICES AFTER VIEWING MATRIX");
        printVector3Array(verticesAfterViewingMatrix);

        Matrix4x4 projectionMatrix = Matrix4x4.Perspective(90, 1920 / 1080, 1, 1000);

        print("PROJECTION MATRIX");
        print(projectionMatrix.ToString());

        Vector3[] verticesAfterProjection = transformVertices(verticesAfterViewingMatrix, projectionMatrix);

        print("VERTICES AFTER PROJECTION");
        printVector3Array(verticesAfterProjection);

        Matrix4x4 singleMatrixForEverything = projectionMatrix * viewingMatrix * singleMatrixofTransformations;

        print("SINGLE MATRIX OF EVERYTHING");
        print(singleMatrixForEverything.ToString());

        Vector3[] finalImage = transformVertices(vertices, singleMatrixForEverything);

        print("FINAL IMAGE");
        printVector3Array(finalImage);

        Vector3[] originalVerticesByMatrixForEverything = transformVertices(vertices, singleMatrixForEverything);

        print("ORIGINAL VERTS BY MATRIX FOR EVERYTHING");
        printVector3Array(originalVerticesByMatrixForEverything);
    }
   
    private bool line_clip(ref Vector2 startPoint, ref Vector2 endPoint)
        { 
        OutCode startOutcode = new OutCode(startPoint);
        OutCode endOutcode = new OutCode(endPoint);
 

        if ((startOutcode == new OutCode()) && (endOutcode == new OutCode())) //Trivial Acceptance
        {
            return true;
        }

        if ((startOutcode & endOutcode) != new OutCode())  // Trivial Rejection
        {
            return false;
        }

     
        for (int udlr = 0;udlr<4;udlr++)
            {
                if (startOutcode.udlr[udlr])
                {
                    startPoint = lineIntercept(startPoint, endPoint, udlr);
                    if (new OutCode(startPoint) == new OutCode())// new point in viewport
                    {
                        return line_clip(ref endPoint, ref startPoint);
                    }
                }
            }

        return false;

     
    }

    private Vector2 lineIntercept(Vector2 startPoint, Vector2 endPoint, int udlr)
    {
        float m = getSlope(startPoint, endPoint);

        switch (udlr)
        {
            case 0: // Top Edge  y = 1

                return new Vector2(startPoint.x + (1/m) * (1 - startPoint.y), 1);

            case 1: // Bottom Edge  y = -1

                return new Vector2(startPoint.x + (1 / m) * (-1 - startPoint.y), -1);

            case 2: // Left Edge x = -1

                return new Vector2(-1, ( startPoint.y + (m * (-1 - startPoint.x))));

            case 3: // Right Edge x = 1

                return new Vector2(1, (startPoint.y + (m * (1 - startPoint.x))));

            default :

                return new Vector2();
        }
    }

    private float getSlope(Vector2 v1, Vector2 v2)
    {
        return (v2.y - v1.y) / (v2.x - v1.x);
    }

    public static void printVector3Array(Vector3[] arrayToBePrinted)
    {
        string array = "";
        for(int i = 0; i < arrayToBePrinted.Length; i++)
        {
            array += arrayToBePrinted[i] + "\n";
        }
        print(array);
    }
  
    public static Vector3[] transformVertices(Vector3[] vertices, Matrix4x4 tranformMatrix)
    {
        Vector3[] imageAfterTransformation = new Vector3[8];

        for (int i = 0; i < vertices.Length; i++)
            imageAfterTransformation[i] = tranformMatrix * new Vector4(vertices[i].x, vertices[i].y, vertices[i].z, 1);
        
        return imageAfterTransformation;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrices : MonoBehaviour {

    // Use this for initialization
    void Start() {

        //matricesTransformations();

       
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

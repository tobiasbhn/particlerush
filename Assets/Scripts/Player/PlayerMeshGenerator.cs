using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriangleNet.Geometry;

public class PlayerMeshGenerator : MonoBehaviour {

    //INTERNAL MASS TO WORK WITH
    private int mass;

    //MESHES
    private TriangleNet.Mesh generatedMesh;
    [HideInInspector] public UnityEngine.Mesh unityMesh;

    //ARRAYs KEEPING MESH_RELATED INFO
    private Vector3[] sphereCoords;
    private Vector3[] planeCoords;
    private int[] triangles;
    private Vector3[] vectors;
    private List<int>[] indexes;

    //WAVE VARIABLES
    private bool areWavesToSmallToShow = true;
    private float waveImpactTime;
    private float smallestCurrentWave;
    private float[] distanceFromInpactToPoint;
    private float[] waveHeightInSpecificPoint;

    public void CreateShape(float _mass) {
        if (Mathf.FloorToInt(_mass) != mass) {
            if (_mass > ConstantManager.PLAYER_MAX_MESH_GENERATION_SIZE)
                mass = ConstantManager.PLAYER_MAX_MESH_GENERATION_SIZE;
            else
                mass = Mathf.FloorToInt(_mass);
            if (_mass < ConstantManager.PLAYER_MIN_MESH_GENERATION_SIZE)
                mass = ConstantManager.PLAYER_MIN_MESH_GENERATION_SIZE;
            else
                mass = Mathf.FloorToInt(_mass);

            DefineSphereVertices();
            DefinePlaneVertices();
            StartDelaunayTriangulation();
            DefineTriangles();
            UpdateMesh();
        }
    }

    //called once on new Impact
    public void NewCollision(Vector3 pos) {
        //get nearest vertex
        var dist = Vector3.Distance(pos, sphereCoords[0]);
        var num = 0;
        for (int i = 1; i < sphereCoords.Length; i++)
            if (Vector3.Distance(pos, sphereCoords[i]) < dist) {
                dist = Vector3.Distance(pos, sphereCoords[i]);
                num = i;
            }

        //define waves and wave-heights
        Vector3 center = sphereCoords[num];
        distanceFromInpactToPoint = new float[sphereCoords.Length];
        waveHeightInSpecificPoint = new float[sphereCoords.Length];
        areWavesToSmallToShow = false;
        smallestCurrentWave = ConstantManager.PLAYER_WAVES_MIN_WAVE_MOTION;
        for (int i = 0; i < sphereCoords.Length; i++)
            distanceFromInpactToPoint[i] = Vector3.Distance(center, sphereCoords[i]);
        waveHeightInSpecificPoint = distanceFromInpactToPoint;
        waveImpactTime = Time.time;
    }

    //called every Update
    public void UpdateWaves() {
        if (!areWavesToSmallToShow && distanceFromInpactToPoint != null) {
            float maxWaveLenght = Vector3.Distance(sphereCoords[0], new Vector3(0, 0, 0));
            for (int i = 0; i < sphereCoords.Length && i < distanceFromInpactToPoint.Length && i < waveHeightInSpecificPoint.Length; i++) {
                float SinX = ((Time.time - waveImpactTime - (distanceFromInpactToPoint[i] / ConstantManager.PLAYER_WAVES_OFFSET)) * ConstantManager.PLAYER_WAVES_SPEED);
                double SinY = Mathf.Sin(SinX) / (ConstantManager.PLAYER_WAVES_HEIGHT + waveHeightInSpecificPoint[i] * ConstantManager.PLAYER_WAVES_DECREASE_WAVES_OUTSIDE);
                float distToMove = (float)SinY;
                if (distToMove >= maxWaveLenght)
                    distToMove = maxWaveLenght;
                if (distToMove <= -maxWaveLenght)
                    distToMove = -maxWaveLenght;
                for (int x = 0; x < indexes[i].Count; x++)
                    vectors[indexes[i][x]] = Vector3.MoveTowards(sphereCoords[i], new Vector3(0, 0, 0), distToMove);
                waveHeightInSpecificPoint[i] += ConstantManager.PLAYER_WAVES_REDUCION_PER_FRAME * Time.deltaTime * 60;
                if (waveHeightInSpecificPoint[i] > smallestCurrentWave)
                    smallestCurrentWave = waveHeightInSpecificPoint[i];
            }
            if (smallestCurrentWave > ConstantManager.PLAYER_WAVES_MIN_WAVE_MOTION)
                areWavesToSmallToShow = true;
            UpdateMesh();
        }
    }
    
    private void UpdateMesh() {
        unityMesh.Clear();
        unityMesh.vertices = vectors;
        unityMesh.triangles = triangles;
        unityMesh.RecalculateNormals();
    }

    //define x points in a spheric form, saved in sphereCoords[]
    void DefineSphereVertices() {
        sphereCoords = new Vector3[mass];
        float inc = Mathf.PI * (3 - Mathf.Sqrt(5));
        float off = 2.0f / mass;
        float x = 0;
        float y = 0;
        float z = 0;
        float r = 0;
        float phi = 0;

        for (var k = 0; k < mass; k++) {
            y = k * off - 1 + (off / 2);
            r = Mathf.Sqrt(1 - y * y);
            phi = k * inc;
            x = Mathf.Cos(phi) * r;
            z = Mathf.Sin(phi) * r;
            sphereCoords[k] = new Vector3(x, y, z);
        }
    }

    //project the sphereCoords onto a plane, saved in planeCoords[]
    void DefinePlaneVertices() {
        planeCoords = new Vector3[mass];
        for (int i = 0; i < sphereCoords.Length; i++) {
            var fromVec = new Vector3(0, -1, 0);
            var vec = (sphereCoords[i] - fromVec).normalized;
            var unit = 1 / vec.y;
            var result = fromVec + vec * unit;
            planeCoords[i] = result;
        }
    }

    //create 2D Delauny Triangulation out of the planeCoords[]
    void StartDelaunayTriangulation() {
        TriangleNet.Geometry.Polygon polygon = new TriangleNet.Geometry.Polygon();
        for (int i = 0; i < planeCoords.Length; i++)
            polygon.Add(new TriangleNet.Geometry.Vertex(planeCoords[i].x, planeCoords[i].z));
        TriangleNet.Meshing.ConstraintOptions options = new TriangleNet.Meshing.ConstraintOptions() { ConformingDelaunay = false };
        generatedMesh = (TriangleNet.Mesh)polygon.Triangulate(options);
    }

    //create Mesh by projection the 2D Triangulation back to a Sphere
    void DefineTriangles() {
        int countNor = 0;
        int countRev = generatedMesh.Triangles.Count * 3 - 1;
        var numTris = (generatedMesh.Triangles.Count * 3) + 9;

        triangles = new int[numTris];
        vectors = new Vector3[numTris];
        indexes = new List<int>[sphereCoords.Length];
        for (int i = 0; i < indexes.Length; i++)
            indexes[i] = new List<int>();

        //close hole that stays after projection the 2D Delauny Triangulation back to the sphere
        vectors[0] = sphereCoords[2];
        vectors[1] = sphereCoords[1];
        vectors[2] = sphereCoords[0];
        vectors[3] = sphereCoords[4];
        vectors[4] = sphereCoords[1];
        vectors[5] = sphereCoords[2];
        vectors[6] = sphereCoords[0];
        vectors[7] = sphereCoords[1];
        vectors[8] = sphereCoords[3];
        indexes[0].AddRange(new int[] { 2, 6 });
        indexes[1].AddRange(new int[] { 7, 4, 1 });
        indexes[2].AddRange(new int[] { 0, 5 });
        indexes[3].Add(8);
        indexes[4].Add(3);
        for (int i = 0; i < 9; i++)
            triangles[numTris - i - 1] = i;
        countNor = 9;

        //project 2D Delauny Triangulation back to a sphere
        foreach (var triangle in generatedMesh.Triangles) {
            var num = triangle.vertices[0].hash;
            vectors[countNor] = sphereCoords[num];
            triangles[countRev] = countNor;
            indexes[num].Add(countNor);
            countNor++; countRev--;

            num = triangle.vertices[1].hash;
            vectors[countNor] = sphereCoords[num];
            triangles[countRev] = countNor;
            indexes[num].Add(countNor);
            countNor++; countRev--;

            num = triangle.vertices[2].hash;
            vectors[countNor] = sphereCoords[num];
            triangles[countRev] = countNor;
            indexes[num].Add(countNor);
            countNor++; countRev--;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ParticleMeshScript {

    [HideInInspector]
    public static Mesh mesh1 = new Mesh()
    {
        vertices = new Vector3[] {
             new Vector3(0.1f, 0f, -1f), //3
                new Vector3(-0.6f, 0.8f, -0.1f), //5
                new Vector3(-0.7f, -0.4f, 0.6f), //2
                new Vector3(-0.7f, -0.4f, 0.6f), //2
                new Vector3(-0.6f, 0.8f, -0.1f), //5
                new Vector3(0.6f, 0.4f, 0.7f), //4
                new Vector3(0.6f, 0.4f, 0.7f), //4
                new Vector3(-0.6f, 0.8f, -0.1f), //5
                new Vector3(0.1f, 0f, -1f), //3
                new Vector3(0.1f, 0f, -1f), //3
                new Vector3(-0.7f, -0.4f, 0.6f), //2
                new Vector3(0.6f, -0.8f, 0f), //1
                new Vector3(-0.7f, -0.4f, 0.6f), //2
                new Vector3(0.6f, 0.4f, 0.7f), //4
                new Vector3(0.6f, -0.8f, 0f), //1
                new Vector3(0.6f, 0.4f, 0.7f), //4
                new Vector3(0.1f, 0f, -1f), //3
                new Vector3(0.6f, -0.8f, 0f) //1
        },
        triangles = new int[] {
            17, 16, 15,
            14, 13, 12,
            11, 10, 9,
            8, 7, 6,
            5, 4, 3,
            2, 1, 0
        }
    };

    [HideInInspector]
    public static Mesh mesh2 = new Mesh()
    {
        vertices = new Vector3[] {
             new Vector3(0.1f, -0.2f, -1.0f), //2
                new Vector3(-0.6f, -0.5f, 0.6f), //1
                new Vector3(0.6f, -0.8f, 0f), //0
                new Vector3(-0.9f, 0.5f, -0.2f), //4
                new Vector3(-0.6f, -0.5f, 0.6f), //1
                new Vector3(0.1f, -0.2f, -1.0f), //2
                new Vector3(-0.9f, 0.5f, -0.2f), //4
                new Vector3(0.6f, 0.2f, 0.8f), //3
                new Vector3(-0.6f, -0.5f, 0.6f), //1
                new Vector3(0.1f, -0.2f, -1.0f), //2
                new Vector3(0.5f, 0.8f, -0.3f), //5
                new Vector3(-0.9f, 0.5f, -0.2f), //4
                new Vector3(0.6f, 0.2f, 0.8f), //3
                new Vector3(0.5f, 0.8f, -0.3f), //5
                new Vector3(0.6f, -0.8f, 0f), //0
                new Vector3(0.1f, -0.2f, -1.0f), //2
                new Vector3(0.6f, -0.8f, 0f), //0
                new Vector3(0.5f, 0.8f, -0.3f), //5
                new Vector3(-0.6f, -0.5f, 0.6f), //1
                new Vector3(0.6f, 0.2f, 0.8f), //3
                new Vector3(0.6f, -0.8f, 0f), //0
                new Vector3(-0.9f, 0.5f, -0.2f), //4
                new Vector3(0.5f, 0.8f, -0.3f), //5
                new Vector3(0.6f, 0.2f, 0.8f) //3
        },
        triangles = new int[] {
            23, 22, 21,
            20, 19, 18,
            17, 16, 15,
            14, 13, 12,
            11, 10, 9,
            8, 7, 6,
            5, 4, 3,
            2, 1, 0
        }
    };

    [HideInInspector]
    public static Mesh mesh3 = new Mesh()
    {
        vertices = new Vector3[] {
             new Vector3(0.1f, -0.3f, -1f), //2
                new Vector3(-0.6f, -0.6f, 0.6f), //1
                new Vector3(0.5f, -0.9f, 0f), //0
                new Vector3(-0.9f, 0.3f, -0.2f), //4
                new Vector3(-0.6f, -0.6f, 0.6f), //1
                new Vector3(0.1f, -0.3f, -1f), //2
                new Vector3(0.5f, -0.9f, 0f), //0
                new Vector3(-0.6f, -0.6f, 0.6f), //1
                new Vector3(0.6f, 0f, 0.8f), //3
                new Vector3(-0.1f, 0.9f, 0.5f), //6
                new Vector3(-0.6f, -0.6f, 0.6f), //1
                new Vector3(-0.9f, 0.3f, -0.2f), //4
                new Vector3(0.7f, 0.6f, -0.4f), //5
                new Vector3(-0.1f, 0.9f, 0.5f), //6
                new Vector3(-0.9f, 0.3f, -0.2f), //4
                new Vector3(0.6f, 0f, 0.8f), //3
                new Vector3(-0.6f, -0.6f, 0.6f), //1
                new Vector3(-0.1f, 0.9f, 0.5f), //6
                new Vector3(-0.1f, 0.9f, 0.5f), //6
                new Vector3(0.7f, 0.6f, -0.4f), //5
                new Vector3(0.6f, 0f, 0.8f), //3
                new Vector3(0.5f, -0.9f, 0f), //0
                new Vector3(0.7f, 0.6f, -0.4f), //5
                new Vector3(0.1f, -0.3f, -1f), //2
                new Vector3(0.7f, 0.6f, -0.4f), //5
                new Vector3(0.5f, -0.9f, 0f), //0
                new Vector3(0.6f, 0f, 0.8f), //3
                new Vector3(0.1f, -0.3f, -1f), //2
                new Vector3(0.7f, 0.6f, -0.4f), //5
                new Vector3(-0.9f, 0.3f, -0.2f) //4
        },
        triangles = new int[] {
            29, 28, 27,
            26, 25, 24,
            23, 22, 21,
            20, 19, 18,
            17, 16, 15,
            14, 13, 12,
            11, 10, 9,
            8, 7, 6,
            5, 4, 3,
            2, 1, 0
        }
    };

    [HideInInspector]
    public static Mesh mesh4 = new Mesh()
    {
        vertices = new Vector3[] {
             new Vector3(0.1f, -0.4f, -0.9f), //2
                new Vector3(-0.6f, -0.6f, 0.5f), //1
                new Vector3(0.5f, -0.9f, 0f), //0
                new Vector3(-1f, 0.1f, -0.2f), //4
                new Vector3(-0.6f, -0.6f, 0.5f), //1
                new Vector3(0.1f, -0.4f, -0.9f), //2
                new Vector3(0.5f, -0.9f, 0f), //0
                new Vector3(-0.6f, -0.6f, 0.5f), //1
                new Vector3(0.6f, -0.1f, 0.8f), //3
                new Vector3(-0.2f, 0.6f, 0.8f), //6
                new Vector3(-1f, 0.1f, -0.2f), //4
                new Vector3(-0.3f, 0.9f, -0.4f), //7
                new Vector3(0.1f, -0.4f, -0.9f), //2
                new Vector3(-0.3f, 0.9f, -0.4f), //7
                new Vector3(-1f, 0.1f, -0.2f), //4
                new Vector3(0.6f, -0.1f, 0.8f), //3
                new Vector3(-0.6f, -0.6f, 0.5f), //1
                new Vector3(-0.2f, 0.6f, 0.8f), //6
                new Vector3(-1f, 0.1f, -0.2f), //4
                new Vector3(-0.2f, 0.6f, 0.8f), //6
                new Vector3(-0.6f, -0.6f, 0.5f), //1
                new Vector3(-0.2f, 0.6f, 0.8f), //6
                new Vector3(-0.3f, 0.9f, -0.4f), //7
                new Vector3(0.8f, 0.4f, -0.5f), //5
                new Vector3(-0.2f, 0.6f, 0.8f), //6
                new Vector3(0.8f, 0.4f, -0.5f), //5
                new Vector3(0.6f, -0.1f, 0.8f), //3
                new Vector3(0.5f, -0.9f, 0f), //0
                new Vector3(0.8f, 0.4f, -0.5f), //5
                new Vector3(0.1f, -0.4f, -0.9f), //2
                new Vector3(0.8f, 0.4f, -0.5f), //5
                new Vector3(0.5f, -0.9f, 0f), //0
                new Vector3(0.6f, -0.1f, 0.8f), //3
                new Vector3(0.1f, -0.4f, -0.9f), //2
                new Vector3(0.8f, 0.4f, -0.5f), //5
                new Vector3(-0.3f, 0.9f, -0.4f) //7
        },
        triangles = new int[] {
            35, 34, 33,
            32, 31, 30,
            29, 28, 27,
            26, 25, 24,
            23, 22, 21,
            20, 19, 18,
            17, 16, 15,
            14, 13, 12,
            11, 10, 9,
            8, 7, 6,
            5, 4, 3,
            2, 1, 0
        }
    };

    [HideInInspector]
    public static Mesh mesh5 = new Mesh()
    {
        vertices = new Vector3[] {
             new Vector3(0.1f, -0.4f, -0.9f), //2
                new Vector3(-0.5f, -0.7f, 0.5f), //1
                new Vector3(0.5f, -0.9f, 0f), //0
                new Vector3(-1f, 0f, -0.2f), //4
                new Vector3(-0.5f, -0.7f, 0.5f), //1
                new Vector3(0.1f, -0.4f, -0.9f), //2
                new Vector3(0.5f, -0.9f, 0f), //0
                new Vector3(-0.5f, -0.7f, 0.5f), //1
                new Vector3(0.6f, -0.2f, 0.8f), //3
                new Vector3(-0.2f, 0.4f, 0.9f), //6
                new Vector3(-1f, 0f, -0.2f), //4
                new Vector3(-0.3f, 0.7f, -0.7f), //7
                new Vector3(0.1f, -0.4f, -0.9f), //2
                new Vector3(-0.3f, 0.7f, -0.7f), //7
                new Vector3(-1f, 0f, -0.2f), //4
                
                new Vector3(0.6f, -0.2f, 0.8f), //3
                new Vector3(-0.5f, -0.7f, 0.5f), //1
                new Vector3(-0.2f, 0.4f, 0.9f), //6
                new Vector3(-1f, 0f, -0.2f), //4
                new Vector3(-0.2f, 0.4f, 0.9f), //6
                new Vector3(-0.5f, -0.7f, 0.5f), //1
                new Vector3(-0.2f, 0.4f, 0.9f), //6
                new Vector3(-0.3f, 0.7f, -0.7f), //7
                new Vector3(0.4f, 0.9f, 0.2f), //8
                new Vector3(-0.3f, 0.7f, -0.7f), //7
                new Vector3(0.8f, 0.2f, -0.5f), //5
                new Vector3(0.4f, 0.9f, 0.2f), //8
                new Vector3(0.5f, -0.9f, 0f), //0
                new Vector3(0.8f, 0.2f, -0.5f), //5
                new Vector3(0.1f, -0.4f, -0.9f), //2
                
                new Vector3(0.4f, 0.9f, 0.2f), //8
                new Vector3(0.8f, 0.2f, -0.5f), //5
                new Vector3(0.6f, -0.2f, 0.8f), //3
                new Vector3(0.5f, -0.9f, 0f), //0
                new Vector3(0.6f, -0.2f, 0.8f), //3
                new Vector3(0.8f, 0.2f, -0.5f), //5
                new Vector3(-0.2f, 0.4f, 0.9f), //6
                new Vector3(0.4f, 0.9f, 0.2f), //8
                new Vector3(0.6f, -0.2f, 0.8f), //3
                new Vector3(0.1f, -0.4f, -0.9f), //2
                new Vector3(0.8f, 0.2f, -0.5f), //5
                new Vector3(-0.3f, 0.7f, -0.7f), //7
        },
        triangles = new int[] {
            41, 40, 39,
            38, 37, 36,
            35, 34, 33,
            32, 31, 30,
            29, 28, 27,
            26, 25, 24,
            23, 22, 21,
            20, 19, 18,
            17, 16, 15,
            14, 13, 12,
            11, 10, 9,
            8, 7, 6,
            5, 4, 3,
            2, 1, 0
        }
    };
  
    [HideInInspector] public static bool thisFileLoaded = true;
}

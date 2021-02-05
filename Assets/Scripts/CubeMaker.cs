using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CubeMaker : MonoBehaviour
{
    [SerializeField]
    Transform _cube = null;
    [SerializeField]
    Transform _particels = null;

    [Range(1, 100)]
    public int numberOfCubes = 5;

    public void BuildMania()
    {
        float x = numberOfCubes + (int)Mathf.Ceil(numberOfCubes * 2);
        Camera.main.transform.position = new Vector3(-x, 0, -x);

        Vector3 cubeCenter = Vector3.one * (numberOfCubes - 1) * 0.625f;

        transform.position = cubeCenter;
        Camera.main.transform.parent.transform.position = cubeCenter;

        CombineInstance[] combine = new CombineInstance[numberOfCubes * numberOfCubes * numberOfCubes];

        Transform obj = null;

        for(int i = 0; i < numberOfCubes; i++)
        {
            for(int j = 0; j < numberOfCubes; j++)
            {
                for(int k = 0; k < numberOfCubes; k++)
                {
                    Vector3 spawnPosition = new Vector3(k * 1.25f, j * 1.25f, i * 1.25f);

                    obj = Instantiate(_cube, spawnPosition, Quaternion.identity, transform).GetComponent<Transform>();

                    if (numberOfCubes > 10)
                    {
                        UInt32 index = (UInt32)(k + numberOfCubes * (j + numberOfCubes * i));

                        MeshFilter filter = obj.GetComponent<MeshFilter>();
                        combine[index].mesh = filter.sharedMesh;
                        combine[index].transform = filter.transform.localToWorldMatrix;
                        filter.gameObject.SetActive(false);
                    }
                    else
                    {
                        Renderer rend = obj.GetComponent<Renderer>();
                        rend.material.color = UnityEngine.Random.ColorHSV();
                    }
                }
            }
        }

        if (numberOfCubes > 10)
        {
            if(obj != null) 
                obj.GetComponent<Renderer>().sharedMaterial.color = UnityEngine.Random.ColorHSV();

            transform.GetComponent<MeshFilter>().mesh = new Mesh();
            transform.GetComponent<MeshFilter>().mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
            transform.gameObject.SetActive(true);

            transform.rotation = Quaternion.identity;
            transform.position = Vector3.zero;

            _particels.position = cubeCenter;
        }

        if(numberOfCubes > 30)
        {
            _particels.gameObject.SetActive(false);
        }
    }
}

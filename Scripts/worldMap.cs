using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldMap : MonoBehaviour
{
    [SerializeField] private RenderTexture MiniMapTexture;
    [SerializeField] GameObject MiniMapTextureHolder;

    public GameObject[] MapViewers;
    

    public void click(int quadNum)
    {
        foreach (GameObject mapViewer in MapViewers)
        {
            mapViewer.SetActive(false);
        }

        GameObject quadrantViewer = MapViewers[quadNum - 1];

        quadrantViewer.SetActive(true);
        MiniMapTextureHolder.GetComponent<TextureToWorldCoordinates>().renderCamera = quadrantViewer.gameObject.GetComponent<Camera>();

        Ray ray = quadrantViewer.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity);
        GameObject.Find("TurnManager").GetComponent<turnManager>().chosenQuadrant = hit.collider.gameObject;
    }
}

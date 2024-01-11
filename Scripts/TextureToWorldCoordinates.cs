using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextureToWorldCoordinates : MonoBehaviour, IPointerClickHandler
{
    public Camera renderCamera;
    private RectTransform textureTransform;
    [SerializeField] GameObject movementCamera;

    private void Awake()
    {
        textureTransform = GetComponent<RectTransform>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(textureTransform, eventData.position, null, out Vector2 localClick);

        localClick.x = (textureTransform.rect.xMin * -1) - (localClick.x * -1);
        localClick.y = (textureTransform.rect.yMin * -1) - (localClick.y * -1);

        Vector2 viewportClick = new Vector2(localClick.x / textureTransform.rect.size.x, localClick.y / textureTransform.rect.size.y);

        LayerMask layer = LayerMask.GetMask("InteractableLayer");

        Ray ray = renderCamera.ViewportPointToRay(new Vector3(viewportClick.x, viewportClick.y, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layer))
        {
            movementCamera.GetComponent<CharacterController>().enabled = false;
            movementCamera.transform.position = new Vector3(hit.point.x, movementCamera.transform.position.y, hit.point.z);
            movementCamera.GetComponent<CharacterController>().enabled = true;
        }
    }
}

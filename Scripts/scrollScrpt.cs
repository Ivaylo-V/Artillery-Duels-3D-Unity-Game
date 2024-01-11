using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollScrpt : MonoBehaviour
{

    private float distance = 100;

    private void FixedUpdate()
    {
        scroll();

        RaycastHit hit;

        if(Physics.Raycast(gameObject.transform.position,-Vector3.up,out hit))
        {
            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, hit.transform.position.y + distance, gameObject.transform.position.z);
            }
        }


    }

    private void scroll()
    {
        if (gameObject.transform.position.y <= 200 && gameObject.transform.position.y >= 100)
        {
            Vector2 scrollAmount = Input.mouseScrollDelta;
            distance -= scrollAmount.y;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - scrollAmount.y, gameObject.transform.position.z);

        }
        else if (gameObject.transform.position.y > 200)
        {
            Vector2 scrollAmount = Input.mouseScrollDelta;

            if (scrollAmount.y > 0)
            {
                distance -= scrollAmount.y;
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - scrollAmount.y, gameObject.transform.position.z);
            }
        }
        else if (gameObject.transform.position.y < 100)
        {
            Vector2 scrollAmount = Input.mouseScrollDelta;

            if (scrollAmount.y < 0)
            {
                distance -= scrollAmount.y;
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - scrollAmount.y, gameObject.transform.position.z);
            }
        }


    }
}

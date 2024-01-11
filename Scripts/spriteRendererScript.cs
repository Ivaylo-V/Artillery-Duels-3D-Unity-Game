using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteRendererScript : MonoBehaviour
{
    [SerializeField] private GameObject enemyIcon;
    [SerializeField] private GameObject friendlyIcon;

    private void FixedUpdate()
    {
        SpriteActivator();
    }

    public void SpriteActivator()
    {
        if (GameObject.Find("TurnManager").GetComponent<turnManager>().currentTeam == gameObject.GetComponent<life>().team)
        {
            enemyIcon.GetComponent<SpriteRenderer>().enabled = false;
            friendlyIcon.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            if (gameObject.GetComponent<life>().camoflage)
            {
                friendlyIcon.GetComponent<SpriteRenderer>().enabled = false;
                enemyIcon.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                friendlyIcon.GetComponent<SpriteRenderer>().enabled = false;
                enemyIcon.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
}

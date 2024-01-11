using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class helpScript : MonoBehaviour
{

    public static helpScript _instance;
    public TextMeshProUGUI Text;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y + 3);
    }

    public void showToolTip(string message)
    {
        gameObject.SetActive(true);
        transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y + 3);
        Text.text = message;
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
        Text.text = string.Empty;
    }
}

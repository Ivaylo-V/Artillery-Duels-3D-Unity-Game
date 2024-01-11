using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StartQuitScript : MonoBehaviour
{
    [SerializeField] private GameObject _playerAmount;

    private int playerNumber;

    public void ClickStart()
    {
        int.TryParse(_playerAmount.GetComponent<TMP_InputField>().text, out int result);
        playerNumber = result;
        Debug.Log($"{result}");
        PlayerPrefs.SetInt("Players", playerNumber);
        SceneManager.LoadScene(1);
    }

    public void ClickQuit()
    {
        Application.Quit();
    }
}

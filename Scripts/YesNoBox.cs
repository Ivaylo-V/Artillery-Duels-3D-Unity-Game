using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class YesNoBox : MonoBehaviour
{
    public static YesNoBox Instance { get; private set; }

    private TextMeshProUGUI text;
    private Button yesBt;
    private Button noBt;
    [SerializeField] private GameObject warningBox;

    private void Awake()
    {
        Instance = this;
        text = warningBox.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        yesBt = warningBox.transform.Find("Yees").GetComponent<Button>();
        noBt = warningBox.transform.Find("No").GetComponent<Button>();
    }

    public void ShowQuestion(string question, Action yesAct, Action noAct)
    {
        warningBox.SetActive(true);
        text.text = question;
        yesBt.onClick.AddListener(new UnityEngine.Events.UnityAction(yesAct));
        noBt.onClick.AddListener(new UnityEngine.Events.UnityAction(noAct));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUnlocker : MonoBehaviour
{
    [SerializeField] GameObject[] _links, _nextButton;
    [SerializeField] GameObject _skillTree;
    [SerializeField] Button _previousButton;

    public int _owner;
    private int skillPoints;

    public bool Clicked;

    public void LinkActivator()
    {

        foreach (GameObject link in _links)
        {
            link.SetActive(true);
        }

    }

    private void FixedUpdate()
    {
        if (Clicked)
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
    }

    public void click()
    {
        skillPointUpdater();

        _skillTree.gameObject.GetComponent<SkillTree>().currentButton = gameObject;

        if (skillPoints > 0 && _previousButton.gameObject.GetComponent<SkillUnlocker>().Clicked)
        {
            foreach(GameObject next in _nextButton)
            {
                next.GetComponent<Button>().interactable = true;
                next.gameObject.GetComponent<SkillUnlocker>().LinkActivator();
            }

            Clicked = true;
            gameObject.GetComponent<Image>().color = new Color(0.1f, 0.65f, 0.5f, 1);
            skillPoints--;
        }
        else if(skillPoints <= 0)
        {
            Debug.Log("You don't have enough skillpoints.");
        }
        else if (!_previousButton.gameObject.GetComponent<SkillUnlocker>().Clicked)
        {
            Debug.Log("Please unlock previous skill first.");
        }
    }

    public void FirstButton()
    {
        skillPointUpdater();
        _skillTree.gameObject.GetComponent<SkillTree>().currentButton = gameObject;

        if (skillPoints > 0)
        {
            foreach (GameObject next in _nextButton)
            {
                next.GetComponent<Button>().interactable = true;
                next.gameObject.GetComponent<SkillUnlocker>().LinkActivator();
            }

            Clicked = true;
            gameObject.GetComponent<Image>().color = new Color(0.1f,0.65f,0.5f,1);
            skillPoints--;
        }
        else if (skillPoints <= 0)
        {
            Debug.Log("You don't have enough skillpoints.");
        }
    }

    public void LastClick()
    {
        skillPointUpdater();
        _skillTree.gameObject.GetComponent<SkillTree>().currentButton = gameObject;

        if(skillPoints > 0)
        {
            Clicked = true;
            gameObject.GetComponent<Image>().color = new Color(0.1f, 0.65f, 0.5f, 1);
            skillPoints--;
        }
    }

    private void skillPointUpdater()
    {
        if (_owner == 0)
        {
            skillPoints = _skillTree.gameObject.GetComponent<SkillTree>().CurrentSkillPointsFob;
        }
        else if (_owner == 1)
        {
            skillPoints = _skillTree.gameObject.GetComponent<SkillTree>().CurrentUnit1SkillPoints;
        }
        else if (_owner == 2)
        {
            skillPoints = _skillTree.gameObject.GetComponent<SkillTree>().CurrentUnit2SkillPoints;
        }
        else if (_owner == 3)
        {
            skillPoints = _skillTree.gameObject.GetComponent<SkillTree>().CurrentUnit3SkillPoints;
        }
        else if (_owner == 4)
        {
            skillPoints = _skillTree.gameObject.GetComponent<SkillTree>().CurrentUnit4SkillPoints;
        }
        else if (_owner == 5)
        {
            skillPoints = _skillTree.gameObject.GetComponent<SkillTree>().CurrentUnit5SkillPoints;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private LayerMask InteractableLayer;
    [SerializeField] private LayerMask ObstacleLayer;
    [SerializeField] private int radius;
    [SerializeField] GameObject[] visibleParts;

    public GameObject _fieldOfView;
    public bool isVisable;
    private Coroutine myRoutine;

    private void Start()
    {
        StartCoroutine(FOVRoutine());
    }

    private void Update()
    {
        _fieldOfView.transform.localScale = new Vector3(gameObject.GetComponent<life>().fieldOfVision, gameObject.GetComponent<life>().fieldOfVision, _fieldOfView.transform.localScale.y);

        if(gameObject.GetComponent<life>().team != 0)
        {
            if (GameObject.Find("TurnManager").GetComponent<turnManager>().currentTeam == gameObject.GetComponent<life>().team)
            {
                _fieldOfView.SetActive(true);
                isVisable = true;
            }
            else
            {
                _fieldOfView.SetActive(false);
            }

            if (isVisable)
            {
                foreach(GameObject part in visibleParts)
                {
                    part.GetComponent<MeshRenderer>().enabled = true;
                }
            }
            else
            {
                foreach (GameObject part in visibleParts)
                {
                    part.GetComponent<MeshRenderer>().enabled = false;
                }
            }
        }
        else
        {
            _fieldOfView.SetActive(false);
            foreach (GameObject part in visibleParts)
            {
                part.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            fieldOfViewCheck();
        }
    }

    private IEnumerator DecayRoutine(int timer)
    {
        WaitForSeconds wait = new WaitForSeconds(1);

        while(timer > 0)
        {
            yield return wait;
            timer -= 1;
        }

        isVisable = false;
    }

    private void fieldOfViewCheck()
    {

        Collider[] colCheck = Physics.OverlapSphere(gameObject.transform.position, radius * 5, InteractableLayer);


        if (colCheck.Length > 0)
        {

            foreach(Collider col in colCheck)
            {

                if(col.gameObject.GetComponent<life>() != null)
                {

                    if(col.gameObject.GetComponent<life>().team != gameObject.GetComponent<life>().team)
                    {
                        Transform target = col.gameObject.transform;
                        Vector3 direction = (target.position - gameObject.transform.position).normalized;
                        float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);

                        if (Physics.Raycast(gameObject.transform.position, direction, distance, ObstacleLayer))
                        {
                            col.GetComponent<FieldOfView>().isVisable = false;
                        }
                        else
                        {
                            col.GetComponent<FieldOfView>().isVisable = true;
                            col.GetComponent<FieldOfView>().ResetRoutine();
                        }

                    }

                }

            }
        }

    }

    public void ResetRoutine()
    {
        if(myRoutine != null)
        {
            StopCoroutine(myRoutine);
        }
            myRoutine = StartCoroutine(DecayRoutine(3));
    }

    public void startNewTurnBaby()
    {
        if (GameObject.Find("TurnManager").GetComponent<turnManager>().currentTeam != gameObject.GetComponent<life>().team)
        {
            isVisable = false;
        }
    }
}

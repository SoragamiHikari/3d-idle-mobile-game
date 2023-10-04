using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Cinetouch : MonoBehaviour
{
    private CinemachineFreeLook cineCam;
    [SerializeField] TouchField touchField;
    [SerializeField] float SenstivityX = 2f;
    [SerializeField] float SenstivityY = 2f;

    // Start is called before the first frame update
    void Start()
    {
        cineCam = GetComponent<CinemachineFreeLook>();
    }

    // Update is called once per frame
    void Update()
    {
        cineCam.m_XAxis.Value += touchField.TouchDist.x * 200 * SenstivityX * Time.deltaTime;
        cineCam.m_YAxis.Value += touchField.TouchDist.y * -SenstivityY * Time.deltaTime;
    }
}

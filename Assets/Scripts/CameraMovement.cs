// =======================================
//     Auteur: Thomas Brunet
//     Automne 2022, TIM
// =======================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _cible;      //déclaration du transform de la cible
    private Vector3 _endPos;        //déclaration du vector 3 de position final de la camera
    private Vector3 _startPos;      //déclaration du vector 3 de position initial de la camera
    private float _vitesse = 7f;        //déclaration de la vitesse de la caméra

    void FixedUpdate()      //fonction fixedupdate
    {
        _startPos = transform.position;
        _endPos = _cible.position;
        _endPos.z = -10f;
        transform.position = Vector3.Lerp(_startPos, _endPos, _vitesse * Time.fixedDeltaTime);
    }

}

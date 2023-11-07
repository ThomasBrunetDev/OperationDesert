// =======================================
//     Auteur: Thomas Brunet
//     Automne 2022, TIM
// =======================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vies : MonoBehaviour
{
    [SerializeField] private GameObject[] _coeurs;

    public void Afficher(int nbVies)
    {
        for (int i = 0; i < _coeurs.Length; i++)
        {
            if(i<nbVies)
            {
                _coeurs[i].SetActive(true);
            }
            else
            {
                _coeurs[i].SetActive(false);
            }
        }
    }
}

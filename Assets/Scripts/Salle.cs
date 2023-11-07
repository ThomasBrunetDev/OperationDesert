// =======================================
//     Auteur: Thomas Brunet
//     Automne 2022, TIM
// =======================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Salle : MonoBehaviour
{
    [SerializeField] private string _nomSalle;
    [SerializeField] private Text _endroit;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            _endroit.text = _nomSalle;
        }
        
    }
}

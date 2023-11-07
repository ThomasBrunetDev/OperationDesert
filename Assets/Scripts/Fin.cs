// =======================================
//     Auteur: Thomas Brunet
//     Automne 2022, TIM
// =======================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fin : MonoBehaviour
{
    [SerializeField] private Text _txtFin;      //declaration d'un champ de texte de fin
    void Start()        //fonction start
    {   
        if(GameManager.partieGagne)
        {
            _txtFin.text = "Vous avez réussi à vous échapper de la base ennemie";
        }
        else
        {
            _txtFin.text = "Vous n'avez pas réussi à vous échapper de la base ennemie";
        }
    }

}

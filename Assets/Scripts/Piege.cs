// =======================================
//     Auteur: Thomas Brunet
//     Automne 2022, TIM
// =======================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piege : MonoBehaviour
{
    private Animator _anim;        //déclaration de l'animator

    void Start()        //fontion start
    {
        _anim = GetComponent<Animator>();
    }

    public void DebutPartie()       //fonction qui dbute la parti
    {
        Invoke("SortirPiege", Random.Range(0f,2f));
    }
    private void SortirPiege()      //fonction qui fait sortir les pieges
    {
        _anim.SetTrigger("Declanche");
        Invoke("EntrerPiege", Random.Range(2f, 4f));
    }
    private void EntrerPiege()      //fonction qui fait entrer les pieges
    {
        _anim.SetTrigger("Fin");
        Invoke("SortirPiege", Random.Range(2f,4f));
    }
}

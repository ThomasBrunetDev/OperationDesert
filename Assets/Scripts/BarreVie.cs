// =======================================
//     Auteur: Thomas Brunet
//     Automne 2022, TIM
// =======================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarreVie : MonoBehaviour
{
    private const float _VIE_MAX = 30;      //déclaration de la constante de la vie max
    private float _vieEnCours;      // déclaration d'un float pour la vie en cour
    [SerializeField] private Boss _boss;        // déclaration du lien avec le script Boss 
    [SerializeField] private GameObject _barre;     //déclaration du gameObject de la barre de vie

    void Start()        //fonction start
    {
        _vieEnCours = _VIE_MAX;
    }

    public void Modifier(float valeur)      //fonction qui modifie l'affichage des vies
    {
        _vieEnCours=Mathf.Clamp(_vieEnCours+valeur, 0f, _VIE_MAX);
        float ratio = _vieEnCours/_VIE_MAX;
        _barre.transform.localScale = new Vector3(ratio,1,1);
        if(_vieEnCours == 0)
        {
            _boss.Explosion();
            Invoke("FinPartie", 3f);
        }
    }

    private void FinPartie()        //fonction qui qui a gamemanager que la partie est fini
    {
        GameManager.instance.FinPartie(false);
    }
}

// =======================================
//     Auteur: Thomas Brunet
//     Automne 2022, TIM
// =======================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private AudioClip _sonBoom;    // Déclaration du son de l'explosion

    public void Detruire()      // Fonction qui détruit l'objet  
    {
        Destroy(gameObject);
    }

    public void JouerSon()      // Fonction qui fait jouer les sons 
    {
        AudioSource.PlayClipAtPoint(_sonBoom, transform.position, 30f);
    }

}

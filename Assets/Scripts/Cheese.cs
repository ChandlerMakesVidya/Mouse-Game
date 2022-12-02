using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : MonoBehaviour
{
    public AudioClip getSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.cheeseCount -= 1;
            AudioSource.PlayClipAtPoint(getSound, transform.position);
            Destroy(this.gameObject);
        }
    }
}

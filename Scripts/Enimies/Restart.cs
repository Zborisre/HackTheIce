using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enitity>())
            if (collision.gameObject.GetComponent<Enitity>() == Hero.Instance.gameObject)
            {
                SceneManager.LoadScene(2);
            }
            else
            {
                collision.gameObject.GetComponent<Enitity>().Die();
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

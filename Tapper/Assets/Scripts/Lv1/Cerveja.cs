using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cerveja : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameController spawnClientes = GameObject.Find("Controller").GetComponent<GameController>();

        if (spawnClientes != null)
        {
            Dictionary<string, int> tagVivos = new Dictionary<string, int>
            {
                { "Cliente1", spawnClientes.vivos[0] },
                { "Cliente2", spawnClientes.vivos[1] },
                { "Cliente3", spawnClientes.vivos[2] },
                { "Cliente4", spawnClientes.vivos[3] }
            };

            if (tagVivos.ContainsKey(collision.tag) && tagVivos[collision.tag] > 0)
            {
                Destroy(collision.gameObject);
                tagVivos[collision.tag]--;
                GameController.totalClientesDestruidos++;
                Destroy(gameObject);
            }
        }
    }
}


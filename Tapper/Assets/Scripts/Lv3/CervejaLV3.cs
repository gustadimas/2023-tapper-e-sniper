using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CervejaLV3 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameControllerLV3 spawnClientesLV3 = GameObject.Find("Controller").GetComponent<GameControllerLV3>();

        if (spawnClientesLV3 != null)
        {
            Dictionary<string, int> tagVivosLV3 = new Dictionary<string, int>
            {
                { "Cliente1", spawnClientesLV3.vivosLV3[0] },
                { "Cliente2", spawnClientesLV3.vivosLV3[1] },
                { "Cliente3", spawnClientesLV3.vivosLV3[2] },
                { "Cliente4", spawnClientesLV3.vivosLV3[3] }
            };

            if (tagVivosLV3.ContainsKey(collision.tag) && tagVivosLV3[collision.tag] > 0)
            {
                Destroy(collision.gameObject);
                tagVivosLV3[collision.tag]--;
                GameControllerLV3.totalClientesDestruidosLV3++;
                Destroy(gameObject);
            }
        }
    }
}

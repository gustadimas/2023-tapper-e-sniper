using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CervejaLV2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameControllerLV2 spawnClientesLV2 = GameObject.Find("Controller").GetComponent<GameControllerLV2>();

        if (spawnClientesLV2 != null)
        {
            Dictionary<string, int> tagVivosLV2 = new Dictionary<string, int>
            {
                { "Cliente1", spawnClientesLV2.vivosLV2[0] },
                { "Cliente2", spawnClientesLV2.vivosLV2[1] },
                { "Cliente3", spawnClientesLV2.vivosLV2[2] },
                { "Cliente4", spawnClientesLV2.vivosLV2[3] }
            };

            if (tagVivosLV2.ContainsKey(collision.tag) && tagVivosLV2[collision.tag] > 0)
            {
                Destroy(collision.gameObject);
                tagVivosLV2[collision.tag]--;
                GameControllerLV2.totalClientesDestruidosLV2++;
                Destroy(gameObject);
            }
        }
    }
}

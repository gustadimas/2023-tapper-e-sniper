using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameControllerLV2 : MonoBehaviour
{
    [Header("Configurações de SpawnLV2")] 
    [SerializeField] GameObject[] clientesLV2;
    [SerializeField] Transform[] pontosSpawnLV2;
    [SerializeField] string[] tagsClientesLV2;
    [HideInInspector] public float ultimoResetLV2 = 0, intervaloResetLV2;

    int[] invocouLV2;
    int totalClientesInvocadosLV2 = 0;
    [HideInInspector] public int[] vivosLV2 = new int[4];

    public static int totalClientesDestruidosLV2 = 0;

    private void Start()
    {
        int _numPontosSpawnLV2 = pontosSpawnLV2.Length;
        invocouLV2 = new int[_numPontosSpawnLV2];

        for (int i = 0; i < _numPontosSpawnLV2; i++)
        {
            invocouLV2[i] = 0;
            vivosLV2[i] = 1;
        }

        StartCoroutine(SpawnInimigosComDelayLV2());
    }

    private void Update()
    {
        if (totalClientesDestruidosLV2 == 8)
        {
            Debug.Log("Transição para Fase 3");
            SceneManager.LoadScene(2);
        }

        if (Time.timeSinceLevelLoad - ultimoResetLV2 >= intervaloResetLV2)
        {
            ResetarFileirasLV2();
        }
    }

    private IEnumerator SpawnInimigosComDelayLV2()
    {
        while (totalClientesInvocadosLV2 < 8)
        {
            for (int _fileiraLV2 = 0; _fileiraLV2 < pontosSpawnLV2.Length; _fileiraLV2++)
            {
                for (int i = 0; i < 2; i++)
                {
                    int _probabilidadeLV2 = _fileiraLV2;

                    if (invocouLV2[_probabilidadeLV2] < 2 && vivosLV2[_probabilidadeLV2] != 0)
                    {
                        GameObject _clienteLV2 = Instantiate(clientesLV2[Random.Range(0, clientesLV2.Length)], pontosSpawnLV2[_probabilidadeLV2].position, Quaternion.identity);
                        Rigidbody2D _rbLV2 = _clienteLV2.GetComponent<Rigidbody2D>();
                        _rbLV2.velocity = Vector3.right * 1.5f;

                        if (_probabilidadeLV2 >= 0 && _probabilidadeLV2 < tagsClientesLV2.Length)
                        {
                            _clienteLV2.tag = tagsClientesLV2[_probabilidadeLV2];
                        }

                        invocouLV2[_probabilidadeLV2]++;
                        totalClientesInvocadosLV2++;
                    }
                    yield return new WaitForSeconds(2f);
                }
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    public void ReiniciarRecargasLV2()
    {
        int _numPontosSpawnLV2 = pontosSpawnLV2.Length;
        for (int i = 0; i < _numPontosSpawnLV2; i++)
        {
            vivosLV2[i] = 1;
        }
    }

    private void ResetarFileirasLV2()
    {
        for (int i = 0; i < invocouLV2.Length; i++)
        {
            invocouLV2[i] = 0;
        }

        ultimoResetLV2 = Time.timeSinceLevelLoad;
    }
}

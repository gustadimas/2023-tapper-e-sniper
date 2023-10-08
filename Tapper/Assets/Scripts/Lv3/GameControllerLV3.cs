using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerLV3 : MonoBehaviour
{
    [Header("Configurações de SpawnLV3")]
    [SerializeField] GameObject[] clientesLV3;
    [SerializeField] Transform[] pontosSpawnLV3;
    [SerializeField] string[] tagsClientesLV3;
    [SerializeField] GameObject balaInimigoPrefab;
    [SerializeField] int velocidadeBalaInimigo;
    [SerializeField] float taxaTiroInimigo = 2.0f;

    [HideInInspector] public float ultimoResetLV3 = 0, intervaloResetLV3;

    int[] invocouLV3;
    int totalClientesInvocadosLV3 = 0;
    [HideInInspector] public int[] vivosLV3 = new int[4];

    public static int totalClientesDestruidosLV3 = 0;

    private GameObject[] ultimosInimigos;



    private void Start()
    {
        int _numPontosSpawnLV3 = pontosSpawnLV3.Length;
        invocouLV3 = new int[_numPontosSpawnLV3];
        ultimosInimigos = new GameObject[_numPontosSpawnLV3];

        for (int i = 0; i < _numPontosSpawnLV3; i++)
        {
            invocouLV3[i] = 0;
            vivosLV3[i] = 1;
        }

        StartCoroutine(SpawnInimigosComDelayLV3());
    }

    private void Update()
    {
        if (totalClientesDestruidosLV3 == 12)
        {
            Debug.Log("Transição para Vitória");
            SceneManager.LoadScene(3);
        }

        if (Time.timeSinceLevelLoad - ultimoResetLV3 >= intervaloResetLV3)
        {
            ResetarFileirasLV3();
        }
    }

    private IEnumerator SpawnInimigosComDelayLV3()
    {
        while (totalClientesInvocadosLV3 < 12)
        {
            for (int _fileiraLV3 = 0; _fileiraLV3 < pontosSpawnLV3.Length; _fileiraLV3++)
            {
                for (int i = 0; i < 3; i++)
                {
                    int _probabilidadeLV3 = _fileiraLV3;

                    if (invocouLV3[_probabilidadeLV3] < 3 && vivosLV3[_probabilidadeLV3] != 0)
                    {
                        GameObject _clienteLV3 = Instantiate(clientesLV3[Random.Range(0, clientesLV3.Length)], pontosSpawnLV3[_probabilidadeLV3].position, Quaternion.identity);
                        Rigidbody2D _rbLV3 = _clienteLV3.GetComponent<Rigidbody2D>();
                        _rbLV3.velocity = Vector3.right * 1.5f;

                        if (_probabilidadeLV3 >= 0 && _probabilidadeLV3 < tagsClientesLV3.Length)
                        {
                            _clienteLV3.tag = tagsClientesLV3[_probabilidadeLV3];
                        }

                        if (i == 2)
                        {
                            ultimosInimigos[_probabilidadeLV3] = _clienteLV3;

                            PermitirAtirar(_clienteLV3);
                        }

                        invocouLV3[_probabilidadeLV3]++;
                        totalClientesInvocadosLV3++;
                    }
                    yield return new WaitForSeconds(2f);
                }
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    public void ReiniciarRecargasLV3()
    {
        int _numPontosSpawnLV3 = pontosSpawnLV3.Length;
        for (int i = 0; i < _numPontosSpawnLV3; i++)
        {
            vivosLV3[i] = 1;
        }
    }

    private void ResetarFileirasLV3()
    {
        for (int i = 0; i < invocouLV3.Length; i++)
        {
            invocouLV3[i] = 0;
        }

        ultimoResetLV3 = Time.timeSinceLevelLoad;
    }

    private void PermitirAtirar(GameObject inimigo)
    {
        StartCoroutine(Atirar(inimigo));
    }

    private IEnumerator Atirar(GameObject _inimigo)
    {
        while (true)
        {
            if (_inimigo != null)
            {
                Vector2 _direcao = Vector2.right;

                GameObject _balaInimigo = Instantiate(balaInimigoPrefab, _inimigo.transform.position, Quaternion.identity);
                Rigidbody2D _bala = _balaInimigo.GetComponent<Rigidbody2D>();
                _bala.velocity = _direcao * velocidadeBalaInimigo;

                Destroy(_balaInimigo, 1.5f);
            }

            yield return new WaitForSeconds(taxaTiroInimigo);
        }
    }
}
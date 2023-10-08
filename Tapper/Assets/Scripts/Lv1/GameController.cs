using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour
{
    [Header("Configurações de Spawn")]
    [SerializeField] GameObject[] clientes;
    [SerializeField] Transform[] pontosSpawn;
    [SerializeField] string[] tagsClientes;
    [HideInInspector] public float ultimoReset = 0, intervaloReset;

    int[] invocou;
    int totalClientesInvocados = 0;
    [HideInInspector] public int[] vivos = new int[4];

    public static int totalClientesDestruidos = 0;

    private void Start()
    {
        int _numPontosSpawn = pontosSpawn.Length;
        invocou = new int[_numPontosSpawn];

        for (int i = 0; i < _numPontosSpawn; i++)
        {
            invocou[i] = 0;
            vivos[i] = 1;
        }

        StartCoroutine(SpawnInimigosComDelay());
    }

    private void Update()
    {
        if (totalClientesDestruidos == 4)
        {
            Debug.Log("Transição para Fase 2");
            SceneManager.LoadScene(1);
        }

        if (Time.timeSinceLevelLoad - ultimoReset >= intervaloReset)
        {
            int _numPontosSpawn = pontosSpawn.Length;
            for (int i = 0; i < _numPontosSpawn; i++)
            {
                invocou[i] = 0;
            }
            ultimoReset = Time.timeSinceLevelLoad;
        }
    }

    private IEnumerator SpawnInimigosComDelay()
    {
        while (totalClientesInvocados < 4)
        {
            for (int _fileira = 0; _fileira < pontosSpawn.Length; _fileira++)
            {
                for (int i = 0; i < 1; i++)
                {
                    int _probabilidade = _fileira;

                    if (invocou[_probabilidade] < 1 && vivos[_probabilidade] != 0)
                    {
                        GameObject _cliente = Instantiate(clientes[Random.Range(0, clientes.Length)], pontosSpawn[_probabilidade].position, Quaternion.identity);
                        Rigidbody2D _rb = _cliente.GetComponent<Rigidbody2D>();
                        _rb.velocity = Vector3.right * 1.5f;

                        if (_probabilidade >= 0 && _probabilidade < tagsClientes.Length)
                        {
                            _cliente.tag = tagsClientes[_probabilidade];
                        }

                        invocou[_probabilidade]++;
                        totalClientesInvocados++;
                    }

                    yield return new WaitForSeconds(2f);
                }
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    public void ReiniciarRecargas()
    {
        int _numPontosSpawn = pontosSpawn.Length;
        for (int i = 0; i < _numPontosSpawn; i++)
        {
            vivos[i] = 1;
        }
    }
}

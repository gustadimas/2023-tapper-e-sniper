using UnityEngine;
using UnityEngine.UI;

public class Barman : MonoBehaviour
{
    [SerializeField] Transform[] pontosMove;
    [SerializeField] GameObject bebidaPrefab;
    [SerializeField] Image[] recargas;
    [SerializeField] float tempoBebida;

    private float[] tempoUltimoToquePorBalcao;

    private void Start()
    {
        tempoBebida = 1.5f;
        transform.position = pontosMove[0].position;

        tempoUltimoToquePorBalcao = new float[pontosMove.Length];

        for (int i = 0; i < recargas.Length; i++)
        {
            recargas[i].enabled = true;
            tempoUltimoToquePorBalcao[i] = 0f;
        }
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch _toque = Input.GetTouch(0);
            Vector3 _posToque = Camera.main.ScreenToWorldPoint(_toque.position);

            int index = ObterIndiceBalcaoAtual();

            switch (_toque.phase)
            {
                case TouchPhase.Began:
                    if (Time.timeSinceLevelLoad - tempoUltimoToquePorBalcao[index] < 0.3f)
                    {
                        if (index >= 0)
                        {
                            if (recargas[index].enabled)
                            {
                                GameObject _bebida = Instantiate(bebidaPrefab, pontosMove[index].position, Quaternion.identity);
                                Rigidbody2D _rb = _bebida.GetComponent<Rigidbody2D>();
                                _rb.velocity = Vector3.left * 5;
                                recargas[index].enabled = false;
                                Destroy(_bebida, 5f);
                            }
                        }
                    }

                    tempoUltimoToquePorBalcao[index] = Time.timeSinceLevelLoad;

                    Collider2D _colisao = Physics2D.OverlapPoint(_posToque);

                    if (_colisao != null)
                    {
                        int _novoIndice = ObterIndiceBalcao(_colisao.gameObject.name);
                        if (_novoIndice >= 0)
                        {
                            transform.position = pontosMove[_novoIndice].position;
                        }
                    }
                    break;
            }
        }

        Recarregar();
    }

    private void Recarregar()
    {
        for (int i = 0; i < recargas.Length; i++)
        {
            if (!recargas[i].enabled && Time.timeSinceLevelLoad - tempoUltimoToquePorBalcao[i] >= tempoBebida)
            {
                recargas[i].enabled = true;
            }
        }
    }

    private int ObterIndiceBalcaoAtual()
    {
        for (int i = 0; i < pontosMove.Length; i++)
        {
            if (transform.position == pontosMove[i].position)
            {
                return i;
            }
        }
        return -1;
    }

    private int ObterIndiceBalcao(string _nomeBalcao)
    {
        for (int i = 0; i < pontosMove.Length; i++)
        {
            if (_nomeBalcao == pontosMove[i].name)
            {
                return i;
            }
        }
        return -1;
    }
}

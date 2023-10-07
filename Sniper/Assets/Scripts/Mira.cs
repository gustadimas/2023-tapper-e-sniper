using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mira : MonoBehaviour
{
    [SerializeField] float duracaoTremor = 0.2f;
    [SerializeField] float magnitudeTremor = 0.1f;
    [SerializeField] GameObject objetoBranco, objetoAzul, objetoAmarelo;

    Camera cameraPrincipal;
    Transform transformMira;
    bool estaTremendo, duploClique = false;
    bool todosDestruidos = false;

    Dictionary<GameObject, int> contadoresTiros = new Dictionary<GameObject, int>();

    void Start()
    {
        cameraPrincipal = Camera.main;
        transformMira = transform;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch _toque = Input.GetTouch(0);
            Vector3 _posicaoToque = cameraPrincipal.ScreenToWorldPoint(_toque.position);
            transformMira.position = new Vector3(_posicaoToque.x, _posicaoToque.y, 0f);

            if (_toque.tapCount == 2 && !duploClique && !estaTremendo)
            {
                GameObject _objetoAlvo = DetectarObjetoAlvo();

                if (_objetoAlvo != null)
                {
                    duploClique = true;
                    StartCoroutine(TremerMira());
                    Atirar(_objetoAlvo);
                }
            }
            else if (_toque.tapCount == 1)
                duploClique = false;
        }

        if (objetoBranco == null && objetoAzul == null && objetoAmarelo == null)
        {
            if (!todosDestruidos)
            {
                todosDestruidos = true;
                Invoke(nameof(ReiniciarCena), 2f);
            }
        }
    }

    GameObject DetectarObjetoAlvo()
    {
        RaycastHit2D _hit = Physics2D.Raycast(transformMira.position, Vector2.zero);

        if (_hit.collider != null)
        {
            GameObject _objetoHit = _hit.collider.gameObject;
            if (_objetoHit == objetoBranco || _objetoHit == objetoAzul || _objetoHit == objetoAmarelo)
                return _objetoHit;
        }
        return null;
    }

    void Atirar(GameObject _objetoAlvo)
    {
        if (!contadoresTiros.ContainsKey(_objetoAlvo))
            contadoresTiros[_objetoAlvo] = 0;

        contadoresTiros[_objetoAlvo]++;

        if (contadoresTiros[_objetoAlvo] >= GetTirosNecessarios(_objetoAlvo))
        {
            Destroy(_objetoAlvo);
            contadoresTiros.Remove(_objetoAlvo);
        }
    }

    int GetTirosNecessarios(GameObject _objetoAlvo)
    {
        if (_objetoAlvo == objetoBranco) return 1;
        if (_objetoAlvo == objetoAzul) return 3;
        if (_objetoAlvo == objetoAmarelo) return 5;
        return 0;
    }

    void ReiniciarCena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator TremerMira()
    {
        estaTremendo = true;
        Vector3 _posicaoOriginal = transformMira.localPosition;
        float _tempoPassado = 0f;

        while (_tempoPassado < duracaoTremor)
        {
            Vector3 _deslocamentoTremor = Random.insideUnitSphere * magnitudeTremor;
            transformMira.localPosition = _posicaoOriginal + _deslocamentoTremor;

            _tempoPassado += Time.deltaTime;
            yield return null;
        }

        transformMira.localPosition = _posicaoOriginal;
        estaTremendo = false;
    }
}

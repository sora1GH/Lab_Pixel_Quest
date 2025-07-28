using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public Transform _respawnPoint;

    public int _playerLife = 3;
    public int _playerCoin = 0;

    private const string deathTag = "Death";
    private const string healthTag = "Health";
    private const string coinTag = "Coin";
    private const string respawnTag = "Respawn";
    private const string respawnColPoint = "Point";
    private const string finishTag = "Finish";

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        string colTag = collision.tag;

        switch (colTag)
        {
            case deathTag:
                {
                    _rigidbody2D.velocity = Vector2.zero;
                    transform.position = _respawnPoint.position;
                    _playerLife--;
                    if( _playerLife <= 0 )
                    {
                        string SceneName = SceneManager.GetActiveScene().name;
                        SceneManager.LoadScene( SceneName );
                    }
                    return;
                }
            case healthTag:
                {
                    _playerLife++;
                    Destroy(collision.gameObject);
                    return;
                }
            case coinTag:
                {
                    _playerCoin++;
                    Destroy(collision.gameObject);
                    return;
                }
            case respawnTag:
                {
                    _respawnPoint = collision.gameObject.transform.FindChild(respawnColPoint).transform;
                    return;
                }
            case finishTag:
                {
                    string nextLevel = collision.GetComponent<EndLevel>().nextLevel;
                    SceneManager.LoadScene(nextLevel);
                    return;
                }
        }
    }
}
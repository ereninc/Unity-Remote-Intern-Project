using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionManager : MonoBehaviour
{
    public static CollisionManager instance;
    public bool isGrounded = true;
    public bool takeWood = false;
    [SerializeField] private GameObject woodStackedPosition;
    [SerializeField] private Transform placedWoodPosition;
    [SerializeField] private GameObject _player;
    private int _stackedWood = 0;
    private float _yOffset = 0;
    private readonly List<GameObject> _woodList = new List<GameObject>();
    private float _deltaTime = 0.0f;
    [SerializeField] private GameObject finishedGround;
    [SerializeField] private GameObject _cameraController;
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _cameraPos;
    public bool isFinished = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wood"))
        {
            takeWood = true;
            TakeWood(other);
        }

        if (other.CompareTag("10X"))
        {
            isFinished = true;
            _cameraController.SetActive(false);
            PlayerController.instance.playerSpeed = 0f;
            _player.transform.rotation = new Quaternion(0, 160, 0, 0);
            LeanTween.move(_player, new Vector3(finishedGround.transform.position.x, -1, finishedGround.transform.position.z), 0f);
            _camera.transform.SetParent(_cameraPos.transform);
            LeanTween.move(_camera, _cameraPos.transform.position, 0.0f);
            _cameraController.SetActive(true);
            //StartCoroutine(NextScene());
        }
    }

    private IEnumerator NextScene()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(0);
    }

    private void TakeWood(Collider wood)
    {
        _yOffset += 1.25f;
        _stackedWood++;
        wood.transform.position = woodStackedPosition.transform.position;
        wood.transform.parent = woodStackedPosition.transform;
        wood.transform.rotation = woodStackedPosition.transform.rotation;
        LeanTween.scale(wood.gameObject, new Vector3(0.6375f, 0.9563f, 0.6375f), 0.0f);
        LeanTween.moveLocalY(wood.gameObject, _yOffset, 0.0f);
        _woodList.Add(wood.transform.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground") || other.CompareTag("Wood"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
            
        }

        if (other.CompareTag("Wood"))
        {
            takeWood = false;
        }
    }
    private void Update()
    {
        MoveStackedWoods();
        _deltaTime += Time.deltaTime;
        if (!isGrounded && _deltaTime > 0.15f)
        {
            PlaceWoods();
            _deltaTime = 0.0f;
        }
    }

    private void PlaceWoods()
    {
        if (_woodList.Count > 0 && !isFinished)
        {
            GameObject placedWood = _woodList[_stackedWood-1];
            _woodList.RemoveAt(_stackedWood-1);
            placedWood.tag = "empty";
            LeanTween.scale(placedWood, new Vector3(1, 0.06f, 0.7f), 0.0f);
            LeanTween.move(placedWood.transform.gameObject, placedWoodPosition.transform, 0.025f);
            _yOffset -= 1.25f;
            placedWood.GetComponent<BoxCollider>().isTrigger = false;
            placedWood.transform.rotation = placedWoodPosition.transform.rotation;
            placedWood.transform.SetParent(null);
            _stackedWood--;
        }
        else
        {
            _player.GetComponent<Rigidbody>().isKinematic = false;
            //_player.GetComponent<Rigidbody>().AddForce(Vector3.up * 50f);
        }
    }

    private void MoveStackedWoods()
    {
        if (isFinished)
        {
            LeanTween.moveLocal(woodStackedPosition, new Vector3(20.0f, 20.0f, -1.0f), 0.5f);
        }
    }
}
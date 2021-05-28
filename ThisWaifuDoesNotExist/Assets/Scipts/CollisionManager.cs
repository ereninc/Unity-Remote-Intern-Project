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
    public bool playerNotFirst = false;
    [SerializeField] private Animator animator;
    [SerializeField] private Camera mainCamera;
    public GameObject lastTouchedBoost;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        mainCamera.fieldOfView = 60.0f;
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

        if (_player.transform.position.y <= -2)
        {
            isFinished = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wood"))
        {
            takeWood = true;
            TakeWood(other);
        }

        if (other.CompareTag("2X"))
        {
            lastTouchedBoost = other.gameObject;
            DataManager.instance.currentLevelScore = 200;
            FinishedOnBoost();
        }
        
        if (other.CompareTag("3X"))
        {
            lastTouchedBoost = other.gameObject;
            DataManager.instance.currentLevelScore = 300;
            FinishedOnBoost();
        }
        
        if (other.CompareTag("5X"))
        {
            lastTouchedBoost = other.gameObject;
            DataManager.instance.currentLevelScore = 500;
            FinishedOnBoost();
        }
        
        if (other.CompareTag("6X"))
        {
            lastTouchedBoost = other.gameObject;
            DataManager.instance.currentLevelScore = 600;
            FinishedOnBoost();
        }
        
        if (other.CompareTag("8X"))
        {
            lastTouchedBoost = other.gameObject;
            DataManager.instance.currentLevelScore = 800;
            FinishedOnBoost();
        }

        if (other.CompareTag("10X"))
        {
            isFinished = true;
            DataManager.instance.currentLevelScore = 1000;
            _cameraController.SetActive(false);
            PlayerController.instance.playerSpeed = 0f;
            _player.transform.rotation = new Quaternion(0, 160, 0, 0);
            LeanTween.move(_player, new Vector3(finishedGround.transform.position.x, -1, finishedGround.transform.position.z), 0f);
            _camera.transform.SetParent(_cameraPos.transform);
            LeanTween.move(_camera, _cameraPos.transform.position, 0.0f);
            _cameraController.SetActive(true);
        }

        if (other.CompareTag("FinishLine"))
        {
            if (playerNotFirst == true)
            {
                isFinished = true;
                MoveStackedWoods();
                PlayerController.instance.playerSpeed = 0.0f;
                _cameraController.SetActive(false);
                _player.transform.rotation = new Quaternion(0, 160, 0, 0);
            }
        }
    }

    public void FinishedOnBoost()
    {
        if (_woodList.Count == 0)
        {
            isFinished = true;
            _cameraController.SetActive(false);
            PlayerController.instance.playerSpeed = 0f;
            _player.transform.rotation = new Quaternion(0, 160, 0, 0);
            LeanTween.move(_player, new Vector3(lastTouchedBoost.transform.position.x, -1.0f, lastTouchedBoost.transform.position.z), 0f);
            _camera.transform.SetParent(_cameraPos.transform);
            //LeanTween.move(_camera, _cameraPos.transform.position, 0.0f);
            _cameraController.SetActive(true);
        }
    }

    private void TakeWood(Collider wood)
    {
        _yOffset += 1.25f;
        _stackedWood++;
        wood.tag = "empty";
        wood.transform.position = woodStackedPosition.transform.position;
        wood.transform.parent = woodStackedPosition.transform;
        wood.transform.rotation = woodStackedPosition.transform.rotation;
        LeanTween.scale(wood.gameObject, new Vector3(0.6375f, 0.9563f, 0.6375f), 0.0f);
        LeanTween.moveLocalY(wood.gameObject, _yOffset, 0.0f);
        _woodList.Add(wood.transform.gameObject);
        DataManager.instance.EarnGold();
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
    
    // ReSharper disable Unity.PerformanceAnalysis
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
            DataManager.instance.LoseGold();
        }
        else
        {
            _player.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private void MoveStackedWoods()
    {
        if (isFinished)
        {
            LeanTween.moveLocal(woodStackedPosition, new Vector3(20.0f, 20.0f, -1.0f), 0.5f);
            mainCamera.fieldOfView = 30.0f;
        }
    }

    public int WoodCount()
    {
        return _woodList.Count;
    }
}
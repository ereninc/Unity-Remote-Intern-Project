using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class AI : MonoBehaviour
{
    public static AI instance;
    [SerializeField] private Animator animator;
    public bool isGrounded = true;
    public bool takeWood = false;
    [SerializeField] private GameObject woodStackedPosition;
    [SerializeField] private Transform placedWoodPosition;
    [SerializeField] private GameObject _player;
    private int _stackedWood = 0;
    private float _yOffset = 0;
    private readonly List<GameObject> _woodList = new List<GameObject>();
    private float _deltaTime = 0.0f;
    public bool isFinished = false;
    [SerializeField] private GameObject realPlayer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        StopAnimations();
    }

    public void StopAnimations()
    {
        animator.enabled = false;
    }

    private void StartAnimations()
    {
        if (PlayerController.instance.isStarted)
        {
            animator.enabled = true;
        }
    }

    void CheckWoodList()
    {
        if (_woodList.Count <= 0 && !isGrounded)
        {
            animator.transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            animator.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wood"))
        {
            takeWood = true;
            TakeWood(other);
        }

        if (other.CompareTag("FinishLine"))
        {
            CollisionManager.instance.playerNotFirst = true;
        }
    }

    private void TakeWood(Collider wood)
    {
        _yOffset += 1.25f;
        wood.tag = "empty";
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
        StartAnimations();
        CheckWoodList();
        _deltaTime += Time.deltaTime;
        if (!isGrounded && _deltaTime > 0.15f)
        {
            PlaceWoods();
            _deltaTime = 0.0f;
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
using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Global : MonoBehaviour {

    public static Global instance;

    private Canvas _canvas;
    private SearchBar _userSearch;
    private ManagerData _managerData;
    private ManagerUsers _managerUsers;
    private ManagerTouch _managerTouch;

    public Canvas canvas { get { return _canvas; } }
    public ManagerData managerData { get { return _managerData; } }
    public ManagerUsers managerUsers { get { return _managerUsers; } }
    public ManagerTouch managerTouch { get { return _managerTouch; } }

    // Use this for initialization
    private void Awake()
    {
        instance = this;
    }


    private void Start () {

        _canvas = GameObject.FindObjectOfType<Canvas>();

        _managerData = new ManagerData();
        _managerUsers = new ManagerUsers();
        _managerTouch = new ManagerTouch();
        _userSearch = new SearchBar();
    }
	

	private void Update () {

        _managerTouch.update();
	}



    public Tweener onDelayedCall(float duration, TweenCallback onComplete)
    {
        float start = 1;
        float stop = 0;
        float startVal = 1;

        return DOTween.To(() => start, x => startVal = x, stop, duration).OnComplete(onComplete);
    }
}

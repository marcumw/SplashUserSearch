using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Global : MonoBehaviour {

    private UserSearch _userSearch;
    private ManagerData _managerData;
    private ManagerUsers _managerUsers;
    private Canvas _canvas;

    public static Global instance;

    public Canvas canvas { get { return _canvas; } }
    public ManagerData managerData { get { return _managerData; } }
    public ManagerUsers managerUsers { get { return _managerUsers; } }

    // Use this for initialization
    private void Awake()
    {

        instance = this;
    }


    private void Start () {

        _canvas = GameObject.FindObjectOfType<Canvas>();

        _userSearch = new UserSearch();
        _managerData = new ManagerData();
        _managerUsers = new ManagerUsers();
	}
	
	// Update is called once per frame
	private void Update () {
	
	}



    public Tweener onDelayedCall(float duration, TweenCallback onComplete)
    {
        float start = 1;
        float stop = 0;
        float startVal = 1;

        return DOTween.To(() => start, x => startVal = x, stop, duration).OnComplete(onComplete);
    }
}

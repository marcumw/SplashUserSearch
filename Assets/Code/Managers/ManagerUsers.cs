using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using DG.Tweening;

public class ManagerUsers {

    #region variables
    private List<User> _currUsersList;
    private float _currUserListHeight;

    private List<GuiUser> _guiUsersPool;
    private GameObject _go;
    private RectTransform _rt;

    private bool _tweening = false;

    private int _countUsersToReturn = 10;

    private float _targetInitY;

    private Vector2 _targetAnchorPos;

    private List<Texture2D> _avatars;

    private UserInfo _userInfo;
    #endregion

    public ManagerUsers()
    {
        _go = new GameObject();
        _go.name = "Users";

        _go.transform.SetParent(Global.instance.Canvas.transform);

        _targetInitY = Screen.height * .34f;
        _targetAnchorPos = new Vector2(0, _targetInitY);

        _rt = _go.AddComponent<RectTransform>();
        _rt.anchoredPosition = _targetAnchorPos;

        _currUsersList = new List<User>();

        _userInfo = new UserInfo();

        loadAvatars();
        createGuiUserPool();
        checkIfUserDataFetchComplete();
    }

    public void toggle(bool value)
    {
        _go.SetActive(value);
    }

    private void loadAvatars()
    {
        _avatars = new List<Texture2D>();
        for (int i = 1; i <= 9; i++)
        {
            Texture2D texture = (Texture2D)Resources.Load("avatars/avatar" + i);
            _avatars.Add(texture);
        }
    }

    private void checkIfUserDataFetchComplete()
    {
        if (Global.instance.ManagerData.Complete)
        {

        }
        else
        {
            Global.instance.onDelayedCall(.25f, checkIfUserDataFetchComplete);
        }
    }

    private void createGuiUserPool()
    {
        float currTargetY = 0;
        float spacerY = 0;
        float spacerX = 0;

        _guiUsersPool = new List<GuiUser>();

        User dummyUser = new User(0, "user", 0);
  
        for (int i = 0; i < _countUsersToReturn; i++)
        {
            GuiUser guiUser = new GuiUser(dummyUser, getAvatarAsync(), _go.transform);
            spacerY = guiUser.SizeDelta.y * .25f;
            spacerX = guiUser.SizeDelta.y * .75f;

            guiUser.rt.anchoredPosition = new Vector2(spacerX, currTargetY);

            currTargetY -= (guiUser.SizeDelta.y + spacerY);

            _guiUsersPool.Add(guiUser);
        }
    }

    //Not a real async call...just a future suggestion
    //the avatar probably should be separate async call to user.avatarUrl ???
    //there is no reason to store binary data with every User object
    private Texture2D getAvatarAsync()
    {
        return _avatars[UnityEngine.Random.Range(0, _avatars.Count)];
    }

    public void onUserClicked(GuiUser guiUser)
    {
        _userInfo.update(guiUser);

        toggleUserInfo(true);
    }

    public void onUserInfoBackButtonClicked()
    {
        toggleUserInfo(false);
    }

    private void toggleUserInfo(bool value)
    {
        if (value)
        {
            Global.instance.SearchBar.toggle(false);
            Global.instance.ManagerUsers.toggle(false);
            _userInfo.toggle(true);
  
        }
        else
        {
            Global.instance.SearchBar.toggle(true);
            Global.instance.ManagerUsers.toggle(true);
            _userInfo.toggle(false);
        }
    }

    public void getUsersByAlpha(string currAlpha)
    {
        _currUsersList.Clear();
        GuiUsersToggleAll(false);

        if (currAlpha == "")
            return;

        _currUsersList = Global.instance.ManagerData.getUsersByAlpha(currAlpha);

        _currUserListHeight = 0;

        if (_currUsersList.Count > 0)
        {
            for (int i = 0; i < _guiUsersPool.Count; i++)
            {
                GuiUser.Update(_guiUsersPool[i], getAvatarAsync(), _currUsersList[i]);
                GuiUser.ToggleGo(true, _guiUsersPool[i]);

                if (i == _currUsersList.Count - 1)
                    break;

                _currUserListHeight += Screen.height * .085f;
            }
        }
    }

    public void scroll(float ySmoothDelta)
    {
        if (_tweening)
            return;

        if (_currUserListHeight < Screen.height * .75f)
            return;

        //psuedo clamping logic - no time for the real thing
        if (_targetAnchorPos.y <= _targetInitY - Screen.height * .1f)
        {
            //clamping top
            _tweening = true;
            _targetAnchorPos.y = _targetInitY;
            _rt.DOAnchorPos(_targetAnchorPos, .75f, true).OnComplete(onScrollTweenComplete);
        }
        else if (_targetAnchorPos.y >= _targetInitY + _currUserListHeight)
        {
            //clamping bottom
            _tweening = true;
            _targetAnchorPos.y = _targetInitY + _currUserListHeight - Screen.height * .1f;
            _rt.DOAnchorPos(_targetAnchorPos, .75f, true).OnComplete(onScrollTweenComplete);
        }
        else
        {
            _targetAnchorPos.y += (ySmoothDelta * 5);
            _rt.anchoredPosition = _targetAnchorPos;
        }

        //Debug.Log(_targetAnchorPos.y);
    }

    private void onScrollTweenComplete()
    {
        _tweening = false;
        Global.instance.ManagerTouch.resetTouchVars();
    }

    private void GuiUsersToggleAll(bool toggle)
    {
        for (int i = 0; i < _guiUsersPool.Count; i++)
        {
            GuiUser.ToggleGo(toggle, _guiUsersPool[i]);
        }
    }
}

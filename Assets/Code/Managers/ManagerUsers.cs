using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class ManagerUsers {

    private List<User> _currUsersList;
    private List<GuiUser> _guiUsersPool;
    private GameObject _go;

    private List<Texture2D> _avatars;

    public ManagerUsers()
    {
        _go = new GameObject();
        _go.name = "Users";

        _go.transform.SetParent(Global.instance.canvas.transform);

        RectTransform rt = _go.AddComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(0, Screen.height * .3f);

        _currUsersList = new List<User>();

        loadAvatars();
        createGuiUserPool();
        checkIfUserDataFetchComplete();
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
        if (Global.instance.managerData.Complete)
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
  
        for (int i = 0; i < 10; i++)
        {
            GuiUser guiUser = new GuiUser(dummyUser, getAvatarAsync(), _go.transform);
            spacerY = guiUser.SizeDelta.y * .25f;
            spacerX = guiUser.SizeDelta.y * .75f;

            guiUser.rt.anchoredPosition = new Vector2(spacerX, currTargetY);

            currTargetY -= (guiUser.SizeDelta.y + spacerY);

            _guiUsersPool.Add(guiUser);
        }
    }

    public Texture2D getAvatarAsync()
    {
        //the avatar probably should be separate async call to href as 
        //there is no reason to store binary with every User object
        return _avatars[UnityEngine.Random.Range(0, _avatars.Count)];
    }

    public void getUsersByAlpha(string currAlpha)
    {
        _currUsersList.Clear();
        GuiUsersToggleAll(false);

        if (currAlpha == "")
        {

        }
        else
        {
            _currUsersList = Global.instance.managerData.Users.Where(
                            u => u.UserName.StartsWith(currAlpha, StringComparison.OrdinalIgnoreCase))
                            .OrderByDescending(u2 => u2.UserLikes).ToList();

            if (_currUsersList.Count > 0)
            {
                for (int i = 0; i < _guiUsersPool.Count; i++)
                {
                    GuiUser.Update(_guiUsersPool[i], getAvatarAsync(), _currUsersList[i]);
                    GuiUser.ToggleGo(true, _guiUsersPool[i]);

                    if (i == _currUsersList.Count - 1)
                        break;
                }
            }

        }
    }

    private void GuiUsersToggleAll(bool toggle)
    {
        for (int i = 0; i < _guiUsersPool.Count; i++)
        {
            GuiUser.ToggleGo(toggle, _guiUsersPool[i]);
        }
    }
}

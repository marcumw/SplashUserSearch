using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerUsers {

    private List<GuiUser> _guiUsers;
    private GameObject _go;

    public ManagerUsers()
    {
        _guiUsers = new List<GuiUser>();

        _go = new GameObject();
        _go.name = "Users";

        _go.transform.parent = Global.instance.canvas.transform;

        checkIfUserDataFetchComplete();
    }

    private void checkIfUserDataFetchComplete()
    {
        if (Global.instance.managerData.Complete)
        {
            onUserDataFetchComplete();
        }
        else
        {
            Global.instance.onDelayedCall(.25f, checkIfUserDataFetchComplete);
        }
    }

    private void onUserDataFetchComplete()
    {
        GuiUser guiUser;
        foreach (User user in Global.instance.managerData.Users)
        {
            guiUser = new GuiUser(user, _go.transform);
        }
    }



}

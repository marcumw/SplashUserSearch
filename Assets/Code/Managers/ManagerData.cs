using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

public class ManagerData  {

    private bool _complete = false;
    private List<User> _users = new List<User>();

    public ManagerData()
    {
        loadUserData();
    }

    private void loadUserData()
    {
        string json = ((TextAsset)Resources.Load("data/users")).text;

        string jsonNew = "{\"users\":" + Regex.Replace(json, @"\s+", "") + "}";
        List<UserJson> usersJson = JsonUtility.FromJson<Users>(jsonNew).users;

        foreach (UserJson user in usersJson)
        {
            User newUser = new User(user.id, user.userName, user.userLikes);
            _users.Add(newUser);
        }

        _complete = true;
    }

    public bool Complete { get { return _complete; } }

    public List<User> Users { get { return _users; } }
}


#region user serialization
[Serializable]
public class Users
{
    public List<UserJson> users;
}

[Serializable]
public class UserJson
{
    public int id;
    public string userName;
    public int userLikes;
}

#endregion

public class User
{
    private int _id;
    private string _userName;
    private int _userLikes;

    public User(int id, string userName, int userLikes)
    {
        _id = id;
        _userName = userName;
        _userLikes = userLikes;
    }


    public int Id
    {
        get
        {
            return _id;
        }
    }

    public string UserName
    {
        get
        {
            return _userName;
        }
    }

    public int UserLikes
    {
        get
        {
            return _userLikes;
        }
    }

}

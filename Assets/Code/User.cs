using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public struct User
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



using UnityEngine;
using System.Collections;

public class UserSearch {
    
    public UserSearch()
    {
        Vector2 sizeDelta = new Vector2(Screen.width, Screen.height * .1f);
        Vector2 target = new Vector2(0, Screen.height / 2 - sizeDelta.y / 2);

        GuiInputField usersInputField = new GuiInputField("user search", "user name", target, Global.instance.canvas.transform, sizeDelta);
    }
}

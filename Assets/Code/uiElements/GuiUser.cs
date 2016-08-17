using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiUser {

    private User _user;
    private GameObject _go;
    private GuiText _gtUserName;
    private GuiText _gtUserLikes;
    private Image _image;

    public GuiUser(User user, Transform parent)
    {
        _user = user;

        _go = new GameObject();
        _go.name = "GuiUserBase";

        Vector2 sizeDelta = new Vector2(Screen.width, Screen.height * .1f);
        int fontSizeDefault = (int)(sizeDelta.y * .4f);

        _gtUserName = new GuiText(_go.transform, _user.UserName, _user.UserName, fontSizeDefault);

        _gtUserName.guiText.horizontalOverflow = HorizontalWrapMode.Overflow;
        _gtUserName.guiText.rectTransform.sizeDelta = new Vector2(sizeDelta.x, _gtUserName.guiText.preferredHeight);
        _gtUserName.guiText.transform.localPosition = Vector3.zero;
    }
}

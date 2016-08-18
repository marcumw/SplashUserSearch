using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiUser {

    private User _user;
    private GameObject _go;

    private RectTransform _rt;
    public RectTransform rt { get { return _rt; } set { _rt = value; } }

    private GuiText _gtUserName;
    private GuiText _gtUserLikes;
    private Image _image;
    private Sprite _sprite;
    private Texture2D _texture;

    private Vector2 _sizeDelta;
    public Vector2 SizeDelta { get { return _sizeDelta; } set { _sizeDelta = value; } }

    public GuiUser(User user, Texture2D texture, Transform parent)
    {
        _user = user;
        _texture = texture;

        _go = new GameObject();
        _go.name = "GuiUserPool";
        _go.transform.SetParent(parent);

        _rt = _go.AddComponent<RectTransform>();

        _sizeDelta = new Vector2(Screen.width, Screen.height * .1f);

        //set up image
        GameObject _goImage = new GameObject();
        _goImage.name = "avatar";

        _sprite = Sprite.Create(_texture, new Rect(0, 0, _texture.width, _texture.height), new Vector2(0.5f, 0.5f));
        _image = _goImage.AddComponent<Image>();

        _image.rectTransform.sizeDelta = new Vector2(_sizeDelta.y, _sizeDelta.y);
        _image.sprite = _sprite;

        _goImage.transform.SetParent(_go.transform);

        RectTransform rtImage = _goImage.GetComponent<RectTransform>();
        rtImage.anchoredPosition = new Vector2(-Screen.width / 2, 0);


        //set up Name
        int fontSizeNames = (int)(_sizeDelta.y * .4f);
        _gtUserName = new GuiText(_go.transform, _user.UserName, _user.UserName, fontSizeNames);
        _gtUserName.guiText.horizontalOverflow = HorizontalWrapMode.Overflow;
        _gtUserName.guiText.rectTransform.sizeDelta = new Vector2(_sizeDelta.x, _gtUserName.guiText.preferredHeight);

        Vector2 targetName = new Vector2(_image.rectTransform.sizeDelta.x * .8f, _gtUserName.Height * .25f);
        _gtUserName.SetPosition(targetName.x, targetName.y);


        //set up likes
        int fontSizeLikes = (int)(_sizeDelta.y * .25f);
        _gtUserLikes = new GuiText(_go.transform, "likes", _user.UserLikes.ToString() + " Likes", fontSizeLikes, "a4a4a4");
        _gtUserLikes.guiText.horizontalOverflow = HorizontalWrapMode.Overflow;
        _gtUserLikes.guiText.rectTransform.sizeDelta = new Vector2(_sizeDelta.x, _gtUserLikes.guiText.preferredHeight);
        //_gtUserLikes.guiText.transform.localPosition = new Vector3(0, -_sizeDelta.y/2, 0);

        Vector2 targetLikes = new Vector2(_image.rectTransform.sizeDelta.x * .8f, -_gtUserName.Height * .65f);
        _gtUserLikes.SetPosition(targetLikes.x, targetLikes.y);

        _rt.anchoredPosition = Vector2.zero;

        _go.SetActive(false);
    }

    public static void Update(GuiUser guiUser, Texture texture, User user)
    {
        guiUser._go.name = user.UserName;
        guiUser._gtUserName.guiText.text = user.UserName;
        guiUser._gtUserLikes.guiText.text = user.UserLikes.ToString() + " LIKES";
    }

    public static void ToggleGo(bool toggle, GuiUser guiUser)
    {
        //guiUser._go.GetComponent<Canvas>().enabled = toggle;
        guiUser._go.SetActive(toggle);
    }

}

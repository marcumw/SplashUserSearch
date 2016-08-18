using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public sealed class GuiInputField
{
    private GameObject _go;
    private GuiText _gt;
    private RectTransform _rt;
    private GuiText _gtPlaceholder;
    public GuiText PlaceHolder { get { return _gtPlaceholder; } }

    private InputField _inputField;
    public InputField InputField { get { return _inputField; } }

    private Sprite _spriteBackground, _spriteSearch, _spriteDelete;
    private Image _imageBackground, _imageSearch, _imageDelete;

    private string _initValue;
    private Vector3 _target;
    private Vector2 _sizeDelta;

    private string _name;
    private int _fontSizeDefault;
    private int _fontSizePlaceholder;
    private bool _reset = false;

    private Button _buttonDelete;

    private static Texture2D _textureBackground;
    private static Texture2D _textureSearchIcon;
    private static Texture2D _textureDeleteIcon;

    public GuiInputField(string name, string initValue, Vector3 target, Transform parent, Vector2 sizeDelta, 
                                TouchScreenKeyboardType keyboardType = TouchScreenKeyboardType.Default,
                                InputField.InputType inputType = InputField.InputType.Standard)
    {
        _go = new GameObject();
        _go.name = name;
        _go.transform.parent = parent;

        _rt = _go.AddComponent<RectTransform>();

        _name = name;
        _target = target;
        _initValue = initValue;
        _sizeDelta = sizeDelta;

        _rt.anchoredPosition = _target;


        //---------------
        //placeholder
        //---------------
        _fontSizePlaceholder = (int)(_sizeDelta.y * .4f);
        _gtPlaceholder = new GuiText(_go.transform, name + "Placeholder", _initValue, _fontSizePlaceholder, "545454", FontStyle.Italic);
        _gtPlaceholder.guiText.horizontalOverflow = HorizontalWrapMode.Overflow;

        Vector2 targetPlaceholder = new Vector2(-Screen.width / 2 + _gtPlaceholder.Width / 2 + _gtPlaceholder.Height * 1.75f, 0);
        _gtPlaceholder.SetPosition(targetPlaceholder.x, targetPlaceholder.y);


        //---------------
        // input text
        //---------------

        _fontSizeDefault = (int)(_sizeDelta.y * .4f);
        _gt = new GuiText(_go.transform, name + "TextBox", "", _fontSizeDefault);
        _gt.guiText.horizontalOverflow = HorizontalWrapMode.Overflow;
        _gt.guiText.rectTransform.sizeDelta = new Vector2(_sizeDelta.x, _gt.guiText.preferredHeight);

        Vector2 targetInput = new Vector2(_gt.Height * 1.75f, 0);
        _gt.SetPosition(targetInput.x, 0);

        if (_textureBackground == null)
        {
            _textureBackground = (Texture2D)Resources.Load("background");
            _textureSearchIcon = (Texture2D)Resources.Load("icons/icnSearch@3x");
            _textureDeleteIcon = (Texture2D)Resources.Load("icons/icnDelete");

        }

        //---------------
        //background
        //---------------
        _spriteBackground = Sprite.Create(_textureBackground, new Rect(0, 0, _textureBackground.width, _textureBackground.height), new Vector2(0.5f, 0.5f));
        _imageBackground = _go.AddComponent<Image>();

        _imageBackground.rectTransform.sizeDelta = _sizeDelta;
        _imageBackground.sprite = _spriteBackground;

        //---------------
        //search icon
        //---------------
        Vector2 sizeDeltaSearchIcon = new Vector2(_sizeDelta.y * .35f, _sizeDelta.y * .35f);
        GameObject goSearch = new GameObject();
        goSearch.name = "icnSearch";

        goSearch.transform.SetParent(_go.transform);

        _spriteSearch = Sprite.Create(_textureSearchIcon, new Rect(0, 0, _textureSearchIcon.width, _textureSearchIcon.height), new Vector2(0.5f, 0.5f));
        _imageSearch = goSearch.AddComponent<Image>();

        _imageSearch.rectTransform.sizeDelta = sizeDeltaSearchIcon;
        _imageSearch.sprite = _spriteSearch;

        RectTransform rtSearch = goSearch.GetComponent<RectTransform>();
        rtSearch.anchoredPosition = new Vector2(-Screen.width / 2 + sizeDeltaSearchIcon.x, 0);

        //---------------
        //delete icon
        //---------------

        Vector2 sizeDeltaDeleteIcon = new Vector2(_sizeDelta.y * .35f, _sizeDelta.y * .35f);
        GameObject goDelete = new GameObject();
        goDelete.name = "icnDelete";

        _buttonDelete = goDelete.AddComponent<Button>();
        _buttonDelete.onClick.AddListener(delegate { onButtonDeleteClicked(); });

        goDelete.transform.SetParent(_go.transform);

        _spriteDelete = Sprite.Create(_textureDeleteIcon, new Rect(0, 0, _textureDeleteIcon.width, _textureDeleteIcon.height), new Vector2(0.5f, 0.5f));
        _imageDelete = goDelete.AddComponent<Image>();

        _imageDelete.rectTransform.sizeDelta = sizeDeltaDeleteIcon;
        _imageDelete.sprite = _spriteDelete;

        RectTransform rtDelete = goDelete.GetComponent<RectTransform>();
        rtDelete.anchoredPosition = new Vector2(Screen.width / 2 - sizeDeltaDeleteIcon.x, 0);


        _inputField = _go.AddComponent<InputField>();
        _inputField.lineType = InputField.LineType.SingleLine;
        _inputField.textComponent = _gt.guiText;
        _inputField.image = _imageBackground;
        _inputField.keyboardType = keyboardType;
        _inputField.characterLimit = 50;
        _inputField.shouldHideMobileInput = true;
        _inputField.inputType = inputType;
        _inputField.placeholder = _gtPlaceholder.guiText;

    }

    private void onButtonDeleteClicked()
    {

        GuiInputField.reset(this);
    }

    public static void reset(GuiInputField to)
    {
        to._reset = true;
        to._inputField.text = "";
        to._inputField.characterLimit = 50;
        to._gt.guiText.fontSize = to._fontSizeDefault;
        to._gtPlaceholder.guiText.text = to._initValue;
        to._gtPlaceholder.guiText.color = Utils.HexToColor("949494");
    }


    public RectTransform rectTransform {
        get
        {
            return _rt;
        }
    }

    public GuiText guiText
    {
        get
        {
            return _gt;
        }
    }

    public GameObject go
    {
        get
        {
            return _go;
        }
    }

}
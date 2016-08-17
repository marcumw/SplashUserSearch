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
    private InputField _inputField;
    private Sprite _sprite;
    private Image _image;

    private string _initValue;
    private Vector3 _target;
    private Vector2 _sizeDelta;

    private string _name;
    private int _fontSizeDefault;
    private int _fontSizePlaceholder;
    private bool _reset = false;

    private static Texture2D _texture;

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

        _fontSizeDefault = (int)(_sizeDelta.y * .4f);
        _fontSizePlaceholder = (int)(_sizeDelta.y * .4f);

        _gt = new GuiText(_go.transform, name + "TextBox", "", _fontSizeDefault);
        _gt.guiText.horizontalOverflow = HorizontalWrapMode.Overflow;
        _gt.guiText.rectTransform.sizeDelta = new Vector2(_sizeDelta.x, _gt.guiText.preferredHeight);
        _gt.guiText.transform.localPosition = Vector3.zero;

        if (_texture == null)
        {
            _texture = (Texture2D)Resources.Load("background");
        }

        _sprite = Sprite.Create(_texture, new Rect(0, 0, _texture.width, _texture.height), new Vector2(0.5f, 0.5f));
        _image = _go.AddComponent<Image>();

        _image.rectTransform.sizeDelta = _sizeDelta;
        _image.sprite = _sprite;

        _inputField = _go.AddComponent<InputField>();
        _inputField.lineType = InputField.LineType.SingleLine;
        _inputField.textComponent = _gt.guiText;
       
        _inputField.image = _image;
        _inputField.keyboardType = keyboardType;
        _inputField.characterLimit = 50;
        _inputField.shouldHideMobileInput = true;
        _inputField.inputType = inputType;

        //_inputField.onValueChange.AddListener(delegate { onInputFieldValueChange(); });

        _gtPlaceholder = new GuiText(_go.transform, name + "Placeholder", _initValue, _fontSizePlaceholder, "949494", FontStyle.Italic);
        _gtPlaceholder.guiText.horizontalOverflow = HorizontalWrapMode.Overflow;

        _inputField.placeholder = _gtPlaceholder.guiText;

        //OnPointerDownOverride onPointerDown = _inputField.gameObject.AddComponent<OnPointerDownOverride>();
        //onPointerDown._tfo = this;

        //OnPointerExitOverride onPointerExit = _inputField.gameObject.AddComponent<OnPointerExitOverride>();
        //onPointerExit._tfo = this;

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
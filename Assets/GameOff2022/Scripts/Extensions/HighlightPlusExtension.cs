using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using HighlightPlus;
using TMPro;
using UnityEngine;

public class HighlightPlusExtension : HighlightEffect {
    protected bool _textBillboardEnabled;
    public bool TextBillboardEnabled {
        get => _textBillboardEnabled;
        set {
            if(TextBillboard) TextBillboard.enabled = value;
            _textBillboardEnabled = value;
        }
    }
    [SerializeField] protected TextMeshPro TextBillboard;
}

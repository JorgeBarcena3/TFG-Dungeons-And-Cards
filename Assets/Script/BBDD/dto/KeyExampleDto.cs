using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KeyExampleDto : DtoFirebase
{
    
    [SerializeField]
    public string Attr1 { get; set; }

    [SerializeField]
    public string Attr2 { get; set; }

    [SerializeField]
    public string Attr3 { get; set; }
}

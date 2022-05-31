using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newReadableText", menuName = "In-Game Text / Readable" )]
public class D_ReadableText : ScriptableObject
{

    public List<string> texts = new List<string>();
}

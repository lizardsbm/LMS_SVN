using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomText : Text
{
    public override string text {
        get => base.text;

        set {
            string Txt = value;

            if(Txt != null) {
                Txt = Txt.Replace("\\n", "\n");
            }

            base.text = Txt;
        }

    }
}

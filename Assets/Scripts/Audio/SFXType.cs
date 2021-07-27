using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXType : MonoBehaviour
{
    public enum SoundType
    {
        SWITCH_OPTION,
        CHOPPING_STONE,
        CHOPPING_TREE,
        CRAFT,
        RUBY
    }

    public SoundType type;
}

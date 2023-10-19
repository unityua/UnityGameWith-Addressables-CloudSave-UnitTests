using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PesPatron.Helpers
{
    public static class Layers
    {
        public static readonly int Default = LayerMask.NameToLayer("Default");
        public static readonly int Characters = LayerMask.NameToLayer("Characters");
        public static readonly int CharactersTrigger = LayerMask.NameToLayer("CharactersTrigger");
    }
}
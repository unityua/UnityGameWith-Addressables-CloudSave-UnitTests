using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using PesPatron.Helpers;
using UnityEngine;
using UnityEngine.TestTools;

public class LayersTest
{
    [Test]
    public void Default()
    {
        Assert.AreNotEqual(Layers.Default, -1);
    }

    [Test]
    public void Characters()
    {
        Assert.AreNotEqual(Layers.Characters, -1);
    }

    [Test]
    public void CharactersTrigger()
    {
        Assert.AreNotEqual(Layers.CharactersTrigger, -1);
    }
}

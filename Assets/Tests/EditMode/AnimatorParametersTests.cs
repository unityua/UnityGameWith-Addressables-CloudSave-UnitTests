using NUnit.Framework;
using PesPatron.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorParametersTests
{
    [Test]
    public void VelocityX()
    {
        Assert.AreEqual(AnimatorParameters.VelocityX, Animator.StringToHash("VelocityX"));
    }

    [Test]
    public void VelocityZ()
    {
        Assert.AreEqual(AnimatorParameters.VelocityZ, Animator.StringToHash("VelocityZ"));
    }

    [Test]
    public void IsMoving()
    {
        Assert.AreEqual(AnimatorParameters.IsMoving, Animator.StringToHash("IsMoving"));
    }
}

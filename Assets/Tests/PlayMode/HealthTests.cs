using System.Collections;
using NUnit.Framework;
using PesPatron.Characters;
using UnityEngine;
using UnityEngine.TestTools;

public class HealthTests
{
    [Test]
    public void HealthNegativeDamageTest()
    {
        Health health = new GameObject().AddComponent<Health>();

        int healthAmount = 1;

        health.SetHealth(healthAmount);

        health.ApplyDamage(-1);

        Assert.AreEqual(health.HealthAmount, healthAmount);
    }

    [Test]
    public void HealthZeroHealthAfterDeath()
    {
        Health health = new GameObject().AddComponent<Health>();

        int healthAmount = 10;

        health.SetHealth(healthAmount);

        health.ApplyDamage(1000);

        Assert.AreEqual(health.HealthAmount, 0);
    }
}

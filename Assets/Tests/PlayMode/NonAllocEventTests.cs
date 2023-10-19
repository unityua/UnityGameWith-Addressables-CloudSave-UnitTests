using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using PesPatron.Characters;
using PesPatron.Helpers;
using UnityEngine;
using UnityEngine.TestTools;

public class NonAllocEventTests
{
    [Test]
    public void SubscribingAndInvoking()
    {
        NonAllocEvent<int> nonAllocEvent = new NonAllocEvent<int>();

        List<int> results = new List<int>();

        nonAllocEvent.Add((r) => results.Add(r));

        nonAllocEvent.Invoke(0);
        nonAllocEvent.Invoke(1);
        nonAllocEvent.Invoke(2);
        nonAllocEvent.Invoke(3);

        bool resultsCountMatch = results.Count == 4;
        bool resultsOrderMatch = results[0] == 0 && results[1] == 1 && results[2] == 2 && results[3] == 3;

        Assert.IsTrue(resultsCountMatch && resultsOrderMatch);
    }

    [Test]
    public void InvokeOrder()
    {
        NonAllocEvent<int> nonAllocEvent = new NonAllocEvent<int>();

        List<int> result = new List<int>();

        nonAllocEvent.Add(r => result.Add(r + 1));
        nonAllocEvent.Add(r => result.Add(r - 1));
        nonAllocEvent.Add(r => result.Add(r));

        nonAllocEvent.Invoke(0);

        bool resultsMatched = result[0] == 0 && result[1] == -1 && result[2] == 1;
        Assert.IsTrue(resultsMatched);
    }
}

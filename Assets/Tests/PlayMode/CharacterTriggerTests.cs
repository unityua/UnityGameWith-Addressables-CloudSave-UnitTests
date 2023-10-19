using NUnit.Framework;
using PesPatron.Characters;
using PesPatron.Helpers;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class CharacterTriggerTests 
{
    [UnityTest]
    public IEnumerator TriggerEnter()
    {
        bool sameCharacterEntered = false;
        CharactersTrigger trigger = CreateCharacterTrigger(Vector3.zero, 1f);
        trigger.CharacterEntered += e => sameCharacterEntered = true;

        Character character = CreateCharacter(Vector3.up * 2f);

        yield return new WaitForSeconds(1);

        Assert.IsTrue(sameCharacterEntered);
    }

    [UnityTest]
    public IEnumerator TriggerExit()
    {
        bool characterEntered = false;
        bool characterExited = false;
        CharactersTrigger trigger = CreateCharacterTrigger(Vector3.zero, 1f);
        trigger.CharacterEntered += e => characterEntered = true;
        trigger.CharacterExited += e => characterExited = true;

        Character character = CreateCharacter(Vector3.up * 2f);

        yield return new WaitForSeconds(1);

        Assert.IsTrue(characterEntered && characterExited);
    }

    private CharactersTrigger CreateCharacterTrigger(Vector3 position,  float radius)
    {
        CharactersTrigger trigger = new GameObject().AddComponent<CharactersTrigger>();
        trigger.gameObject.layer = Layers.CharactersTrigger;
        SphereCollider collider = trigger.gameObject.AddComponent<SphereCollider>();
        collider.radius = radius;
        collider.isTrigger = true;

        trigger.transform.position = position;

        return trigger;
    }

    private Character CreateCharacter(Vector3 position)
    {
        Character character = new GameObject().AddComponent<Character>();
        character.gameObject.layer = Layers.Characters;
        SphereCollider collider = character.gameObject.AddComponent<SphereCollider>();
        collider.radius = 1f;
        Rigidbody rigidbody = character.gameObject.AddComponent<Rigidbody>();

        character.transform.position = position;

        return character;
    }
}

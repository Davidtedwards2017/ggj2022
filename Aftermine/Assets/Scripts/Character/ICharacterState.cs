using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterState
{
    CharacterState State { get; }
    void Update();
    void LateUpdate();
    void OnEnter();
    void OnExit();
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DescriptedAction
{
    public Action action;
    public string description;

    public static implicit operator Action(DescriptedAction descriptedAction) {
        return descriptedAction.action;
    }

    public static implicit operator string(DescriptedAction descriptedAction) {
        return descriptedAction.description;
    }

    public DescriptedAction(Action action, string description = "") {
        this.action = action;
        this.description = description;
    }
}

public static class ActionExtensions
{
    public static DescriptedAction d(this Action a, string description = "") {
        return new DescriptedAction(a, description);
    }
}
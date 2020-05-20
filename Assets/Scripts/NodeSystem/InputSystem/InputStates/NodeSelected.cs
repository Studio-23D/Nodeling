﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeSystem;

public class NodeSelected : InputState
{
    public NodeSelected(SystemEventHandler eventHandler) : base(eventHandler)
    {
        this.conditions.Add(InputTypes.Hold, new Movement(eventHandler));
    }

    public override void OnElementClick(Element element)
    {
        base.OnElementClick(element);
    }

    public override void OnElementHover(Element element)
    {
        base.OnElementHover(element);
    }

    public override void OnElementSelected(Element element)
    {
        base.OnElementSelected(element);
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
    }

    public override void OnStateLeave()
    {
        base.OnStateLeave();
    }
}

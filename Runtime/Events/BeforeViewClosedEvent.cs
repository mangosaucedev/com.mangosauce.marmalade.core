using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Marmalade.UI;

namespace Marmalade
{
    public class BeforeViewClosedEvent : Event
    {
        public ActiveView view;

        public override string Name => "Before View Closed";

        public BeforeViewClosedEvent(ActiveView view) : base()
        {
            this.view = view;
        }
    }
}
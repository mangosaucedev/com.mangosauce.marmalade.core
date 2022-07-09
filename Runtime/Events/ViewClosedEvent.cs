using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Marmalade.UI;

namespace Marmalade
{
    public class ViewClosedEvent : Event
    {
        public ActiveView view;

        public override string Name => "View Closed";

        public ViewClosedEvent(ActiveView view) : base()
        {
            this.view = view;
        }
    }
}

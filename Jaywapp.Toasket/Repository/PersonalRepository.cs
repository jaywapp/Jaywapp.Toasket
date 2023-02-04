using Jaywapp.Toasket.Model;
using System;
using System.Collections.Generic;

namespace Jaywapp.Toasket.Repository
{
    public class PersonalRepository
    {
        public List<Box> Boxes { get; } = new List<Box>();

        public EventHandler Updated;

        public void Add(Box box)
        {
            Boxes.Add(box);
            InvokeEvent();
        }

        public void Delete(Box box)
        {
            Boxes.Remove(box);
            InvokeEvent();
        }

        private void InvokeEvent() => Updated?.Invoke(this, EventArgs.Empty);
    }
}

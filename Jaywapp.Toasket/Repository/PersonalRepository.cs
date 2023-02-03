using Jaywapp.Toasket.Model;
using System.Collections.Generic;

namespace Jaywapp.Toasket.Repository
{
    public class PersonalRepository
    {
        public List<Box> Boxes { get; } = new List<Box>();


        public void Add(Box box)
        {
            Boxes.Add(box);
        }
    }
}

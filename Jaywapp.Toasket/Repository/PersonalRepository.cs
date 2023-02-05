using Jaywapp.Toasket.Interface;
using Jaywapp.Toasket.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Jaywapp.Toasket.Repository
{
    public class PersonalRepository : IXmlFileSerializable
    {
        private const string FILE_NAME = @"PersonalRepository.xml";

        public List<Box> Boxes { get; set; } = new List<Box>();

        public EventHandler Updated;

        public PersonalRepository()
        {
            Load(FILE_NAME);
        }

        ~PersonalRepository()
        {
            Save(FILE_NAME);
        }

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

        public XElement Serialize()
        {
            var element = new XElement(nameof(PersonalRepository));

            foreach(var box in Boxes)
                element.Add(box.Serialize());

            return element;
        }

        public void Serialize(XElement element)
        {
            if (element.Name != nameof(PersonalRepository))
                return;

            var boxes = element
                .Elements(nameof(Box))
                .Select(e => e.Load<Box>())
                .ToList();

            Boxes = boxes;
        }

        public void Load(string path)
        {
            if (File.Exists(path))
                Serialize(XDocument.Load(path).Root);
        }

        public void Save(string path)
        {
            var element = Serialize();
            element.Save(path);
        }
    }
}

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
        #region Const Field
        private const string FILE_NAME = @"PersonalRepository.xml";
        #endregion

        #region Properties
        public List<Box> Boxes { get; set; } = new List<Box>();
        #endregion

        #region Event
        public EventHandler Updated;
        #endregion

        #region Constructor
        public PersonalRepository()
        {
            Load(FILE_NAME);
        }
        #endregion

        #region Destructor
        ~PersonalRepository()
        {
            Save(FILE_NAME);
        }
        #endregion

        #region Functions
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

        public void Refresh() => InvokeEvent();

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
        #endregion
    }
}

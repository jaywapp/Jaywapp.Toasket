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
        /// <summary>
        /// 사용자가 선택한 매치 박스 목록
        /// </summary>
        public List<Box> Boxes { get; set; } = new List<Box>();
        #endregion

        #region Event
        /// <summary>
        /// update 이벤트
        /// </summary>
        public EventHandler Updated;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PersonalRepository()
        {
            Load(FILE_NAME);
        }
        #endregion

        #region Destructor
        /// <summary>
        /// Destructor
        /// </summary>
        ~PersonalRepository()
        {
            Save(FILE_NAME);
        }
        #endregion

        #region Functions
        /// <summary>
        /// Box를 추가합니다.
        /// </summary>
        /// <param name="box"></param>
        public void Add(Box box)
        {
            Boxes.Add(box);
            InvokeEvent();
        }

        /// <summary>
        /// Box를 제거합니다.
        /// </summary>
        /// <param name="box"></param>
        public void Delete(Box box)
        {
            Boxes.Remove(box);
            InvokeEvent();
        }

        /// <summary>
        /// 목록을 갱신합니다. (<see cref="Updated"/> 이벤트 발생)
        /// </summary>
        public void Refresh() => InvokeEvent();

        /// <summary>
        /// 이벤트를 발생합니다.
        /// </summary>
        private void InvokeEvent() => Updated?.Invoke(this, EventArgs.Empty);

        /// <inheritdoc/>
        public XElement Serialize()
        {
            var element = new XElement(nameof(PersonalRepository));

            foreach(var box in Boxes)
                element.Add(box.Serialize());

            return element;
        }

        /// <inheritdoc/>
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

        /// <summary>
        /// <paramref name="path"/>를 읽어 객체를 구성합니다.
        /// </summary>
        /// <param name="path"></param>
        public void Load(string path)
        {
            if (File.Exists(path))
            {
                Serialize(XDocument.Load(path).Root);
                InvokeEvent();
            }
        }

        /// <summary>
        /// 객체를 <paramref name="path"/>에 저장합니다.
        /// </summary>
        /// <param name="path"></param>
        public void Save(string path)
        {
            var element = Serialize();
            element.Save(path);
        }
        #endregion
    }
}

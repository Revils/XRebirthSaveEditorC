using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Linq;

namespace XRebirthSaveEditorC
{
    public class CMember : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private XElement _crewMember;
        private XElement _boarding;
        private XElement _combat;
        private XElement _engineering;
        private XElement _navigation;
        private XElement _leadership;
        private XElement _morale;
        private XElement _science;
        private XElement _management;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChangedEvent = this.PropertyChanged;
            if (propertyChangedEvent != null)
            {
                propertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public XElement CrewMember
        {
            get
            {
                return this._crewMember;
            }
            set
            {
                this._crewMember = value;
                this.OnPropertyChanged("CrewMember");
            }
        }

        public XElement Boarding
        {
            get
            {
                return this._boarding;
            }
            set
            {
                this._boarding = value;
                this.OnPropertyChanged("Boarding");
            }
        }

        public XElement Combat
        {
            get
            {
                return this._combat;
            }
            set
            {
                this._combat = value;
                this.OnPropertyChanged("Combat");
            }
        }

        public XElement Engineering
        {
            get
            {
                return this._engineering;
            }
            set
            {
                this._engineering = value;
                this.OnPropertyChanged("Engineering");
            }
        }

        public XElement Navigation
        {
            get
            {
                return this._navigation;
            }
            set
            {
                this._navigation = value;
                this.OnPropertyChanged("Navigation");
            }
        }

        public XElement Leadership
        {
            get
            {
                return this._leadership;
            }
            set
            {
                this._leadership = value;
                this.OnPropertyChanged("Leadership");
            }
        }

        public XElement Morale
        {
            get
            {
                return this._morale;
            }
            set
            {
                this._morale = value;
                this.OnPropertyChanged("Morale");
            }
        }

        public XElement Science
        {
            get
            {
                return this._science;
            }
            set
            {
                this._science = value;
                this.OnPropertyChanged("Science");
            }
        }

        public XElement Management
        {
            get
            {
                return this._management;
            }
            set
            {
                this._management = value;
                this.OnPropertyChanged("Management");
            }
        }


        public CMember(XElement crewMember)
        {
            CrewMember = crewMember;
            IEnumerable<XElement> skillquery =
                from skill in CrewMember.Elements("skills").Elements("skill")
                select skill;

            foreach (XElement skill in skillquery)
            {
                switch (skill.Attribute("type").Value)
                {
                    case "boarding":
                        Boarding = skill;
                        break;
                    case "combat":
                        Combat = skill;
                        break;
                    case "engineering":
                        Engineering = skill;
                        break;
                    case "navigation":
                        Navigation = skill;
                        break;
                    case "leadership":
                        Leadership = skill;
                        break;
                    case "morale":
                        Morale = skill;
                        break;
                    case "science":
                        Science = skill;
                        break;
                    case "management":
                        Management = skill;
                        break;
                }
            }
        }

        public void addAttributeNode(string attribute)
        {
            XElement skill = new XElement("skill", new object[]
			{
				new XAttribute("type", attribute),
				new XAttribute("value", "0")
			});

            CrewMember.Elements("skills").First<XElement>().Add(skill);

            switch (skill.Attribute("type").Value)
            {
                case "boarding":
                    Boarding = skill;
                    break;
                case "combat":
                    Combat = skill;
                    break;
                case "engineering":
                    Engineering = skill;
                    break;
                case "navigation":
                    Navigation = skill;
                    break;
                case "leadership":
                    Leadership = skill;
                    break;
                case "morale":
                    Morale = skill;
                    break;
                case "science":
                    Science = skill;
                    break;
                case "management":
                    Management = skill;
                    break;
            }
        }
    }
}

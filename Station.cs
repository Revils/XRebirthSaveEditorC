using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Collections.ObjectModel;

namespace XRebirthSaveEditorC
{
    public class Station : Component
    {
        private ObservableCollection<XElement> _orders;
        private ObservableCollection<XElement> _offers;
        private ObservableCollection<CMember> _crew;

        public ObservableCollection<XElement> Orders
        {
            get
            {
                return this._orders;
            }
            set
            {
                this._orders = value;
                this.OnPropertyChanged("Orders");
            }
        }
        public ObservableCollection<XElement> Offers
        {
            get
            {
                return this._offers;
            }
            set
            {
                this._offers = value;
                this.OnPropertyChanged("Offers");
            }
        }
        public ObservableCollection<CMember> Crew
        {
            get
            {
                return this._crew;
            }
            set
            {
                this._crew = value;
                this.OnPropertyChanged("Crew");
            }
        }

        public Station(XElement station)
            : base(station)
        {
            this._orders = new ObservableCollection<XElement>();
            this._offers = new ObservableCollection<XElement>();
            this._crew = new ObservableCollection<CMember>();

            IEnumerable<XElement> crewquery =
                        from crew in this.Data.Descendants().Elements("component")
                        where (string) crew.Attribute("class") == "npc"
                        select crew;

            foreach (XElement person in crewquery)
                Crew.Add(new CMember(person));

            foreach (XElement order in this.Data.Elements("trade").Elements("orders").Elements("trade"))
                Orders.Add(order);

            foreach (XElement offer in this.Data.Elements("trade").Elements("offers").Descendants<XElement>().Elements("trade"))
                Offers.Add(offer);

        }

        public void addCrewAttributeNode(string attribute, CMember crewMember)
        {
            if (this.Crew.Contains(crewMember))
            {
                crewMember.addAttributeNode(attribute);
            }
        }
    }
}

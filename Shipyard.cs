using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using System.Windows;

namespace XRebirthSaveEditorC
{
    public class Shipyard : Station
    {
        private ObservableCollection<Ship> _shipBuildQueue;
        private ObservableCollection<XElement> _constructionWares;

        public ObservableCollection<Ship> ShipBuildQueue
        {
            get
            {
                return this._shipBuildQueue;
            }
            set
            {
                this._shipBuildQueue = value;
                this.OnPropertyChanged("ShipBuildQueue");
            }
        }

        public ObservableCollection<XElement> ConstructionWares
        {
            get
            {
                return this._constructionWares;
            }
            set
            {
                this._constructionWares = value;
                this.OnPropertyChanged("ConstructionWares");
            }
        }

        public Shipyard(XElement shipyard)
            : base(shipyard)
        {
            _shipBuildQueue = new ObservableCollection<Ship>();
            _constructionWares = new ObservableCollection<XElement>();

            IEnumerable<XElement> buildmodules = this.Data.Descendants().Elements("connection").Where(delegate(XElement buildmodule)
            {
                bool result = false;
                if (buildmodule.Attribute("connection") != null && buildmodule.Attribute("connection").Value.ToString().Contains("buildmodule"))
                {
                    result = true;
                }
                return result;
            });

            IEnumerable<XElement> shipQueue = buildmodules.Descendants<XElement>().Elements("component").Where(delegate(XElement ship)
            {
                bool result = false;
                if (ship.Attribute("class") != null && ship.Attribute("class").Value.ToString().Contains("ship"))
                {
                    result = true;
                }
                return result;
            });

            foreach (XElement ship in shipQueue)
                ShipBuildQueue.Add(new Ship(ship));


            IEnumerable<XElement> constructionWaresQuery = from wares in buildmodules.Descendants<XElement>().Elements("resources").Elements("ware")
                                                           select wares;

            foreach (XElement ware in constructionWaresQuery)
                ConstructionWares.Add(ware);
        }

        public void finishShip(Ship shipyardQueueShip)
        {
            if (this.ShipBuildQueue.Contains(shipyardQueueShip))
            {
                foreach (XElement ware in ConstructionWares)
                {
                    ware.Attribute("amount").Value = "0";
                }
                MessageBox.Show("Ship wares successfully transferred to shipyard");
            }
        }
    }
}

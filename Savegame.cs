using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Windows;

namespace XRebirthSaveEditorC
{
    public class Savegame : INotifyPropertyChanged
    {
        private XElement _data;
        private ObservableCollection<Ship> _playerShips;
        private ObservableCollection<Shipyard> _shipyards;
        private ObservableCollection<Station> _stations;
        private string _path;
        public event PropertyChangedEventHandler PropertyChanged;

        public XElement Data
        {
            get
            {
                return this._data;
            }
            set
            {
                this._data = value;
                this.OnPropertyChanged("Data");
            }
        }

        public ObservableCollection<Ship> PlayerShips
        {
            get
            {
                return this._playerShips;
            }
            set
            {
                this._playerShips = value;
                this.OnPropertyChanged("PlayerShips");
            }
        }
        public ObservableCollection<Shipyard> Shipyards
        {
            get
            {
                return this._shipyards;
            }
            set
            {
                this._shipyards = value;
                this.OnPropertyChanged("Shipyards");
            }
        }
        public ObservableCollection<Station> Stations
        {
            get
            {
                return this._stations;
            }
            set
            {
                this._stations = value;
                this.OnPropertyChanged("Stations");
            }
        }
        public string Path
        {
            get
            {
                return this._path;
            }
            set
            {
                this._path = value;
            }
        }
	

        public Savegame(string fileLoc)
        {
            this._playerShips = new ObservableCollection<Ship>();
            this._shipyards = new ObservableCollection<Shipyard>();
            this._stations = new ObservableCollection<Station>();

            Path = fileLoc;
            Data = XElement.Load(_path, LoadOptions.PreserveWhitespace);

            IEnumerable<XElement> ships = from ship in _data.Descendants().Elements("component")
                                          where (string) ship.Attribute("owner") == "player" && (string) ship.Attribute("state") != "wreck" && ((string) ship.Attribute("class") == "ship_xl" | (string) ship.Attribute("class") == "ship_l" | (string) ship.Attribute("class") == "ship_s" | (string) ship.Attribute("class") == "ship_xs")
                                          orderby (string) ship.Attribute("name")
                                          select ship;

            foreach (XElement ship in ships)
                PlayerShips.Add(new Ship(ship));

            IEnumerable<XElement> shipyards = this._data.Descendants().Elements("component").Where(delegate(XElement shipyard)
            {
                bool result = false;
                if (shipyard.Attribute("macro") != null && shipyard.Attribute("macro").Value.ToString().Contains("shipyard"))
                {
                    result = true;
                }
                return result;
            }
            );

            foreach (XElement shipyard in shipyards)
                Shipyards.Add(new Shipyard(shipyard));


            IEnumerable<XElement> stations = this._data.Descendants().Elements("component").Where(delegate(XElement ele)
            {
                bool result = false;
                if (ele.Attribute("owner") != null && ele.Attribute("class") != null && (ele.Attribute("owner").Value.ToString() == "player") && ele.Attribute("class").Value.Contains("station"))
                {
                    result = true;
                }
                return result;
                /*
                }
       
                ).OrderBy(delegate(XElement ele)
                {
                    bool result = false;
                    if (ele.Attribute("owner") != null && ele.Attribute("class") != null && (ele.Attribute("owner").Value.ToString() == "player") && ele.Attribute("class").Value.Contains("station"))
                    {
                        result = true;
                    }
                    return result;*/
            });

            foreach (XElement station in stations)
                Stations.Add(new Station(station));
        }

        private void OnPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public void removeMod(XElement modElement)
        {
            if (modElement != null)
            {
                modElement.Remove();
            }
        }

        public void repairShip(Ship ship)
        {
            ship.repairShip();
        }

        public void addCargo(Ship ship, string cargoType)
        {
            ship.addCargo(cargoType);
        }

        public void removeCargo(Ship ship, XElement cargo)
        {
            ship.removeCargo(cargo);
        }

        public void removeTrade(Ship ship, XElement trade)
        {
            ship.removeTrade(trade);
        }

        public void adjustMoney(string money)
        {
            XElement account = this.Data.Descendants().Elements("factions").Elements("faction").Where(delegate(XElement ele)
            {
                bool result = false;
                if (ele.Attribute("id") != null && ele.Attribute("id").Value.ToString() == "player")
                {
                    result = true;
                }

                return result;
            }
            ).Elements("account").First<XElement>();

            string accountId = account.Attribute("id").Value;
            account.Attribute("amount").Value = money;

            IEnumerable<XElement> list = this.Data.Descendants().Elements("account").Where(delegate(XElement ele)
               {
                   bool result = false;
                   if (ele.Attribute("id") != null && ele.Attribute("id").Value.ToString() == accountId)
                   {
                       result = true;
                   }

                   return result;
               }
               );

            foreach (XElement node in list)
                node.Attribute("amount").Value = money;

            list = this.Data.Elements("stats").Elements("stat").Where(delegate(XElement ele)
            {
                bool result = false;
                if (ele.Attribute("id") != null && ele.Attribute("id").Value.ToString() == "money_player")
                {
                    result = true;
                }

                return result;
            }
            );

            foreach (XElement node in list)
                node.Attribute("value").Value = money;
        }

        public void addCrewAttributeNode(string attribute, Ship ship, CMember crewMember)
        {
            if (_playerShips.Contains(ship))
            {
                ship.addCrewAttributeNode(attribute, crewMember);
            }
        }

        public void addCrewAttributeStationNode(string attribute, Station station, CMember crewMember)
        {
            if (_stations.Contains(station))
            {
                station.addCrewAttributeNode(attribute, crewMember);
            }
        }

        public void addDroneShip(Ship ship)
        {
            ship.addDrone();
        }

        public void removeDroneShip(Ship ship, XElement drone)
        {
            ship.removeDrone(drone);
        }

        public void addGravidar(Ship ship)
        {
            ship.addGravidar();
        }

        public void finishShip(Shipyard shipyard, Ship ship)
        {
            shipyard.finishShip(ship);
            this._playerShips = new ObservableCollection<Ship>();
            IEnumerable<XElement> enumerable =
                from element in this.Data.Descendants().Elements("component")
                where (string) element.Attribute("owner") == "player" && (string) element.Attribute("state") != "wreck" && ((string) element.Attribute("class") == "ship_xl" | (string) element.Attribute("class") == "ship_l" | (string) element.Attribute("class") == "ship_s" | (string) element.Attribute("class") == "ship_xs")
                select element;
            try
            {
                IEnumerator<XElement> enumerator = enumerable.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    XElement current = enumerator.Current;
                    this._playerShips.Add(new Ship(current));
                }
            }
            finally
            {
                IEnumerator<XElement> enumerator = null;
                if (enumerator != null)
                {
                    enumerator.Dispose();
                }
            }
        }

        public void deleteShip(Ship ship)
        {
            if (ship.Data.Attribute("macro").Value == "unit_player_ship_macro")
            {
                if (this._playerShips.Contains(ship))
                {
                    ship.Data.Parent.Remove();
                    this._playerShips.Remove(ship);
                }
            }
            else
            {
                MessageBox.Show("You don't want to delete the skunk, will you? :)", "Warning", MessageBoxButton.OK);
            }
        }
        public void save()
        {
            this.Data.Save(_path, SaveOptions.DisableFormatting);
        }

        public void deleteAllMods()
        {
            XElement xElement = this.Data.Element("info").Element("patches");
            if (xElement != null)
            {
                xElement.Remove();
            }
        }

        public void finishBuild(Ship ship)
        {
            ObservableCollection<XElement> wares = ship.ConstructionWares;

            foreach (XElement ware in wares)
            {
                ware.SetAttributeValue("amount","0");
            }
        }
    }
}

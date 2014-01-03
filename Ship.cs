using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using System.Collections.ObjectModel;
using System.Windows;

namespace XRebirthSaveEditorC
{
    public class Ship : Component
    {
        private ObservableCollection<CMember> _crew;
        private ObservableCollection<XElement> _tradeQueue;
        private ObservableCollection<XElement> _constructionWares;
        private ObservableCollection<XElement> _drones;
        private XElement _gravidar;
        private XElement _fuelCargo;
        private XElement _bulkCargo;
        private XElement _containerCargo;
        private XElement _energyCargo;
        private XElement _liquidCargo;
        private XElement _universalCargo;

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
        public ObservableCollection<XElement> TradeQueue
        {
            get
            {
                return this._tradeQueue;
            }
            set
            {
                this._tradeQueue = value;
                this.OnPropertyChanged("TradeQueue");
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
        public ObservableCollection<XElement> Drones
        {
            get
            {
                return this._drones;
            }
            set
            {
                this._drones = value;
                this.OnPropertyChanged("Drones");
            }
        }
        public XElement Gravidar
        {
            get
            {
                return this._gravidar;
            }
            set
            {
                this._gravidar = value;
                this.OnPropertyChanged("Gravidar");
            }
        }
        public XElement FuelCargo
        {
            get
            {
                return this._fuelCargo;
            }
            set
            {
                this._fuelCargo = value;
                this.OnPropertyChanged("FuelCargo");
            }
        }
        public XElement BulkCargo
        {
            get
            {
                return this._bulkCargo;
            }
            set
            {
                this._bulkCargo = value;
                this.OnPropertyChanged("BulkCargo");
            }
        }
        public XElement ContainerCargo
        {
            get
            {
                return this._containerCargo;
            }
            set
            {
                this._containerCargo = value;
                this.OnPropertyChanged("ContainerCargo");
            }
        }
        public XElement EnergyCargo
        {
            get
            {
                return this._energyCargo;
            }
            set
            {
                this._energyCargo = value;
                this.OnPropertyChanged("EnergyCargo");
            }
        }
        public XElement LiquidCargo
        {
            get
            {
                return this._liquidCargo;
            }
            set
            {
                this._liquidCargo = value;
                this.OnPropertyChanged("LiquidCargo");
            }
        }
        public XElement UniversalCargo
        {
            get
            {
                return this._universalCargo;
            }
            set
            {
                this._universalCargo = value;
                this.OnPropertyChanged("UniversalCargo");
            }
        }

        public Ship(XElement ship)
            : base(ship)
        {
            this._crew = new ObservableCollection<CMember>();
            this._tradeQueue = new ObservableCollection<XElement>();
            this._constructionWares = new ObservableCollection<XElement>();
            this._drones = new ObservableCollection<XElement>();

            if (ship.Attribute("name") == null)
            {
                EntityValueConverter conv = new EntityValueConverter();
                XAttribute name = new XAttribute("name", conv.Convert(ship.Attribute("macro").Value, null, null, null));
                ship.Add(name);
            }

            IEnumerable<XElement> crewquery =
            from crew in this.Data.Descendants().Elements("component")
            where (string) crew.Attribute("class") == "npc"
            select crew;

            foreach (XElement person in crewquery)
                Crew.Add(new CMember(person));

            foreach (XElement trade in this.Data.Elements("trade").Elements("shopping").Elements("trade"))
                TradeQueue.Add(trade);

            IEnumerable<XElement> buildmodulesQuery =
            from buildmodules in this.Data.Descendants().Elements("connection")
            where (string) buildmodules.Attribute("macro") == "connection_buildmodule01"
            select buildmodules;

            IEnumerable<XElement> constructionWaresQuery =
            from wares in buildmodulesQuery.Descendants<XElement>().Elements("resources").Elements("ware")
            select wares;

            foreach (XElement ware in constructionWaresQuery)
                ConstructionWares.Add(ware);

            foreach (XElement drone in this.Data.Elements("ammunition").Elements("available").Elements("item"))
                Drones.Add(drone);


            IEnumerable<XElement> cargoList =
            from cargo in this.Data.Descendants().Elements("component")
            where (string) cargo.Attribute("class") == "storage" & (string) cargo.Attribute("macro") != "unit_player_ship_storage_macro"
            select cargo;

            foreach (XElement cargoType in cargoList)
            {
                string cargoTypeName = cargoType.Attribute("macro").Value;
                switch (cargoTypeName)
                {
                    case "storage_ship_l_bulk_01_macro":
                    case "storage_ship_m_bulk_01_macro":
                    case "storage_ship_l_bulk_02_macro":
                    case "storage_ship_l_bulk_03_macro":
                    case "storage_ship_l_bulk_04_macro":
                    case "storage_ship_xl_bulk_01_macro":
                        BulkCargo = cargoType;
                        break;
                    case "storage_ship_l_container_01_macro":
                    case "storage_ship_l_container_02_macro":
                    case "storage_ship_m_container_01_macro":
                    case "storage_ship_xl_container_01_macro":
                        ContainerCargo = cargoType;
                        break;
                    case "storage_ship_l_energy_01_macro":
                    case "storage_ship_l_energy_02_macro":
                    case "storage_ship_xl_energy_01_macro":
                    case "storage_ship_m_energy_01_macro":
                        EnergyCargo = cargoType;
                        break;
                    case "storage_ship_l_fuel_01_macro":
                    case "storage_ship_xl_fuel_01_macro":
                        FuelCargo = cargoType;
                        break;
                    case "storage_ship_l_liquid_01_macro":
                    case "storage_ship_l_liquid_02_macro":
                    case "storage_ship_l_liquid_03_macro":
                    case "storage_ship_xl_liquid_01_macro":
                    case "storage_ship_m_liquid_01_macro":
                        LiquidCargo = cargoType;
                        break;
                    case "storage_ship_xl_universal_01_macro":
                    case "storage_ship_xs_universal_01_macro":
                    case "storage_temp_huge_macro":
                        UniversalCargo = cargoType;
                        break;
                }
            }

            Gravidar = this.Data.Element("gravidar");

            LoadDetails();
        }

        public void LoadDetails()
        {
            Details = String.Format("{0}\nId:{1}\nCrew #:{2}", this.Data.Attribute("name").Value, this.Data.Attribute("id").Value, Crew.Count);

            if (this.Data.Element("hull") != null)
                Details += "\nHull damaged";
        }

        public void removeCargo(XElement cargo)
        {
            if (cargo != null)
            {
                cargo.Remove();
            }
        }

        public void removeTrade(XElement trade)
        {
            if (TradeQueue.Contains(trade))
            {
                TradeQueue.Remove(trade);
                trade.Remove();
            }
        }

        private XElement checkCargoTags(XElement cargoconnection)
        {
            XElement cargoTag = cargoconnection.Element("cargo");

            if (cargoTag == null)
            {
                cargoTag = new XElement("cargo", new XElement("summary", new object[]
				{
					new XAttribute("state", "collapsed"),
					new XAttribute("connection", "cargo")
				}));
                cargoconnection.AddFirst(cargoTag);
            }
            else if (cargoconnection.Element("cargo").Element("summary") == null)
            {
                XElement summaryTag = new XElement("summary", new object[]
					{
						new XAttribute("state", "collapsed"),
						new XAttribute("connection", "cargo")
					});
                cargoconnection.Element("cargo").AddFirst(summaryTag);
            }

            return cargoconnection;
        }

        public void addCargo(string cargoType)
        {
            XElement content = null;
            switch (cargoType)
            {
                case "FuelCargo":
                    content = new XElement("ware", new object[]
				    {
					    new XAttribute("ware", "fuelcells"),
					    new XAttribute("amount", "0")
				    });
                    checkCargoTags(FuelCargo).Element("cargo").Element("summary").Add(content);
                    break;

                case "UniversalCargo":
                    content = new XElement("ware", new object[]
					{
						new XAttribute("ware", "antimattercells"),
						new XAttribute("amount", "0")
					});
                    this.checkCargoTags(UniversalCargo).Element("cargo").Element("summary").Add(content);
                    break;
                case "BulkCargo":
                    content = new XElement("ware", new object[]
					{
						new XAttribute("ware", "crystals"),
						new XAttribute("amount", "0")
					});
                    checkCargoTags(BulkCargo).Element("cargo").Element("summary").Add(content);
                    break;

                case "ContainerCargo":
                    content = new XElement("ware", new object[]
					{
                    	new XAttribute("ware", "bioelectricneurongel"),
						new XAttribute("amount", "0")
					});
                    checkCargoTags(ContainerCargo).Element("cargo").Element("summary").Add(content);
                    break;

                case "EnergyCargo":
                    content = new XElement("ware", new object[]
					{
	                    new XAttribute("ware", "antimattercells"),
						new XAttribute("amount", "0")
					});
                    checkCargoTags(EnergyCargo).Element("cargo").Element("summary").Add(content);
                    break;
                case "LiquidCargo":
                    content = new XElement("ware", new object[]
					{

						new XAttribute("ware", "hydrogen"),
						new XAttribute("amount", "0")
					});
                    checkCargoTags(LiquidCargo).Element("cargo").Element("summary").Add(content);
                    break;
            }
        }

        public void repairComponents()
        {
            IEnumerable<XAttribute> damagedParts = this.Data.Descendants().Attributes("state").Where(delegate(XAttribute parts)
            {
                return (parts != null && (parts.Value.ToString().Contains("wrecked") || parts.Value.ToString().Contains("wreck")));
            }
            );

            bool repaired = false;
            foreach (XAttribute part in damagedParts)
            {
                part.Remove();
                repaired = true;
            }


            IEnumerable<XElement> damagedHull = this.Data.Descendants().Elements("hull");
            foreach (XElement hull in damagedHull)
            {
                hull.Remove();
                repaired = true;
            }

            if (repaired)
                MessageBox.Show("Components Repaired");
        }

        public void repairHull()
        {
            XElement hull = this.Data.Element("hull");
            if (hull != null)
            {
                hull.Remove();
                LoadDetails();
                MessageBox.Show("Hull Repaired");
            }
        }

        public void addCrewAttributeNode(string attribute, CMember crewMember)
        {
            if (Crew.Contains(crewMember))
            {
                crewMember.addAttributeNode(attribute);
            }
        }

        public void addDrone()
        {
            this.addDrone("units_size_xs_transp_empty_macro", "0");
        }

        public void addDrone(string macro, string amount)
        {
            IEnumerable<XElement> ammo = this.Data.Elements("ammunition");
            XElement droneAmmo;
            if (ammo.Count<XElement>() == 0)
            {
                droneAmmo = new XElement("ammunition", new XElement("available"));
                //  check after which tag to add the drone Ammo
                XElement tag = this.Data.Element("survace");
                if (tag == null)
                {
                    tag = this.Data.Element("turret");
                    if (tag == null)
                    {
                        tag = this.Data.Element("shields");
                    }
                }

                if (tag == null)
                {
                    tag = (XElement) this.Data.FirstNode;
                }
                tag.AddAfterSelf(droneAmmo);
            }
            else
            {
                droneAmmo = ammo.First<XElement>();
            }

            XElement newDrone = new XElement("item", new object[]
			{
				new XAttribute("macro", macro),
				new XAttribute("amount", amount)
			});

            droneAmmo.Elements("available").First<XElement>().Add(newDrone);
            this.Drones.Add(newDrone);
        }

        public void removeDrone(XElement drone)
        {
            if (this.Drones.Contains(drone))
            {
                this.Drones.Remove(drone);
                drone.Remove();
            }
        }

        public void addGravidar()
        {
            if (this.Gravidar == null)
            {
                this.Gravidar = new XElement("gravidar");
                this.Data.Add(this.Gravidar);
                MessageBox.Show("Gravidar successfully added to ship");
            }
            else
            {
                MessageBox.Show("Ship has a gravidar");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Linq;

namespace XRebirthSaveEditorC
{
    public class Component : INotifyPropertyChanged
    {

        private XElement _data;
        private string _image;
        public event PropertyChangedEventHandler PropertyChanged;


        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChangedEvent = this.PropertyChanged;
            if (propertyChangedEvent != null)
            {
                propertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Component(XElement component)
        {
            Data = component;
            Image = getImage(component.Attribute("macro").Value);
        }

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

        public string Image
        {
            get
            {
                return this._image;
            }
            set
            {
                this._image = value;
                this.OnPropertyChanged("Image");
            }
        }

        private string getImage(string type)
        {
            switch (type)
            {
                case "units_size_xs_wardrone_macro Moth":
                    return "Images\\XS\\AssaultURV.jpg";
                case "units_size_xs_boarding_ship_macro":
                    return "Images\\XS\\BoardingPod.jpg";
                case "units_size_xs_albion_transport_1_macro":
                case "units_size_xs_albion_transport_2_macro":
                case "units_size_xs_albion_personal_transporter_macro":
                case "units_size_xs_devris_personal_transporter_macro":
                case "units_size_xs_devris_transport_01_macro":
                case "units_size_xs_devris_transport_02_macro":
                    return "Images\\XS\\BulkCarrier.jpg";
                case "units_size_xs_transp_empty_macro":
                    return "Images\\XS\\CargolifterURV.jpg";
                case "units_size_xs_welder_drone_macro":
                    return "Images\\XS\\ConstructionURV.jpg";
                case "units_size_xs_albion_pv_1_blue_macro":
                case "units_size_xs_albion_pv_1_grey_macro":
                case "units_size_xs_albion_pv_1_red_macro":
                case "units_size_xs_albion_pv_1_yellow_macro":
                case "units_size_xs_albion_pv_2_blue_macro":
                case "units_size_xs_devris_pv_01_brown_macro":
                    return "Images\\XS\\ConsumerCompactCraft.jpg";
                case "units_size_xs_albion_police_transport_macro":
                case "units_size_xs_devris_police_transport_macro":
                    return "Images\\XS\\CrewEquipmentCarrier.jpg";
                case "unit_size_xs_escapepod_macro":
                    return "Images\\XS\\EscapePod.jpg";
                case "units_size_xs_albion_police_car_macro":
                case "units_size_xs_devris_police_car_macro":
                    return "Images\\XS\\IndustrialSurveillanceCraft.jpg";
                case "units_size_xs_albion_construction_macro":
                case "units_size_xs_albion_mining_macro":
                case "units_size_xs_devris_construction_macro":
                    return "Images\\XS\\MaintenanceEngineeringCraft.jpg";
                case "units_size_xs_xenon_drone_1_macro":
                case "units_size_xs_xenon_drone_2_macro":
                case "units_size_xs_xenon_drone_3_macro":
                case "units_size_xs_xenon_drone_4_macro":
                case "units_size_xs_xenon_drone_5_macro":
                    return "Images\\XS\\TFXUtilityCraft.jpg";
                case "units_size_xs_albion_police_apc_macro":
                case "units_size_xs_devris_police_apc_macro":
                    return "Images\\XS\\TrafficRiotControlCraft.jpg";

                //Size S
                case "units_size_s_ship_04_macro":
                    return "Images\\S\\Artio.jpg";
                case "units_size_s_ship_23_macro":
                    return "Images\\S\\Birog.jpg";
                case "units_size_s_split_m3_macro":
                    return "Images\\S\\Bonescout.jpg";
                case "units_size_s_ship_ar_military_01_macro":
                    return "Images\\S\\CamulosRaider.jpg";
                case "units_size_s_ship_ar_military_03_macro":
                    return "Images\\S\\CamulosSentinel.jpg";
                case "units_size_s_ship_ar_military_02_macro":
                    return "Images\\S\\CamulosVanguard.jpg";
                case "units_size_s_ship_pirate_02_macro":
                    return "Images\\S\\Cennelath.jpg";
                case "units_size_s_ship_pirate_01_macro":
                    return "Images\\S\\Domelch.jpg";
                case "units_size_s_torpedo_bomber_macro":
                    return "Images\\S\\Drostan.jpg";
                case "units_size_s_ship_ar_military_07_macro":
                    return "Images\\S\\EterscelRaider.jpg";
                case "units_size_s_ship_ar_military_09_macro":
                    return "Images\\S\\EterscelSentinel.jpg";
                case "units_size_s_ship_ar_military_08_macro":
                    return "Images\\S\\EterscelVanguard.jpg";
                case "units_size_s_ship_ar_military_04_macro":
                    return "Images\\S\\FoltorRaider.jpg";
                case "units_size_s_ship_ar_military_06_macro":
                    return "Images\\S\\FoltorSentinel.jpg";
                case "units_size_s_ship_ar_military_05_macro":
                    return "Images\\S\\FoltorVanguard.jpg";
                case "units_size_s_canteran_fighter_01_macro":
                    return "Images\\S\\Hayabusa.jpg";
                case "units_size_s_ship_19_macro":
                    return "Images\\S\\Hesus.jpg";
                case "units_size_s_xenon_swarm_attack_drone_01_macro":
                    return "Images\\S\\M.jpg";
                case "units_size_s_ship_pirate_03_macro":
                    return "Images\\S\\Maelchon.jpg";
                case "units_size_s_pmc_xen_01_macro":
                    return "Images\\S\\Moebius.jpg";
                case "units_size_s_xenon_swarm_attack_drone_02_macro":
                    return "Images\\S\\N.jpg";
                case "units_size_s_ship_07_macro":
                    return "Images\\S\\Nechtan.jpg";
                case "units_size_s_ship_01_macro":
                    return "Images\\S\\Orlam.jpg";
                case "units_size_s_ship_03_macro":
                    return "Images\\S\\Ossian.jpg";
                case "units_size_s_split_m4_macro":
                    return "Images\\S\\SkullCrusher.jpg";
                case "units_size_s_ship_02_macro":
                    return "Images\\S\\Talorcan.jpg";
                case "units_size_s_ship_pmc_01_macro":
                    return "Images\\S\\TriathRaider.jpg";
                case "units_size_s_ship_pmc_03_macro":
                    return "Images\\S\\TriathSentinel.jpg";
                case "units_size_s_ship_pmc_02_macro":
                    return "Images\\S\\TriathVanguard.jpg";
                case "units_size_s_ship_05_macro":
                    return "Images\\S\\Vasio.jpg";

                //Size L
                case "units_size_l_single_attack_ship_macro":
                    return "Images\\L\\Balor.jpg";
                case "units_size_l_hydrogen_collector_macro":
                    return "Images\\L\\Boann.jpg";
                case "units_size_l_crystal_collector_macro":
                case "units_size_l_ore_collector_macro":
                case "units_size_l_nividium_collector_macro":
                    return "Images\\L\\Fedhelm.jpg";
                case "units_size_l_kit_carrier_02_macro":
                    return "Images\\L\\HeavySul.jpg";
                case "units_size_l_liquid_freighter_macro":
                    return "Images\\L\\Hermod.jpg";
                case "units_size_l_xenon_01_macro":
                    return "Images\\L\\K.jpg";
                case "units_size_l_canteran_transporter_macro":
                    return "Images\\L\\Lepton.jpg";
                case "units_size_l_kit_carrier_01_macro":
                    return "Images\\L\\LightSul.jpg";
                case "units_size_l_ions_collector_macro":
                case "units_size_l_plasma_collector_macro":
                    return "Images\\L\\Midir.jpg";
                case "units_size_l_kit_bulk_01_macro":
                    return "Images\\L\\RahanasBulk.jpg";
                case "units_size_l_kit_container_01_macro":
                case "units_size_l_kit_container_02_macro":
                case "units_size_l_kit_hybrid_01_macro":
                case "units_size_l_kit_hybrid_02_macro":
                    return "Images\\L\\RahanasContainer.jpg";
                case "units_size_l_kit_energy_01_macro":
                    return "Images\\L\\RahanasEnergy.jpg";
                case "units_size_l_kit_liquid_01_macro":
                    return "Images\\L\\RahanasLiquid.jpg";
                case "units_size_l_ice_collector_macro":
                    return "Images\\L\\Sequana.jpg";

                //Size XL
                case "units_size_xl_builder_ship_dv_macro":
                case "units_size_xl_builder_ship_macro":
                case "units_size_xl_builder_ship_ol_macro":
                case "units_size_xl_builder_ship_plot_01_macro":
                    return "Images\\XL\\ConstructionVessel.jpg";
                case "units_size_xl_transporter_1_macro":
                case "units_size_xl_transporter_2_macro":
                case "units_size_xl_transporter_3_macro":
                    return "Images\\XL\\SuperFreighter.jpg";
                case "units_size_xl_capital_destroyer_1_macro":
                    return "Images\\XL\\Arawn.jpg";
                case "units_size_xl_capital_destroyer_2_macro":
                    return "Images\\XL\\Taranis.jpg";
                case "units_size_xl_split_m1_macro":
                    return "Images\\XL\\GangreneChaser.jpg";
                case "units_size_xl_cargo_hauler_3_macro":
                    return "Images\\XL\\Scaldis.jpg";
                case "units_size_xl_red_destroyer_macro":
                    return "Images\\XL\\Sucellus.jpg";
                case "units_size_xl_cargo_hauler_2_macro":
                    return "Images\\XL\\Titurel.jpg";
                case "unit_player_ship_macro":
                    return "Images\\PL\\Skunk.jpg";
                default:
                    return "";
            }
        }
    }
}

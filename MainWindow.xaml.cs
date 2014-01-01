using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Xml.Linq;

namespace XRebirthSaveEditorC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Savegame saveXML;
        public MainWindow()
        {
            InitializeComponent();

            string path = AppDomain.CurrentDomain.BaseDirectory + "ComboBoxValues.xml";
            if (File.Exists(path))
            {
                XmlDataProvider x = Resources["comboBoxValues"] as XmlDataProvider;
                x.Source = new Uri(path);
            }
            else
            {
                MessageBox.Show("System could not find ComboBoxValues.xml!", "Warning", MessageBoxButton.OK);
            }
            try
            {
                /*
				if (!this.dotNetCheck())
				{
					Interaction.MsgBox("You need .NET Version 4.5 or greater to run this Application properly. Please update to the newest version.", MsgBoxStyle.OkOnly, null);
				}*/
            }
            catch (Exception)
            {
            }
        }

        private void MenuItemLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "*.xml";
            openFileDialog.Filter = "eXtensible Markup Language file (.xml)|*.xml";
            bool flag = (openFileDialog.ShowDialog() ?? false);
            if (flag)
            {
                this.saveXML = new Savegame(openFileDialog.FileName);
                this.ParentGrid.DataContext = this.saveXML;
                this.SaveMenuItem.IsEnabled = true;
                this.ModMenuItem.IsEnabled = true;
                this.MainTabControl.IsEnabled = true;
            }
        }

        private void ButtonRemoveMod_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.removeMod((XElement) this.ModListBox.SelectedItem);
        }
        private void ButtonRepairShip_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.repairShip((Ship) this.PlayerShipListBox.SelectedItem);
        }
        private void ButtonRemoveCargo_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.removeCargo((Ship) this.PlayerShipListBox.SelectedItem, (XElement) this.BulkCargoListBox.SelectedItem);
        }
        private void ButtonAddFuelCargo_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.addCargo((Ship) this.PlayerShipListBox.SelectedItem, "FuelCargo");
        }
        private void ButtonAddUniversalCargo_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.addCargo((Ship) this.PlayerShipListBox.SelectedItem, "UniversalCargo");
        }
        private void ButtonAddBulkCargo_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.addCargo((Ship) this.PlayerShipListBox.SelectedItem, "BulkCargo");
        }
        private void ButtonAddContainerCargo_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.addCargo((Ship) this.PlayerShipListBox.SelectedItem, "ContainerCargo");
        }
        private void ButtonAddEnergyCargo_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.addCargo((Ship) this.PlayerShipListBox.SelectedItem, "EnergyCargo");
        }
        private void ButtonAddLiquidCargo_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.addCargo((Ship) this.PlayerShipListBox.SelectedItem, "LiquidCargo");
        }
        private void ButtonRemoveFuelCargo_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.removeCargo((Ship) this.PlayerShipListBox.SelectedItem, (XElement) this.FuelCargoListBox.SelectedItem);
        }
        private void ButtonRemoveUniversalCargo_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.removeCargo((Ship) this.PlayerShipListBox.SelectedItem, (XElement) this.UniversalCargoListBox.SelectedItem);
        }
        private void ButtonRemoveBulkCargo_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.removeCargo((Ship) this.PlayerShipListBox.SelectedItem, (XElement) this.BulkCargoListBox.SelectedItem);
        }
        private void ButtonRemoveContainerCargo_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.removeCargo((Ship) this.PlayerShipListBox.SelectedItem, (XElement) this.ContainerCargoListBox.SelectedItem);
        }
        private void ButtonRemoveEnergyCargo_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.removeCargo((Ship) this.PlayerShipListBox.SelectedItem, (XElement) this.EnergyCargoListBox.SelectedItem);
        }
        private void ButtonRemoveLiquidCargo_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.removeCargo((Ship) this.PlayerShipListBox.SelectedItem, (XElement) this.LiquidCargoListBox.SelectedItem);
        }
        private void ButtonRemoveTrade_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.removeTrade((Ship) this.PlayerShipListBox.SelectedItem, (XElement) this.ShipTradeListBox.SelectedItem);
        }
        private void ButtonFinishShip_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.finishShip((Shipyard) this.ShipyardListBox.SelectedItem, (Ship) this.shipyardShipsListBox.SelectedItem);
        }

        private void MenuItemSave_Click(object sender, RoutedEventArgs e)
        {
            this.PlayerNameTextBox.Focusable = true;
            this.PlayerNameTextBox.Focus();
            this.saveXML.save();
            MessageBox.Show("File saved successfully");
        }
        private void ButtonDeleteShip_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Really Delete Ship?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {
                this.saveXML.deleteShip((Ship) this.PlayerShipListBox.SelectedItem);
            }
        }
        private void MenuItemMod_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.deleteAllMods();
        }
        private void PlayerMoneyTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.saveXML.adjustMoney(PlayerMoneyTextBox.Text);
        }
        private void ButtonAddBoarding_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.addCrewAttributeNode("boarding", (Ship) this.PlayerShipListBox.SelectedItem, (CMember) this.ShipCrewListBox.SelectedItem);
        }
        private void ButtonAddCombat_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.addCrewAttributeNode("combat", (Ship) this.PlayerShipListBox.SelectedItem, (CMember) this.ShipCrewListBox.SelectedItem);
        }
        private void ButtonAddEngineering_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.addCrewAttributeNode("engineering", (Ship) this.PlayerShipListBox.SelectedItem, (CMember) this.ShipCrewListBox.SelectedItem);
        }
        private void ButtonAddNavigation_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.addCrewAttributeNode("navigation", (Ship) this.PlayerShipListBox.SelectedItem, (CMember) this.ShipCrewListBox.SelectedItem);
        }
        private void ButtonAddLeadership_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.addCrewAttributeNode("leadership", (Ship) this.PlayerShipListBox.SelectedItem, (CMember) this.ShipCrewListBox.SelectedItem);
        }
        private void ButtonAddMorale_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.addCrewAttributeNode("morale", (Ship) this.PlayerShipListBox.SelectedItem, (CMember) this.ShipCrewListBox.SelectedItem);
        }
        private void ButtonAddScience_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.addCrewAttributeNode("science", (Ship) this.PlayerShipListBox.SelectedItem, (CMember) this.ShipCrewListBox.SelectedItem);
        }
        private void ButtonAddManagement_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.addCrewAttributeNode("management", (Ship) this.PlayerShipListBox.SelectedItem, (CMember) this.ShipCrewListBox.SelectedItem);
        }
        private void ButtonAddBoardingStation_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.addCrewAttributeStationNode("boarding", (Station) this.StationListBox.SelectedItem, (CMember) this.StationCrewListBox.SelectedItem);
        }
        private void ButtonAddCombatStation_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.addCrewAttributeStationNode("combat", (Station) this.StationListBox.SelectedItem, (CMember) this.StationCrewListBox.SelectedItem);
        }
        private void ButtonAddEngineeringStation_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.addCrewAttributeStationNode("engineering", (Station) this.StationListBox.SelectedItem, (CMember) this.StationCrewListBox.SelectedItem);
        }
        private void ButtonAddNavigationStation_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.addCrewAttributeStationNode("navigation", (Station) this.StationListBox.SelectedItem, (CMember) this.StationCrewListBox.SelectedItem);
        }
        private void ButtonAddLeadershipStation_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.addCrewAttributeStationNode("leadership", (Station) this.StationListBox.SelectedItem, (CMember) this.StationCrewListBox.SelectedItem);
        }
        private void ButtonAddMoraleStation_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.addCrewAttributeStationNode("morale", (Station) this.StationListBox.SelectedItem, (CMember) this.StationCrewListBox.SelectedItem);
        }
        private void ButtonAddScienceStation_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.addCrewAttributeStationNode("science", (Station) this.StationListBox.SelectedItem, (CMember) this.StationCrewListBox.SelectedItem);
        }
        private void ButtonAddManagementStation_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.addCrewAttributeStationNode("management", (Station) this.StationListBox.SelectedItem, (CMember) this.StationCrewListBox.SelectedItem);
        }
        private void ButtonAddDroneShip_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.addDroneShip((Ship) this.PlayerShipListBox.SelectedItem);
        }
        private void ButtonRemoveDroneShip_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.removeDroneShip((Ship) this.PlayerShipListBox.SelectedItem, (XElement) this.ShipDronesListBox.SelectedItem);
        }
        private void ButtonAddGravidarShip_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.addGravidar((Ship) this.PlayerShipListBox.SelectedItem);
        }
        private void ButtonFinishBuild_Click(object sender, RoutedEventArgs e)
        {
            this.saveXML.finishBuild((Ship) this.PlayerShipListBox.SelectedItem);
        }
    }
}

﻿using System.Collections.Generic;
using System.Windows.Forms;
using Watchdog.Persistence;
using Watchdog.Entities;
using System;
using static System.Windows.Forms.DataGridView;

namespace Watchdog.Forms
{
    public partial class UserControlAllowedACAndCurrencies : UserControl, PassedForm
    {
        private int currentRowIndex;
        private DataGridView currentDataGridView;

        public UserControlAllowedACAndCurrencies()
        {
            InitializeComponent();
            LoadAssetClasses();
            LoadCurrencies();
        }

        private void LoadCurrencies()
        {
            TableUtility tableUtility = new TableUtility(Globals.WatchdogAddIn.Application.ActiveWorkbook);
            List<Currency> currencyList = tableUtility.ConvertRangeToCurrency(tableUtility.ReadAllRows(Currency.GetDefaultValue().GetTableName()));
            foreach (Currency currency in currencyList)
            {
                currencyBindingSource.Add(currency);
            }
        }

        private void LoadAssetClasses()
        {
            TableUtility tableUtility = new TableUtility(Globals.WatchdogAddIn.Application.ActiveWorkbook);
            List<AssetClass> assetClassList = tableUtility.ConvertRangeToAssetClass(tableUtility.ReadAllRows(AssetClass.GetDefaultValue().GetTableName()));
            foreach (AssetClass assetClass in assetClassList)
            {
                assetClassBindingSource.Add(assetClass);
            }
        }

        private void AddAssetClass(string assetClassName)
        {
            TableUtility tableUtility = new TableUtility(Globals.WatchdogAddIn.Application.ActiveWorkbook);
            tableUtility.CreateMissingTable(AssetClass.GetDefaultValue());
            AssetClass assetClass = new AssetClass
            {
                Name = assetClassName
            };
            tableUtility.InsertTableRow(assetClass, new List<string>()
            {
                assetClass.Name
            });
            assetClassBindingSource.Add(assetClass);
        }

        private void AddCurrency(string currencyIsoCode)
        {
            TableUtility tableUtility = new TableUtility(Globals.WatchdogAddIn.Application.ActiveWorkbook);
            tableUtility.CreateMissingTable(Currency.GetDefaultValue());
            Currency currency = new Currency
            {
                IsoCode = currencyIsoCode
            };
            tableUtility.InsertTableRow(currency, new List<string>()
            {
                currency.IsoCode
            });
            currencyBindingSource.Add(currency);
        }

        private void ButtonNewAssetClass_Click(object sender, EventArgs e)
        {
            _ = new OneAttributeForm(this, "Asset-Klasse", "asset_class")
            {
                Visible = true
            };
        }

        public void OnSubmit(string passedValue, string reference)
        {
            switch (reference)
            {
                case "asset_class":
                    AddAssetClass(passedValue);
                    break;

                case "currency":
                    AddCurrency(passedValue);
                    break;
            }
        }

        private void ButtonNewCurrency_Click(object sender, EventArgs e)
        {
            _ = new OneAttributeForm(this, "Währung", "currency")
            {
                Visible = true
            };
        }

        private void DeleteClick(object sender, EventArgs e)
        {
            TableUtility tableUtility = new TableUtility(Globals.WatchdogAddIn.Application.ActiveWorkbook);
            Persistable objectToDelete = currentDataGridView.Rows[currentRowIndex].DataBoundItem as Persistable;
            currentDataGridView.Rows.RemoveAt(currentRowIndex);
            tableUtility.DeleteTableRow(objectToDelete.GetTableName(), objectToDelete.GetIndex());
        }

        private void AssetClassMouseDown(object sender, MouseEventArgs e)
        {
            DataGridViewMouseDown(sender as DataGridView, e);
        }

        private void CurrencyMouseDown(object sender, MouseEventArgs e)
        {
            DataGridViewMouseDown(sender as DataGridView, e);
        }

        private void DataGridViewMouseDown(DataGridView sender, MouseEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if (e.Button == MouseButtons.Right)
            {
                HitTestInfo hitTest = dataGridView.HitTest(e.X, e.Y);
                dataGridView.ClearSelection();
                if (hitTest.RowIndex < 0)
                {
                    MessageBox.Show("Falsche Zeile ausgewählt.");
                    return;
                }
                dataGridView.Rows[hitTest.RowIndex].Selected = true;
                currentRowIndex = hitTest.RowIndex;
                currentDataGridView = dataGridView;
            }
        }
    }
}
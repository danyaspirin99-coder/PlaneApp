using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PlaneApp
{
    public partial class MainWindow : Window
    {
        private List<TextBox> _modelBoxes = new();
        private List<TextBox> _rangeBoxes = new();
        private List<TextBox> _speedBoxes = new();
        private PlaneControl? _control;

        public MainWindow() => InitializeComponent();

        private void BtnSetCount_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(TxtCount.Text, out int count) && count > 0)
            {
                InputsPanel.Children.Clear();
                _modelBoxes.Clear();
                _rangeBoxes.Clear();
                _speedBoxes.Clear();

                for (int i = 0; i < count; i++)
                {
                    var row = new Grid
                    {
                        Margin = new Thickness(0, 5, 0, 0)
                    };
                    row.ColumnDefinitions.Add(new ColumnDefinition());
                    row.ColumnDefinitions.Add(new ColumnDefinition());
                    row.ColumnDefinitions.Add(new ColumnDefinition());

                    var tbModel = new TextBox { Margin = new Thickness(5) };
                    Grid.SetColumn(tbModel, 0);
                    row.Children.Add(tbModel);
                    _modelBoxes.Add(tbModel);

                    var tbRange = new TextBox { Margin = new Thickness(5) };
                    Grid.SetColumn(tbRange, 1);
                    row.Children.Add(tbRange);
                    _rangeBoxes.Add(tbRange);

                    var tbSpeed = new TextBox { Margin = new Thickness(5) };
                    Grid.SetColumn(tbSpeed, 2);
                    row.Children.Add(tbSpeed);
                    _speedBoxes.Add(tbSpeed);

                    InputsPanel.Children.Add(row);
                }

                InputPanel.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Введите корректное положительное число.");
            }
        }

        private void BtnEnterData_Click(object sender, RoutedEventArgs e)
        {
            var planes = new List<Plane>();
            for (int i = 0; i < _modelBoxes.Count; i++)
            {
                if (!double.TryParse(_rangeBoxes[i].Text, out double range) ||
                    !double.TryParse(_speedBoxes[i].Text, out double speed))
                {
                    MessageBox.Show($"Ошибка в данных для самолёта №{i + 1}. Проверьте числа.");
                    return;
                }

                planes.Add(new Plane
                {
                    Model = _modelBoxes[i].Text.Trim(),
                    MaxRange = range,
                    CruiseSpeed = speed
                });
            }

            _control = new PlaneControl(planes.ToArray());
            LbUnsorted.ItemsSource = planes;
            LbSorted.ItemsSource = null;
        }

        private void BtnSort_Click(object sender, RoutedEventArgs e)
        {
            if (_control == null)
            {
                MessageBox.Show("Сначала введите данные.");
                return;
            }

            _control.SortByRangeAndSpeed();
            var sorted = _control.GetPlanes();
            LbSorted.ItemsSource = sorted;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

            if (_control == null)
            {
                MessageBox.Show("Нет данных для сохранения.");
                return;
            }

            var dlg = new SaveFileDialog();
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    _control.SaveToFile(dlg.FileName);
                    MessageBox.Show("Файл сохранен успешно");
                }
                catch (Exception ex) 
                {
                    MessageBox.Show("Ошибка сохранения файла");
                }
            }
        }
    }
}
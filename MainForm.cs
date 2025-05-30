using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PR10
{
    public partial class MainForm : Form
    {
        public static NpgsqlConnection conn;
        NpgsqlDataAdapter adapter;
        DataTable dataTable;
        string currentTable;
        Dictionary<string, Dictionary<int, string>> lookupData = new Dictionary<string, Dictionary<int, string>>();

        Dictionary<string, Dictionary<string, string>> foreignKeysMap = new Dictionary<string, Dictionary<string, string>>
        {
            ["перевозки"] = new Dictionary<string, string>
            {
                ["договор"] = "договоры",
                ["марка"] = "марки",
                ["сотрудник"] = "сотрудники"
            },
            ["марки"] = new Dictionary<string, string>
            {
                ["водитель"] = "водители"
            }
        };

        public MainForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximumSize = this.Size;
            conn = new NpgsqlConnection("Host=localhost;Port=5432;Database=YP_DB;Username=postgres;Password=4825;");
            conn.Open();

            comboBox1.Items.AddRange(new string[] { "перевозки", "договоры", "марки", "сотрудники", "водители" });
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentTable = comboBox1.Text;
            LoadLookupTables();

            adapter = new NpgsqlDataAdapter($"select * from {currentTable};", conn);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = dataTable;
            dataGridView1.Columns["ин"].Visible = false;

            ReplaceForeignKeyColumns();

            dataGridView1.DefaultValuesNeeded -= dataGridView1_DefaultValuesNeeded;
            dataGridView1.DefaultValuesNeeded += dataGridView1_DefaultValuesNeeded;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                NpgsqlCommandBuilder npgsqlCommandBuilder = new NpgsqlCommandBuilder(adapter);
                adapter.Update(dataTable);
                MessageBox.Show("Изменения сохранены.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}");
            }
        }

        private string GetValueColumnForTable(string table)
        {
            switch (table) {
                case "договоры": return "фио_клиента";
                case "марки": return "название";
                case "водители": return "фио";
                case "сотрудники": return "фио";
                default: return "ин";
            }

        }

        private void LoadLookupTables()
        {
            lookupData.Clear();

            var requiredLookups = foreignKeysMap.Values
                .SelectMany(d => d.Values)
                .Distinct()
                .ToList();

            foreach (var table in requiredLookups)
            {
                string keyColumn = "ин";
                string valueColumn = GetValueColumnForTable(table);
                lookupData[table] = LoadLookup(table, keyColumn, valueColumn);
            }
        }

        private Dictionary<int, string> LoadLookup(string table, string key, string column)
        {
            var dict = new Dictionary<int, string>();
            try
            {
                using (var cmd = new NpgsqlCommand($"select {key}, {column} from {table};", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dict.Add(reader.GetInt32(0), reader.IsDBNull(1) ? "NULL" : reader.GetString(1));
                        }
                    }
                }
            }
            catch (Exception ex){
                MessageBox.Show("Ошибка загрузки данных: " + ex.Message);
            }
            return dict;
        }

        private void ReplaceForeignKeyColumns()
        {
            if (!foreignKeysMap.TryGetValue(currentTable, out var fkColumns))
                return;

            foreach (var fk in fkColumns)
            {
                string columnName = fk.Key;
                string lookupTable = fk.Value;

                if (!dataTable.Columns.Contains(columnName))
                    continue;

                var combo = new DataGridViewComboBoxColumn
                {
                    Name = columnName,
                    DataPropertyName = columnName,
                    DataSource = new BindingSource(lookupData[lookupTable], null),
                    DisplayMember = "Value",
                    ValueMember = "Key",
                    FlatStyle = FlatStyle.Flat
                };

                int index = dataGridView1.Columns[columnName].Index;
                dataGridView1.Columns.Remove(columnName);
                dataGridView1.Columns.Insert(index, combo);
            }
        }

        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            if (!foreignKeysMap.TryGetValue(currentTable, out var fkColumns))
                return;

            foreach (var fk in fkColumns)
            {
                string columnName = fk.Key;
                string lookupTable = fk.Value;

                if (lookupData.TryGetValue(lookupTable, out var lookup) && lookup.Count > 0)
                {
                    e.Row.Cells[columnName].Value = lookup.First().Key;
                }
            }
        }

        private void goods_Click(object sender, EventArgs e)
        {
            Form form = new ContractsForm();
            form.ShowDialog();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (conn != null) { conn.Close(); }
        }

    }
}
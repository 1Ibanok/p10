using Npgsql;

namespace PR10
{

    public partial class ContractsForm : Form
    {
        NpgsqlCommand cmd;
        NpgsqlDataReader reader;
        public ContractsForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximumSize = this.Size;
            LoadData();
        }

        void LoadData()
        {
            visitorsList.Controls.Clear();

            string query = $"select фио_клиента, телефон_клиента, count(фио_клиента), sum(сумма) from договоры";
            if (toggleFilters.Checked == true)
            {
                query += $" where дата_договора = '{date.Value.ToString("yyyy-MM-dd")}'";
            }
            query += " group by фио_клиента, телефон_клиента";
            cmd = new NpgsqlCommand(query, MainForm.conn);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Panel panel = new Panel();
                panel.Location = new Point(3, 3);
                panel.Name = "panel";
                panel.Size = new Size(500, 140);
                panel.TabIndex = 1;
                panel.BorderStyle = BorderStyle.FixedSingle;
                panel.Margin = new Padding(2);

                CreateLabel(panel, new Point(10, 10), "ФИО представителя: " + reader[0].ToString());
                CreateLabel(panel, new Point(10, 25), "Телефон: " + reader[1].ToString());
                CreateLabel(panel, new Point(10, 40), "Количество перевозок: " + reader[2].ToString());
                CreateLabel(panel, new Point(10, 85), "Общая сумма: " + reader[3].ToString());
                string sale = "Скидка ";
                decimal count = decimal.Parse(reader[3].ToString());
                if (count > 100000)
                    sale += "15%";
                else if (count > 50000 && count < 100000)
                    sale += "10%";
                else if (count > 10000 && count < 50000)
                    sale += "5%";
                else
                    sale += "0%";
                CreateLabel(panel, new Point(200, 85), sale);

                visitorsList.Controls.Add(panel);
            }
            cmd.Dispose();
            cmd.Cancel();
            reader.Close();
        }

        private void CreateLabel(Panel parent, Point pos, string text, string? name = "label")
        {
            Label label = new Label();
            label.AutoSize = true;
            label.Location = pos;
            label.Name = name;
            label.Size = new Size(80, 15);
            label.TabIndex = 11;
            label.Text = text;
            parent.Controls.Add(label);
        }



        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clubs_TextChanged(object sender, EventArgs e)
        {
            if (toggleFilters.Checked == true) { LoadData(); }
        }

        private void date_ValueChanged(object sender, EventArgs e)
        {
            if (toggleFilters.Checked == true) { LoadData(); }
        }

        private void toggleFilters_CheckedChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }

}

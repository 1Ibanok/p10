namespace PR10
{
    partial class ContractsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContractsForm));
            visitorsList = new FlowLayoutPanel();
            npgsqlCommandBuilder1 = new Npgsql.NpgsqlCommandBuilder();
            back = new Button();
            clubs = new ComboBox();
            date = new DateTimePicker();
            toggleFilters = new CheckBox();
            SuspendLayout();
            // 
            // visitorsList
            // 
            visitorsList.AutoScroll = true;
            visitorsList.BackColor = Color.FromArgb(164, 164, 163);
            visitorsList.Font = new Font("Times New Roman", 10.2F);
            visitorsList.Location = new Point(12, 12);
            visitorsList.Name = "visitorsList";
            visitorsList.Size = new Size(678, 288);
            visitorsList.TabIndex = 0;
            // 
            // npgsqlCommandBuilder1
            // 
            npgsqlCommandBuilder1.QuotePrefix = "\"";
            npgsqlCommandBuilder1.QuoteSuffix = "\"";
            // 
            // back
            // 
            back.BackColor = Color.FromArgb(121, 2, 2);
            back.Font = new Font("Times New Roman", 10.2F);
            back.ForeColor = Color.White;
            back.Location = new Point(695, 70);
            back.Name = "back";
            back.Size = new Size(200, 30);
            back.TabIndex = 7;
            back.Text = "Назад";
            back.UseVisualStyleBackColor = false;
            back.Click += back_Click;
            // 
            // clubs
            // 
            clubs.FormattingEnabled = true;
            clubs.Location = new Point(695, 11);
            clubs.Name = "clubs";
            clubs.Size = new Size(121, 24);
            clubs.TabIndex = 8;
            clubs.TextChanged += clubs_TextChanged;
            // 
            // date
            // 
            date.Location = new Point(695, 41);
            date.Name = "date";
            date.Size = new Size(200, 23);
            date.TabIndex = 9;
            date.ValueChanged += date_ValueChanged;
            // 
            // toggleFilters
            // 
            toggleFilters.AutoSize = true;
            toggleFilters.Location = new Point(822, 11);
            toggleFilters.Name = "toggleFilters";
            toggleFilters.Size = new Size(77, 20);
            toggleFilters.TabIndex = 10;
            toggleFilters.Text = "Фильтры";
            toggleFilters.UseVisualStyleBackColor = true;
            toggleFilters.CheckedChanged += toggleFilters_CheckedChanged;
            // 
            // ContractsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(904, 306);
            Controls.Add(toggleFilters);
            Controls.Add(date);
            Controls.Add(clubs);
            Controls.Add(back);
            Controls.Add(visitorsList);
            Font = new Font("Times New Roman", 10.2F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ContractsForm";
            Text = "Договора";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel visitorsList;
        private Npgsql.NpgsqlCommandBuilder npgsqlCommandBuilder1;
        private Button back;
        private ComboBox clubs;
        private DateTimePicker date;
        private CheckBox toggleFilters;
    }
}
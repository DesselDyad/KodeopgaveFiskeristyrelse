using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace kodeopgaveFiskeristyrelse
{
    /**
     * Class responible for both handleing the user interaction with the user interface, and 
     * acting as a controller interacting with the database. It's like a miniature MVC in a single class. 
     * Ideally, one wants to separate everything in here into multiple, significantly more complex, sub-systems. 
     */
    public partial class Form1 : Form
    {
        DataTable table = new DataTable();
        private String conn_string = "server=127.0.0.1;uid=root;pwd=nopenopenopenothere;database=kodeOpgavefiskeristyrelse";

        public Form1()
        {
            InitializeComponent();
            SetupUI();
        }

        /**
         * Method Responible for setting up the initial user interface as the application starts up. 
         * Was supposed to initiate certain datepicker's to min-and-max values from the database, but.. 
         * it refused to do so, properly. Weird casting errors, mainly, i think. 
         */
        private void SetupUI()
        {
            using (var connection = new MySqlConnection(conn_string))
            {
                connection.Open();
                using (var adapter = new MySqlDataAdapter("SELECT * from data_til_rapportering_af_fiskeri", connection))
                {
                    adapter.Fill(table);
                    this.dataGridView1.DataSource = table;
                    var firstRow = table.Rows[0];
                    var lastRow = table.Rows[table.Rows.Count - 10];
                    this.dateTimePicker1.Value = DateTime.Parse((String)firstRow["Afsejling_Datomedtid"]);
                    this.dateTimePicker2.Value = DateTime.Parse((String)lastRow["Ankomst_Datomedtid"]);
                }/*
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "(SELECT * FROM data_til_rapportering_af_fiskeri ORDER BY Afsejling_Datomedtid ASC LIMIT 1) UNION (SELECT * FROM data_til_rapportering_af_fiskeri ORDER BY Afsejling_Datomedtid DESC LIMIT 1)";
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        String start = (String)reader["Afsejling_Datomedtid"];
                        String end = (String)reader["Ankomst_Datomedtid"];
                        //this.dateTimePicker1.Value = (DateTime)reader["Afsejling_Datomedtid"];
                        //this.dateTimePicker2.Value = (DateTime)reader["Ankomst_Datomedtid"];
                        this.dateTimePicker1.Value = DateTime.Parse(start.Substring(0,start.IndexOf(" ") - 1));
                        this.dateTimePicker2.Value = DateTime.Parse(end.Substring(0, end.IndexOf(" ") - 1));
                    }
                    //var firstRow = table.Rows[0];
                    //var lastRow = table.Rows[table.Rows.Count - 10];
                    //this.dateTimePicker1.Value = DateTime.Parse((String)firstRow["Afsejling_Datomedtid"]);
                    //this.dateTimePicker2.Value = DateTime.Parse((String)lastRow["Ankomst_Datomedtid"]);
                }*/
            }
        }

        /**
         * Method responsible for registering when the user selects cells on the table 
         * and then populating the corresponding fields to the left accordingly. 
         */
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                this.textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                this.textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                this.textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                this.textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                this.textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                this.textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                this.dateTimePicker3.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString());
                this.dateTimePicker4.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString());
                this.textBox8.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                this.textBox9.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
            } catch (System.FormatException ex)
            {
                MessageBox.Show("ERROR ERROR ERROR" + ex.Message);
            }
        }

        /**
         * Method responsible for registering when the user selects an entire row at a time on the table 
         * and then populating the corresponding fields to the left accordingly. 
         * 
         * SPOILER ALERT didnt work as intended, ran out of time for google'ing why it doesn't work
         * as intended. 
         */
        private void dataGridView1_dataGridView_SelectionChanged(object sender, DataGridViewCellEventArgs e)
        {
            /*
            this.textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            this.textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            this.textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            this.textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            this.textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            this.textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            this.textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            this.textBox8.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            this.textBox9.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            */
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        /**
         * The idea here was to have two DateTimePickers that let the user define a time interval, 
         * and then fundamentally use this as a change-listener, so that every time the user 
         * makes a new selection, the table updates correspondingly. 
         * 
         * Alas, there are some weird type-casting errors occurring in converting DateTimePicker values and 
         * Database values back and forth, respectively. This also didnt end up 
         * working as intended. I tried just about everything and regret to have wasted a rather 
         * significant amount of time trying to get it to function. 
         */
        private void updateUI()
        {
            /**
             * 
                using (var connection = new MySqlConnection("server=127.0.0.1;uid=root;pwd=admin;database=kodeOpgavefiskeristyrelse"))
                using (var cmd = new MySqlCommand("SELECT * FROM data_til_rapportering_af_fiskeri WHERE Afsejling_Datomedtid >= @afsejling AND Ankomst_Datomedtid <= @ankomst", connection))
                {
                    this.label1.Text = dateTimePicker1.Value.ToString();
                    cmd.Parameters.Add("@afsejling", MySqlDbType.DateTime).Value = dateTimePicker1.Value;
                    cmd.Parameters.Add("@ankomst", MySqlDbType.DateTime).Value = dateTimePicker2.Value;
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        table = new DataTable();
                        adapter.Fill(table);
                        this.dataGridView1.DataSource = table;
                    }
                }
             */
        }

        /**
         * Change listener for start-datetimepicker
         */
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            updateUI();

        }
        /**
        * Change listener for end-datetimepicker
        */
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            updateUI();
        }

        /**
         * Event handler for the insert-button.
         * Lacks a whooooole lot of validation, both on form, content and whether or not things already exist in the database. 
         * Ended up costing too much time. 
         */
        private void InsertBtn_Click(object sender, EventArgs e)
        {
            if(this.textBox1.Text != "" && this.textBox2.Text != "" && this.textBox3.Text != "" && this.textBox4.Text != "" && this.textBox5.Text != "" && this.textBox6.Text != "" && this.textBox7.Text != "" && this.textBox8.Text != "" && this.textBox9.Text != "" && this.dateTimePicker3.Value != null && this.dateTimePicker4.Value != null)
            using (var connection = new MySqlConnection(conn_string))
            {
                connection.Open();
                using (MySqlCommand cmd = connection.CreateCommand())
                {
                        cmd.CommandText = "INSERT INTO data_til_rapportering_af_fiskeri (Sloeret_SkibID,Skib_Nationalitet,Fangst,Redskab,Art,Fiske_Zone,Afs_Havn,Afsejling_Datomedtid,Ankomst_Datomedtid,Fng_Um_Eng,Fng_Um_Egenskab) VALUES (@Sloeret_SkibID,@Skib_Nationalitet,@Fangst,@Redskab,@Art,@Fiske_Zone,@Afs_Havn,@Afsejling_Datomedtid,@Ankomst_Datomedtid,@Fng_Um_Eng,@Fng_Um_Egenskab)";
                        cmd.Parameters.Add("@Sloeret_SkibID", MySqlDbType.VarChar).Value = this.textBox1.Text;
                        cmd.Parameters.Add("@Skib_Nationalitet", MySqlDbType.VarChar).Value = this.textBox2.Text;
                        cmd.Parameters.Add("@Fangst", MySqlDbType.VarChar).Value = this.textBox3.Text;
                        cmd.Parameters.Add("@Redskab", MySqlDbType.VarChar).Value = this.textBox4.Text;
                        cmd.Parameters.Add("@Art", MySqlDbType.VarChar).Value = this.textBox5.Text;
                        cmd.Parameters.Add("@Afs_Havn", MySqlDbType.VarChar).Value = this.textBox6.Text;
                        cmd.Parameters.Add("@Afsejling_Datomedtid", MySqlDbType.DateTime).Value = this.dateTimePicker3.Value;
                        cmd.Parameters.Add("@Ankomst_Datomedtid", MySqlDbType.DateTime).Value = this.dateTimePicker4.Value;
                        cmd.Parameters.Add("@Fng_Um_Eng", MySqlDbType.VarChar).Value = this.textBox7.Text;
                        cmd.Parameters.Add("@Fng_Um_Egenskab", MySqlDbType.VarChar).Value = this.textBox8.Text;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Entry Added Successfully");
                        updateUI();
                    }
            } else
            {
                MessageBox.Show("Some Fields Not Filled Out Sufficiently");
            }
        }

        /**
         * Event handler for the update-button. 
         * Lacks a whooooole lot of validation, both on form, content and whether or not things already exist in the database. 
         * Ended up costing too much time. 
         */
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text != "" && this.textBox2.Text != "" && this.textBox3.Text != "" && this.textBox4.Text != "" && this.textBox5.Text != "" && this.textBox6.Text != "" && this.textBox7.Text != "" && this.textBox8.Text != "" && this.textBox9.Text != "" && this.dateTimePicker3.Value != null && this.dateTimePicker4.Value != null)
                using (var connection = new MySqlConnection(conn_string))
                {
                    connection.Open();
                    using (MySqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE data_til_rapportering_af_fiskeri SET Skib_Nationalitet=@Skib_Nationalitet,Fangst=@Fangst,Redskab@Redskab,Art=@Art,Fiske_Zone=@Fiske_Zone,Afs_Havn=@Afs_Havn,Afsejling_Datomedtid=@Afsejling_Datomedtid,Ankomst_Datomedtid=@Ankomst_Datomedtid,Fng_Um_Eng=@Fng_Um_Eng,Fng_Um_Egenskab=@Fng_Um_Egenskab WHERE Sloeret_SkibID=@Sloeret_SkibID";
                        cmd.Parameters.Add("@Sloeret_SkibID", MySqlDbType.VarChar).Value = this.textBox1.Text;
                        cmd.Parameters.Add("@Skib_Nationalitet", MySqlDbType.VarChar).Value = this.textBox2.Text;
                        cmd.Parameters.Add("@Fangst", MySqlDbType.VarChar).Value = this.textBox3.Text;
                        cmd.Parameters.Add("@Redskab", MySqlDbType.VarChar).Value = this.textBox4.Text;
                        cmd.Parameters.Add("@Art", MySqlDbType.VarChar).Value = this.textBox5.Text;
                        cmd.Parameters.Add("@Afs_Havn", MySqlDbType.VarChar).Value = this.textBox6.Text;
                        cmd.Parameters.Add("@Afsejling_Datomedtid", MySqlDbType.DateTime).Value = this.dateTimePicker3.Value;
                        cmd.Parameters.Add("@Ankomst_Datomedtid", MySqlDbType.DateTime).Value = this.dateTimePicker4.Value;
                        cmd.Parameters.Add("@Fng_Um_Eng", MySqlDbType.VarChar).Value = this.textBox7.Text;
                        cmd.Parameters.Add("@Fng_Um_Egenskab", MySqlDbType.VarChar).Value = this.textBox8.Text;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Entry Updated Successfully");
                        updateUI();
                    }
                }
            else
            {
                MessageBox.Show("Some Fields Not Filled Out Sufficiently");
            }
        }

        /**
         * Event handler for the delete-button
         */
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text != "")
                using (var connection = new MySqlConnection(conn_string))
                {
                    connection.Open();
                    using (MySqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "DELETE * FROM data_til_rapportering_af_fiskeri WHERE Sloeret_SkibID = @Sloeret_SkibID";
                        cmd.Parameters.Add("@Sloeret_SkibID", MySqlDbType.VarChar).Value = this.textBox1.Text;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Entry Deleted Successfully");
                        updateUI();
                    }
                }
            else
            {
                MessageBox.Show("Not Pointing To Any Entry");
            }
        }

        /**
         * Unused event handler i didn't mean to create in the first place. VS has a knack for loving 
         * missclicks and hating usability, apparently. 
         */
        private void label5_Click(object sender, EventArgs e)
        {

        }

        /**
         * Unused event handler i didn't mean to create in the first place. VS has a knack for loving 
         * missclicks and hating usability, apparently. 
         */
        private void label4_Click(object sender, EventArgs e)
        {

        }

        /**
         * Unused event handler i didn't mean to create in the first place. VS has a knack for loving 
         * missclicks and hating usability, apparently. 
         */
        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        /**
         * Unused event handler i didn't mean to create in the first place. VS has a knack for loving 
         * missclicks and hating usability, apparently. 
         */
        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}

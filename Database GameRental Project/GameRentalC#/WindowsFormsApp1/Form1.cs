using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'rentalDataSet.GAME' table. You can move, or remove it, as needed.
            this.gAMETableAdapter.Fill(this.rentalDataSet.GAME);

        }

        private void button2_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-TB8DQM3;Initial Catalog=rental;Integrated Security=True");
            con.Open();
            SqlCommand myCommand = new SqlCommand("insert into CLIENT(FIRST_NAME,LAST_NAME,EMAIL,PASSWORD,BIRTH_DATE,STATE,CITY,ZIP_CODE,STREET) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox9.Text + "','" + textBox8.Text + "','" + textBox7.Text + "')", con);
            myCommand.ExecuteNonQuery();
            MessageBox.Show("Client has been added Successfully");

            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-TB8DQM3;Initial Catalog=rental;Integrated Security=True");
            con.Open();
            SqlCommand myCommand = new SqlCommand("insert into ADMIN(FIRST_NAME,LAST_NAME,EMAIL,PASSWORD) values ('" + textBox12.Text + "','" + textBox11.Text + "','" + textBox10.Text + "','" + textBox13.Text + "')", con);
            myCommand.ExecuteNonQuery();
            MessageBox.Show("Admin has been added Successfully");

            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-TB8DQM3;Initial Catalog=rental;Integrated Security=True");
            con.Open();
            SqlCommand myCommand = new SqlCommand("insert into VENDOR(V_FNAME,V_LNAME) values ('" + textBox17.Text + "','" + textBox16.Text + "')", con);
            myCommand.ExecuteNonQuery();
            MessageBox.Show("Vendor has been added Successfully");

            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-TB8DQM3;Initial Catalog=rental;Integrated Security=True");
            con.Open();
            SqlCommand myCommand = new SqlCommand("insert into GAME(V_ID,A_ID,G_NAME,RATING,CATEGORY,DEVELOP_DATE) values ('" + textBox22.Text + "','" + textBox21.Text + "','" + textBox20.Text + "','" + textBox19.Text + "','" + textBox18.Text + "','" + textBox32.Text + "')", con);
            myCommand.ExecuteNonQuery();
            MessageBox.Show("Game has been added Successfully");
            con.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-TB8DQM3;Initial Catalog=rental;Integrated Security=True");
            con.Open();
            SqlCommand myCommand = new SqlCommand("insert into RENT(C_ID,G_ID,START_DAY,START_MONTH,START_YEAR) values ('" + textBox25.Text + "','" + textBox24.Text + "','" + textBox23.Text + "','" + textBox15.Text + "','" + textBox14.Text + "')", con);
            myCommand.ExecuteNonQuery();
            MessageBox.Show("Rent Process has been Successfully");
            con.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-TB8DQM3;Initial Catalog=rental;Integrated Security=True");
            con.Open();
            SqlCommand myCommand = new SqlCommand("insert into RENTING_OUT(V_ID,C_ID,END_DAY,END_MONTH,END_YEAR) values ('" + textBox29.Text + "','" + textBox28.Text + "','" + textBox26.Text + "','" + textBox30.Text + "','" + textBox31.Text + "')", con);
            myCommand.ExecuteNonQuery();
            String query = "select C_ID,START_DAY, START_MONTH, START_YEAR from RENT WHERE C_ID = @id";
            var date = new SqlCommand(query, con);
            date.Parameters.AddWithValue("@id", textBox28.Text);
            try
            {
                
                using (SqlDataReader reader1 = date.ExecuteReader())
                {
                    int d, m, y, amont;
                    if (reader1.Read())
                    {
                        d = (int)reader1["START_DAY"];
                        m = (int)reader1["START_MONTH"];
                        y = (int)reader1["START_YEAR"];
                        int ey = int.Parse(textBox31.Text);
                        int em = int.Parse(textBox30.Text);
                        int ed = int.Parse(textBox26.Text);
                        amont = (((ey-y)*365)+((em-m)*30)+(ed-d))*10;
                        Console.WriteLine($"{d}/{m}/{y}");
                        MessageBox.Show($"The Renting Price is {amont}\nTHANK U FOR USING GAME RENTAL 😁");
                        reader1.Close();
                        Console.WriteLine(amont);
                        SqlCommand amount = new SqlCommand("UPDATE RENTING_OUT SET AMOUNT = "+amont+ "WHERE R_ID = @rid;", con);
                        String s_q = "SELECT TOP 1 * FROM RENTING_OUT ORDER BY R_ID DESC";
                        SqlCommand s_top = new SqlCommand(s_q, con);
                        using (SqlDataReader reader = s_top.ExecuteReader())
                        {
                            int idd;
                            if (reader.Read())
                            {
                                idd = (int)reader["R_ID"];
                                reader.Close();
                                amount.Parameters.AddWithValue("@rid", idd);
                                amount.ExecuteNonQuery();
                            }
                        }
                            

                    }

                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex} Data Not Found");
            }   
            con.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.gAMETableAdapter.Fill(this.rentalDataSet.GAME);    
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-TB8DQM3;Initial Catalog=rental;Integrated Security=True");
            con.Open();
            SqlCommand myCommand = new SqlCommand("update GAME set V_ID = @vid, A_ID = @aid, G_NAME = @gn, RATING = @rate, CATEGORY = @cat, DEVELOP_DATE = @dev where G_ID = @gid", con);
            myCommand.Parameters.AddWithValue("@gid", textBox38.Text);
            myCommand.Parameters.AddWithValue("@vid", textBox37.Text);
            myCommand.Parameters.AddWithValue("@aid", textBox36.Text);
            myCommand.Parameters.AddWithValue("@gn", textBox35.Text);
            myCommand.Parameters.AddWithValue("@rate", textBox34.Text);
            myCommand.Parameters.AddWithValue("@cat", textBox33.Text);
            myCommand.Parameters.AddWithValue("@dev", textBox27.Text);
            myCommand.ExecuteNonQuery();
            MessageBox.Show("Game has been Updated");
            con.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-TB8DQM3;Initial Catalog=rental;Integrated Security=True");
            con.Open();
            string query = "select G_NAME from GAME where G_NAME not in (select GAME.G_NAME from GAME,RENT where GAME.G_ID = RENT.G_ID and RENT.START_MONTH >= 5 and RENT.START_YEAR = 2022 )";
            var q2 = new SqlCommand(query, con);
            try
            {
                using (SqlDataReader reader1 = q2.ExecuteReader())
                {
                    var tupleList = new List<Tuple<int, string>>();
                    int cnt = 1;
                    string gname, ans = "";
                    while (reader1.Read())
                    {
                        gname = reader1["G_NAME"].ToString();
                        tupleList.Add(Tuple.Create(cnt, gname));
                        string item = $"{tupleList[cnt - 1].Item1}) {tupleList[cnt - 1].Item2}\n";
                        ans += item;
                        cnt++;
                    }
                    MessageBox.Show(ans);
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex} Data Not Found");
            }
            con.Close();

        }

        private void button13_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-TB8DQM3;Initial Catalog=rental;Integrated Security=True");
            con.Open();
            string query = "select V_FNAME+' '+V_LNAME as Names from VENDOR where V_ID not in ( select VENDOR.V_ID  from VENDOR inner join GAME  on VENDOR.V_ID=GAME.V_ID inner join RENT on GAME.G_ID=RENT.G_ID where RENT.START_MONTH>=5 and RENT.START_YEAR=2022)";
            var q5 = new SqlCommand(query, con);
            try
            {

                using (SqlDataReader reader1 = q5.ExecuteReader())
                {
                    var tupleList = new List<Tuple<int, string>>();
                    int cnt = 1;
                    string name, ans = "";
                    while (reader1.Read())
                    {
                        name = reader1["Names"].ToString();
                        tupleList.Add(Tuple.Create(cnt, name));
                        string item = $"{tupleList[cnt - 1].Item1}) {tupleList[cnt - 1].Item2}\n";
                        ans += item;
                        cnt++;
                    }
                    MessageBox.Show(ans);
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex} Data Not Found");
            }
            con.Close();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-TB8DQM3;Initial Catalog=rental;Integrated Security=True");
            con.Open();
            string query = "select V_FNAME+' '+V_LNAME as Names from VENDOR where VENDOR.V_ID not in (select VENDOR.V_ID from VENDOR inner join GAME on VENDOR.V_ID=GAME.V_ID where GAME.DEVELOP_DATE >='2022-1-1')";
            var q6 = new SqlCommand(query, con);
            try
            {

                using (SqlDataReader reader1 = q6.ExecuteReader())
                {
                    var tupleList = new List<Tuple<int, string>>();
                    int cnt = 1;
                    string name, ans = "";
                    while (reader1.Read())
                    {
                        name = reader1["Names"].ToString();
                        tupleList.Add(Tuple.Create(cnt, name));
                        string item = $"{tupleList[cnt - 1].Item1}) {tupleList[cnt - 1].Item2}\n";
                        ans += item;
                        cnt++;
                    }
                    MessageBox.Show(ans);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex} Data Not Found");
            }
            con.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-TB8DQM3;Initial Catalog=rental;Integrated Security=True");
            con.Open();
            string query = "SELECT GAME.G_NAME FROM GAME where G_ID in (SELECT RENT.G_ID FROM RENT GROUP BY G_ID HAVING  COUNT(C_ID) in (SELECT TOP 1 COUNT(C_ID) as countt FROM RENT GROUP BY G_ID ORDER BY countt DESC))";
            var q1 = new SqlCommand(query, con);
            try
            {

                using (SqlDataReader reader1 = q1.ExecuteReader())
                {
                    var tupleList = new List<Tuple<int, string>>();
                    int cnt = 1;
                    string name, ans = "";
                    if (reader1.Read())
                    {
                        name = reader1["G_NAME"].ToString();
                        tupleList.Add(Tuple.Create(cnt, name));
                        string item = $"{tupleList[cnt - 1].Item1}) {tupleList[cnt - 1].Item2}\n";
                        ans += item;
                        cnt++;
                    }
                    MessageBox.Show(ans);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex} Data Not Found");
            }
            con.Close();

        }

        private void button11_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-TB8DQM3;Initial Catalog=rental;Integrated Security=True");
            con.Open();
            string query = "SELECT Client.FIRST_NAME+' '+Client.LAST_NAME as Name FROM CLIENT where C_ID in (SELECT RENT.C_ID FROM RENT GROUP BY C_ID HAVING  COUNT(C_ID) in (SELECT TOP 1 COUNT(C_ID) as countt FROM RENT GROUP BY C_ID HAVING MAX(START_YEAR) = 2022 and MAX(START_MONTH)>=5 ORDER BY countt DESC))";
            var q3 = new SqlCommand(query, con);
            try
            {

                using (SqlDataReader reader1 = q3.ExecuteReader())
                {
                    var tupleList = new List<Tuple<int, string>>();
                    int cnt = 1;
                    string name, ans = "";
                    while (reader1.Read())
                    {
                        name = reader1["Name"].ToString();
                        tupleList.Add(Tuple.Create(cnt, name));
                        string item = $"{tupleList[cnt - 1].Item1}) {tupleList[cnt - 1].Item2}\n";
                        ans += item;
                        cnt++;
                    }
                    MessageBox.Show(ans);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex} Data Not Found");
            }
            con.Close();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-TB8DQM3;Initial Catalog=rental;Integrated Security=True");
            con.Open();
            string query = "SELECT VENDOR.V_FNAME+' '+VENDOR.V_LNAME as Name FROM VENDOR where V_ID in (SELECT RENTING_OUT.V_ID FROM RENTING_OUT GROUP BY V_ID HAVING  COUNT(V_ID) in (SELECT TOP 1 COUNT(V_ID) as countt FROM RENTING_OUT GROUP BY V_ID HAVING MAX(END_YEAR) = 2022 and MAX(END_MONTH)>=5 ORDER BY countt DESC))";
            var q4 = new SqlCommand(query, con);
            try
            {

                using (SqlDataReader reader1 = q4.ExecuteReader())
                {
                    var tupleList = new List<Tuple<int, string>>();
                    int cnt = 1;
                    string name, ans = "";
                    while (reader1.Read())
                    {
                        name = reader1["Name"].ToString();
                        tupleList.Add(Tuple.Create(cnt, name));
                        string item = $"{tupleList[cnt - 1].Item1}) {tupleList[cnt - 1].Item2}\n";
                        ans += item;
                        cnt++;
                    }
                    MessageBox.Show(ans);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex} Data Not Found");
            }
            con.Close();
        }
    }
}

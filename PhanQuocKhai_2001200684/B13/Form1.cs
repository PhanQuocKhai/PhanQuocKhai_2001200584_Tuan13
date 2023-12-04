using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace B13
{
    public partial class Form1 : Form
    {
        string conStr = "Data Source = A209PC47; Initial Catalog = USERS; Integrated Security = True";
        DataSet ds_TK;
        SqlDataAdapter da_TK;
        public Form1()
        {
            InitializeComponent();
            SqlConnection con = new SqlConnection(conStr);
            string strsl = "Select * from TK";
            da_TK = new SqlDataAdapter(strsl, con);
            ds_TK = new DataSet();
            da_TK.Fill(ds_TK, "TK");

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conStr);
            if(con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            
            DataRow newrow = ds_TK.Tables[0].NewRow();
            newrow["id"] = generatedID();
            newrow["TenTK"] = txtTaiKhoan.Text;
            newrow["matKhau"] = txtMatKhau.Text;
            ds_TK.Tables[0].Rows.Add(newrow);
            SqlCommandBuilder cB = new SqlCommandBuilder(da_TK);
            da_TK.Update(ds_TK, "TK");
            txtTaiKhoan.Clear();
            txtMatKhau.Clear();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conStr);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            ds_TK.Tables["TK"].PrimaryKey = new DataColumn[] { ds_TK.Tables["TK"].Columns["TenTK"] };
            DataRow dr = ds_TK.Tables["TK"].Rows.Find(txtTaiKhoan.Text);
            if(dr != null)
            {
                dr.Delete();
            }
            SqlCommandBuilder cB = new SqlCommandBuilder(da_TK);
            da_TK.Update(ds_TK, "TK");
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conStr);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            ds_TK.Tables["TK"].PrimaryKey = new DataColumn[] { ds_TK.Tables["TK"].Columns["TenTK"] };
            DataRow dr = ds_TK.Tables["TK"].Rows.Find(txtTaiKhoan.Text);
            if (dr != null)
            {
                dr["matKhau"] = txtMatKhau.Text;
            }
            SqlCommandBuilder cB = new SqlCommandBuilder(da_TK);
            da_TK.Update(ds_TK, "TK");
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        void DataBindDing(DataTable tk)
        {
            txtTaiKhoan.DataBindings.Clear();
            txtMatKhau.DataBindings.Clear();

            txtTaiKhoan.DataBindings.Add("Text", tk, "TenTK");
            txtMatKhau.DataBindings.Add("Text", tk, "matKhau");
        }
        void loadGrid()
        {
            dataGridView1.DataSource = ds_TK.Tables[0];
            DataBindDing(ds_TK.Tables[0]);
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            loadGrid();
        }

        private string generatedID()
        {
            DateTime id = DateTime.Now;
            string day = id.Day.ToString();
            string month = id.Month.ToString();
            string year = id.Year.ToString();
            string hour = id.Hour.ToString();
            string minute = id.Minute.ToString();
            string second = id.Second.ToString();
            string dateID = year + month + day + hour + minute + second;
            return dateID;
        }


    }
}

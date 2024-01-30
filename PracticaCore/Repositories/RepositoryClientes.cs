using PracticaCore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace PracticaCore.Repositories
{
    public class RepositoryClientes
    {
        private SqlConnection cn;
        private SqlCommand com;
        private SqlDataReader reader;

        public RepositoryClientes() 
        {
            string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=NETCORE;User ID=SA;Password=MCSD2023";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;   
        }

        public List<string> GetClientes()
        {
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_CLIENTES";
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            List <string> clientes = new List<string>();
            while (this.reader.Read())
            {
                clientes.Add(this.reader["Empresa"].ToString());
            }
            this.reader.Close();
            this.cn.Close();

            return clientes;
        }

        public Cliente FindCliente(string empresa)
        {
            string sql = "select CodigoCliente,Empresa,Contacto,Cargo,Ciudad,Telefono from clientes where Empresa=@EMPRESA";
            SqlParameter pamEmpresa = new SqlParameter("@EMPRESA", empresa);
            this.com.Parameters.Add(pamEmpresa);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            Cliente cliente = null;
            while (this.reader.Read())
            {
                cliente = new Cliente();
                string codigoCliente = this.reader["CodigoCliente"].ToString();
                string emp = this.reader["empresa"].ToString();
                string contacto = this.reader["Contacto"].ToString();
                string cargo = this.reader["Cargo"].ToString();
                string ciudad = this.reader["Ciudad"].ToString();
                int telefono = int.Parse(this.reader["Telefono"].ToString());
                cliente.CodigoCliente = codigoCliente;
                cliente.Empresa = emp;
                cliente.Contacto = contacto;
                cliente.Cargo = cargo;
                cliente.Ciudad = ciudad;
                cliente.Telefono = telefono;
                //FindPedido(cliente);
            }
            this.reader.Close();
            this.com.Parameters.Clear();
            this.cn.Close();
            return cliente;

        }

        public List<string> FindPedido(Cliente cliente)
        {
            string codigoCliente = cliente.CodigoCliente;
            SqlParameter pamCodigo = new SqlParameter("@CODIGOCLIENTE", codigoCliente );
            this.com.Parameters.Add(pamCodigo );
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_FIND_PEDIDOS";
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            List<string> fechasPedido = new List<string>();
            while (this.reader.Read())
            {
                fechasPedido.Add(this.reader["CodigoPedido"].ToString());
            }
            this.reader.Close();
            this.cn.Close();
            return fechasPedido;


        }


    }
}

using PracticaCore.Models;
using PracticaCore.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticaCore
{
    #region PROCEDIMIENTOS ALMACENADOS
    /*create procedure SP_FIND_CLIENTES(@EMPRESA nvarchar(50))
    AS
    BEGIN
        select CodigoCliente, Empresa, Contacto, Cargo, Ciudad, Telefono from clientes where Empresa = @EMPRESA
    END;*/
    /*
     create procedure SP_FIND_PEDIDOS(@CODIGOCLIENTE nvarchar(50))
        AS
        BEGIN
	        select CodigoPedido from pedidos where CodigoCliente=@CODIGOCLIENTE
        END;
     */
    #endregion
    public partial class FormPractica : Form
    {
        RepositoryClientes repo;
        public FormPractica()
        {
            InitializeComponent();
            this.repo = new RepositoryClientes();
            this.loadClientes();
        }

        private void loadClientes()
        {
            List<string> clientes = this.repo.GetClientes();
            foreach (string data in clientes)
            {
                this.cmbclientes.Items.Add(data);
            }
        }

        private void cmbclientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.cmbclientes.SelectedIndex;
            if (index != -1)
            {
                string empresaElegida = this.cmbclientes.SelectedItem.ToString();
                Cliente cliente = this.repo.FindCliente(empresaElegida);
                List<string> pedidos = this.repo.FindPedido(cliente);
                foreach (string data in pedidos)
                {
                    this.lstpedidos.Items.Add(data);
                }
                this.txtempresa.Text = cliente.Empresa;
                this.txtcontacto.Text = cliente.Contacto;
                this.txtcargo.Text = cliente.Cargo;
                this.txtciudad.Text = cliente.Ciudad;
                this.txttelefono.Text = cliente.Telefono.ToString();
            }

        }
    }
}

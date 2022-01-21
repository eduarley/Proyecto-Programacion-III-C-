using CapaEntidades;
using CapaLogicaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Progra_III
{
    public partial class FrmMantUsuario : Form
    {

        CapaLogicaNegocio.UsuarioLogica usuarioLogica;
        public Usuario Usuario { get; set; }

        public FrmMantUsuario()
        {
            usuarioLogica = new CapaLogicaNegocio.UsuarioLogica();
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            
            try
            {
               
                    
                    FrmDetalleUsuario frm = new FrmDetalleUsuario();
                    
                    var resultado = frm.ShowDialog();
                    if (resultado == DialogResult.OK)
                        Refrescar();
                

            }
            catch (Exception ex)
            {

                MessageBox.Show("Hubo un problema: \n"+ex.Message, "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void CargarCategoria()
        {
            CategoriaLogica log = new CategoriaLogica();
           
            cmbCategoria.DataSource = log.ObtenerCategoriasConTodos();
            cmbCategoria.DisplayMember = "Descripcion";
            cmbCategoria.ValueMember = "Id";
        }



        private void FrmMantEmpleado_Load(object sender, EventArgs e)
        {
           
            CargarCategoria();
            Refrescar();
            cmbCategoria.SelectedIndex = 0;

           
            
        }



        public void Refrescar()
        {
            
            dgvDatos.DataSource = usuarioLogica.ObtenerUsuarios();
            if (Usuario is Gerente)
            {
                dgvDatos.DataSource = usuarioLogica.ObtenerUsuarios();
            }
            else
            {
                dgvDatos.DataSource = usuarioLogica.ObtenerClientes();
                label4.Visible = false;
                cmbCategoria.Visible = false;   
            }

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            



            try
            {
                if (dgvDatos.SelectedRows.Count != 0)
                {
                    Usuario usuario=(Usuario)dgvDatos.SelectedRows[0].DataBoundItem;
                    FrmDetalleUsuario frm = new FrmDetalleUsuario();
                    frm.Usuario = usuario;
                    var resultado = frm.ShowDialog();
                    if (resultado == DialogResult.OK)
                        Refrescar();
                }

            }
            catch (Exception ex)
            {
           
                throw;
            }








        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDatos.SelectedRows.Count != 0)
                {
                    if(MessageBox.Show("¿Seguro que desea desactivar el usuario?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Usuario usuario = (Usuario)dgvDatos.SelectedRows[0].DataBoundItem;
                        usuarioLogica.ActivarODesactivar(usuario);
                        btnEliminar.Enabled = false;
                    }else
                    {
                        return;
                    }
                }
                Refrescar();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbVista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategoria.Text == "Todos")
                dgvDatos.DataSource = usuarioLogica.ObtenerUsuarios();
            //if (cmbVista.Text == "Cajeros")
              //  dgvDatos.DataSource = logica.ObtenerCajeros();
            //if (cmbVista.Text == "Clientes")
               // dgvDatos.DataSource = logica.ObtenerClientes();
            //if (cmbVista.Text == "Gerente")
               // dgvDatos.DataSource = logica.ObtenerGerentes();

        }

        private void cellClick(object sender, DataGridViewCellEventArgs e)
        {
            Usuario usuario = (Usuario)dgvDatos.SelectedRows[0].DataBoundItem;
            if (usuario.EstadoActivo)
                btnEliminar.Enabled = true;
            else
                btnEliminar.Enabled = false;
        }

        private void dgvDatos_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            CategoriaLogica log = new CategoriaLogica();

            if (cmbCategoria.Text=="Gerente")
            {
              
                dgvDatos.DataSource = log.ObtenerGerentes();
            }

            if (cmbCategoria.Text == "Todos")
            {

                Refrescar();
            }
            if (cmbCategoria.Text == "Cliente")
            {

                dgvDatos.DataSource = log.ObtenerClientes();
            }
            if (cmbCategoria.Text == "Cajero")
            {

                dgvDatos.DataSource = log.ObtenerCajeros();
            }

        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            try
            {


                FrmDetalleUsuario frm = new FrmDetalleUsuario();

                var resultado = frm.ShowDialog();
                if (resultado == DialogResult.OK)
                    Refrescar();


            }
            catch (Exception ex)
            {

                MessageBox.Show("Hubo un problema: \n" + ex.Message, "Atención!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {




            try
            {
                if (dgvDatos.SelectedRows.Count != 0)
                {
                    Usuario usuario = (Usuario)dgvDatos.SelectedRows[0].DataBoundItem;
                    FrmDetalleUsuario frm = new FrmDetalleUsuario();
                    frm.Usuario = usuario;
                    var resultado = frm.ShowDialog();
                    if (resultado == DialogResult.OK)
                        Refrescar();
                }

            }
            catch (Exception ex)
            {

                throw;
            }







        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDatos.SelectedRows.Count != 0)
                {
                    if (MessageBox.Show("¿Seguro que desea desactivar el usuario?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Usuario usuario = (Usuario)dgvDatos.SelectedRows[0].DataBoundItem;
                        usuarioLogica.ActivarODesactivar(usuario);
                        btnEliminar.Enabled = false;
                    }
                    else
                    {
                        return;
                    }
                }
                Refrescar();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void dgvDatos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count != 0)
            {
                Usuario u = (Usuario)dgvDatos.SelectedRows[0].DataBoundItem;
                if (u.EstadoActivo)
                    btnEliminar.Enabled = true;
                else
                    btnEliminar.Enabled = false;
            }
         }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

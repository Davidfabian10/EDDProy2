using EDDemo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;


//sing GraphVizWrapper;
//using GraphVizWrapper.Queries;
//using GraphVizWrapper.Commands;

//using csdot;
//using csdot.Attributes.DataTypes;

namespace EDDemo.Estructuras_No_Lineales
{
    public partial class frmArboles : Form
    {
        ArbolBusqueda miArbol;
        NodoBinario miRaiz;

        public frmArboles()
        {
            InitializeComponent();
            miArbol = new ArbolBusqueda();
            miRaiz = null;
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {

            //Obtenemos el nodo Raiz del arbol
            miRaiz = miArbol.RegresaRaiz();

            //Limpiamos la cadena donde se concatenan los nodos del arbol 
            miArbol.strArbol = "";

            //Se inserta el nodo con el dato capturado
            miArbol.InsertaNodo(int.Parse(txtDato.Text),
                                ref miRaiz);

            //Leer arbol completo y mostrarlo en caja de texto
            miArbol.MuestraArbolAcostado(1, miRaiz);
            txtArbol.Text = miArbol.strArbol;
            
            txtDato.Text = "";

 
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            miArbol = null;
            miRaiz = null;
            miArbol = new ArbolBusqueda();
            txtArbol.Text  = "";
            txtDato.Text = "";
            lblRecorridoPreOrden.Text = "";
            lblRecorridoInOrden.Text = "";
            lblRecorridoPostOrden.Text = "";
        }

        private void btnGrafica_Click(object sender, EventArgs e)
        {
            String graphVizString;

            miRaiz = miArbol.RegresaRaiz();
            if (miRaiz == null)
            {
                MessageBox.Show("Cree un arbol para graficarlo", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            StringBuilder b = new StringBuilder();
            b.Append("digraph G { node [shape=\"circle\"]; " + Environment.NewLine);
            b.Append(miArbol.ToDot(miRaiz));
            b.Append("}");
            graphVizString =  b.ToString();

            //graphVizString = @" digraph g{ label=""Graph""; labelloc=top;labeljust=left;}";
            //graphVizString = @"digraph Arbol{Raiz->60; 60->40. 60->90; 40->34; 40->50;}";
            Bitmap bm = FileDotEngine.Run(graphVizString);

            
            frmGrafica graf = new frmGrafica();
            graf.ActualizaGrafica(bm);
            graf.MdiParent = this.MdiParent;
            graf.Show();
        }


        private void btnRecorrer_Click(object sender, EventArgs e)
        {
            //Recorrido en PreOrden
            //Obtenemos el nodo Raiz del arbol
            miRaiz = miArbol.RegresaRaiz();
            miArbol.strRecorrido = "";

            if (miRaiz == null)
            {
                lblRecorridoPreOrden.Text = "El arbol esta vacio";
                return;
            }
            lblRecorridoPreOrden.Text = "";
            miArbol.PreOrden(miRaiz);

            lblRecorridoPreOrden.Text = miArbol.strRecorrido;


            //Recorrido en InOrden
            //Obtenemos el nodo Raiz del arbol
            miRaiz = miArbol.RegresaRaiz();
            miArbol.strRecorrido = "";

            if (miRaiz == null)
            {
                lblRecorridoPostOrden.Text = "El arbol esta vacio";
                return;
            }
            lblRecorridoInOrden.Text = "";
            miArbol.InOrden(miRaiz);
            lblRecorridoInOrden.Text = miArbol.strRecorrido;


            //Recorrido en PostOrden
            //Obtenemos el nodo Raiz del arbol
            miRaiz = miArbol.RegresaRaiz();
            miArbol.strRecorrido = "";

            if (miRaiz == null) {
                lblRecorridoPostOrden.Text = "El arbol esta vacio";
                return;
            }
            lblRecorridoPostOrden.Text = ""; 
            miArbol.PostOrden(miRaiz);
            lblRecorridoPostOrden.Text = miArbol.strRecorrido;
        }

        private void btnCrearArbol_Click(object sender, EventArgs e)
        {
            //Limpiamos los objetos y clases del anterior arbol
            miArbol = null;
            miRaiz = null;
            miArbol = new ArbolBusqueda();
            txtArbol.Text = "";
            txtDato.Text = "";
             
            miArbol.strArbol = "";

            Random rnd = new Random();

            for (int nNodos =1; nNodos <= txtNodos.Value; nNodos++)
            {
                int Dato = rnd.Next(1, 100);
                //Obtenemos el nodo Raiz del arbol
                miRaiz = miArbol.RegresaRaiz();

                //Se inserta el nodo con el dato capturado
                miArbol.InsertaNodo(Dato, ref miRaiz);
            }

            //Leer arbol completo y mostrarlo en caja de texto
            miArbol.MuestraArbolAcostado(1, miRaiz);
            txtArbol.Text = miArbol.strArbol;

            txtDato.Text = "";
        }

        private void BtBuscar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtDato.Text, out int elementoBuscar))
            {
                NodoBinario actual = miRaiz;
                bool encontrado = false;

                while (actual != null)
                {
                    if (actual.Dato == elementoBuscar)
                    {
                        encontrado = true;
                        break;
                    }

                    else if (elementoBuscar < actual.Dato)
                    {
                        actual = actual.Izq;
                    }

                    else actual = actual.Der;

                }

                if (encontrado)
                {
                    MessageBox.Show($"El elemento '{elementoBuscar}' si se encuentra en el árbol");
                }

                else MessageBox.Show($"El elemento '{elementoBuscar}' no se encuentra en el árbol");

            }

            else MessageBox.Show("Ingrese un número válido para buscar en el árbol", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void BtPodar_Click(object sender, EventArgs e)
        {
            if (miArbol.EstaVacio())
            {
                MessageBox.Show("Cree un arbol para podarlo", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                miArbol.PodarTodoElArbol();
                MessageBox.Show("El arbol se podo con éxito");
                txtArbol.Text = "";
            }
        }

        private void BtENPredecesor_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtDato.Text, out int valor))
            {
                miRaiz = miArbol.EliminarNodoPredecesor(valor, ref miRaiz);
                miArbol.MuestraArbolAcostado(1, miRaiz);
                MessageBox.Show("El nodo se eliminó con éxito");
                txtArbol.Text = miArbol.strArbol;
            }
            else MessageBox.Show("Ingresa un valor para eliminarlo", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            txtDato.Text = "";
        }

        private void BtENSucesor_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtDato.Text, out int valor))
            {
                miRaiz = miArbol.EliminarNodoSucesor(valor, ref miRaiz);
                miArbol.MuestraArbolAcostado(1, miRaiz);
                MessageBox.Show("El nodo se eliminó con éxito");
                txtArbol.Text = miArbol.strArbol;
            }
            else MessageBox.Show("Ingresa un valor para eliminarlo", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            txtDato.Text = "";
        }

        private void BtRecorrerNiveles_Click(object sender, EventArgs e)
        {
            if (miArbol.EstaVacio())
            {
                MessageBox.Show("El árbol esta vacio, cree un árbol para recorrerlo", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                miRaiz = miArbol.RegresaRaiz();
                miArbol.RecorrerPorNiveles(miRaiz);
                MessageBox.Show("El arbol se ordenó con éxito");
                txtArbol.Text = miArbol.strRecorrido;
            }
        }

        private void BtAltura_Click(object sender, EventArgs e)
        {
            if (miArbol.EstaVacio())
            {
                MessageBox.Show("El árbol esta vacio, cree un árbol para calcular su altura", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                miRaiz = miArbol.RegresaRaiz();
                int altura = miArbol.ObtenerAltura(miRaiz);
                MessageBox.Show("La altura del árbol es: " + altura, "Altura del Árbol");
            }
        }
    } 
}

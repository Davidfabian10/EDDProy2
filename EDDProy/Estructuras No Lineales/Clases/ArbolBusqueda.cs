using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDDemo.Estructuras_No_Lineales
{
    public class ArbolBusqueda
    {
        NodoBinario Raiz;
        public String strArbol;
        public String strRecorrido;

        public ArbolBusqueda()
        {
            Raiz = null;
            strArbol = "";
            strRecorrido = "";
        }

        public Boolean EstaVacio()
        {
            if (Raiz == null)
                return true;
            else
                return false;
        }
        public NodoBinario RegresaRaiz()
        {
            return Raiz;
        }

        public void InsertaNodo(int Dato, ref NodoBinario Nodo)
        {
            if (Nodo == null)
            {
                Nodo = new NodoBinario(Dato);
                // CAMBIO 2

                if (Raiz == null)
                    Raiz = Nodo;
            }
            else if (Dato < Nodo.Dato)
                InsertaNodo(Dato, ref Nodo.Izq);
            else if (Dato > Nodo.Dato)
                InsertaNodo(Dato, ref Nodo.Der);
        }
        public void MuestraArbolAcostado(int nivel, NodoBinario nodo)
        {
            if (nodo == null)
                return;
            MuestraArbolAcostado(nivel + 1, nodo.Der);
            for (int i = 0; i < nivel; i++)
            {
                strArbol = strArbol + "      ";
            }
            strArbol = strArbol + nodo.Dato.ToString() + "\r\n";
            MuestraArbolAcostado(nivel + 1, nodo.Izq);
        }

        public String ToDot(NodoBinario nodo)
        {
            StringBuilder b = new StringBuilder();
            if (nodo.Izq != null)
            {
                b.AppendFormat("{0}->{1} [side=L] {2} ", nodo.Dato.ToString(), nodo.Izq.Dato.ToString(), Environment.NewLine);
                b.Append(ToDot(nodo.Izq));
            }

            if (nodo.Der != null)
            {
                b.AppendFormat("{0}->{1} [side=R] {2} ", nodo.Dato.ToString(), nodo.Der.Dato.ToString(), Environment.NewLine);
                b.Append(ToDot(nodo.Der));
            }
            return b.ToString();
        }

        public void PreOrden(NodoBinario nodo)
        {
            if (nodo == null)
                return;

            strRecorrido = strRecorrido + nodo.Dato + ", ";
            PreOrden(nodo.Izq);
            PreOrden(nodo.Der);

            return;
        }

        public void InOrden(NodoBinario nodo)
        {
            if (nodo == null)
                return;

            InOrden(nodo.Izq);
            strRecorrido = strRecorrido + nodo.Dato + ", ";
            InOrden(nodo.Der);

            return;
        }
        public void PostOrden(NodoBinario nodo)
        {
            if (nodo == null)
                return;

            PostOrden(nodo.Izq);
            PostOrden(nodo.Der);
            strRecorrido = strRecorrido + nodo.Dato + ", ";

            return;
        }

        public void PodarArbol(ref NodoBinario nodo)
        {
            if (nodo == null) return;
            PodarArbol(ref nodo.Izq);
            PodarArbol(ref nodo.Der);
            nodo = null;  // Elimina el nodo actual
        }

        // Llamada para podar todo el árbol desde la raíz
        public void PodarTodoElArbol()
        {
            PodarArbol(ref Raiz);
            Raiz = null; // Reiniciar la raíz después de la poda
        }

        public NodoBinario EliminarNodoPredecesor(int valor, ref NodoBinario nodo)
        {
            if (nodo == null) return null;

            if (valor < nodo.Dato)
            {
                nodo.Izq = EliminarNodoPredecesor(valor, ref nodo.Izq);
            }
            else if (valor > nodo.Dato)
            {
                nodo.Der = EliminarNodoPredecesor(valor, ref nodo.Der);
            }
            else
            {
                if (nodo.Izq != null && nodo.Der != null)
                {
                    NodoBinario predecesor = EncontrarMaximo(nodo.Izq);
                    nodo.Dato = predecesor.Dato;
                    nodo.Izq = EliminarNodoPredecesor(predecesor.Dato, ref nodo.Izq);
                }
                else
                {
                    nodo = (nodo.Izq != null) ? nodo.Izq : nodo.Der;
                }
            }
            return nodo;
        }

        public NodoBinario EliminarNodoSucesor(int valor, ref NodoBinario nodo)
        {
            if (nodo == null) return null;

            if (valor < nodo.Dato)
            {
                nodo.Izq = EliminarNodoSucesor(valor, ref nodo.Izq);
            }
            else if (valor > nodo.Dato)
            {
                nodo.Der = EliminarNodoSucesor(valor, ref nodo.Der);
            }
            else
            {
                if (nodo.Izq != null && nodo.Der != null)
                {
                    NodoBinario sucesor = EncontrarMinimo(nodo.Der);
                    nodo.Dato = sucesor.Dato;
                    nodo.Der = EliminarNodoSucesor(sucesor.Dato, ref nodo.Der);
                }
                else
                {
                    nodo = (nodo.Izq != null) ? nodo.Izq : nodo.Der;
                }
            }
            return nodo;
        }

        private NodoBinario EncontrarMaximo(NodoBinario nodo)
        {
            while (nodo.Der != null)
            {
                nodo = nodo.Der;
            }
            return nodo;
        }

        private NodoBinario EncontrarMinimo(NodoBinario nodo)
        {
            while (nodo.Izq != null)
            {
                nodo = nodo.Izq;
            }
            return nodo;
        }

        public void RecorrerPorNiveles(NodoBinario nodo)
        {
            if (nodo == null)
            {
                strRecorrido = "El árbol está vacío";
                return;
            }

            // Inicializar una cola para manejar el recorrido por niveles
            Queue<NodoBinario> cola = new Queue<NodoBinario>();
            cola.Enqueue(nodo); // Empezar con la raíz

            strRecorrido = ""; // Limpiar la cadena de recorrido antes de comenzar

            while (cola.Count > 0)
            {
                // Sacar el nodo al frente de la cola
                NodoBinario actual = cola.Dequeue();

                // Agregar el valor del nodo actual a la cadena de recorrido
                strRecorrido += actual.Dato + ", ";

                // Agregar los hijos izquierdo y derecho del nodo actual a la cola
                if (actual.Izq != null)
                {
                    cola.Enqueue(actual.Izq);
                }
                if (actual.Der != null)
                {
                    cola.Enqueue(actual.Der);
                }
            }
        }
    }
}
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
            nodo = null;
        }

        public void PodarTodoElArbol()
        {
            PodarArbol(ref Raiz);
            Raiz = null;
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

            Queue<NodoBinario> cola = new Queue<NodoBinario>();
            cola.Enqueue(nodo);

            strRecorrido = ""; 

            while (cola.Count > 0)
            {
                NodoBinario actual = cola.Dequeue();

                strRecorrido += actual.Dato + ", ";

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

        public int ObtenerAltura(NodoBinario nodo)
        {
            if (nodo == null)
                return -1; 

            int alturaIzquierda = ObtenerAltura(nodo.Izq);
            int alturaDerecha = ObtenerAltura(nodo.Der);

            return 1 + Math.Max(alturaIzquierda, alturaDerecha);
        }

        public int ContarHojas(NodoBinario nodo)
        {
            if (nodo == null)
                return 0;

            if (nodo.Izq == null && nodo.Der == null)
                return 1;

            return ContarHojas(nodo.Izq) + ContarHojas(nodo.Der);
        }

        public int ContarNodos(NodoBinario nodo)
        {
            if (nodo == null)
                return 0;

            return 1 + ContarNodos(nodo.Izq) + ContarNodos(nodo.Der);
        }

        public bool EsCompleto(NodoBinario nodo)
        {
            if (nodo == null)
                return true;

            Queue<NodoBinario> cola = new Queue<NodoBinario>();
            cola.Enqueue(nodo);

            bool encontradoNodoNoCompleto = false;

            while (cola.Count > 0)
            {
                NodoBinario actual = cola.Dequeue();

                if (actual.Izq != null)
                {
                    if (encontradoNodoNoCompleto) 
                        return false;
                    cola.Enqueue(actual.Izq);
                }
                else encontradoNodoNoCompleto = true;

                if (actual.Der != null)
                {
                    if (encontradoNodoNoCompleto) 
                        return false;
                    cola.Enqueue(actual.Der);
                }

                else encontradoNodoNoCompleto = true;
   
            }

            return true;
        }

        public bool EsLleno(NodoBinario nodo)
        {
            if (nodo == null)
                return true;

            if (nodo.Izq == null && nodo.Der == null)
                return true;

            if (nodo.Izq != null && nodo.Der != null)
                return EsLleno(nodo.Izq) && EsLleno(nodo.Der);

            return false;
        }
    }
}
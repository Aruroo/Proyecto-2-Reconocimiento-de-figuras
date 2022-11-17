
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System;


namespace ReconocimientoFiguras
{
    public class Pixel

    {

        private int x;

        private int y;

        private Color color;
        
        /// <summary>
        /// Constructor de la clase Pixel
        /// </summary>
        /// <param name="x">Coordenada x del pixel</param>
        /// <param name="y">Coordenada y del pixel</param>
        /// <param name="color">Color del pixel</param>
        public Pixel(int x, int y, Color color)
        {

            this.x = x;

            this.y = y;

            this.color = color;

        }

        public Color getColor(){
            return color;
        }

        /// <summary>
        /// Comparador de igualdad entre dos pixeles
        /// </summary>
        /// <param name="obj">Pixel a comparar</param>
        /// <returns>True si son iguales, false si no lo son</returns>

        public bool mismoColor(Pixel pixel)
        {

            return this.color == pixel.color;

        }

        public int getX(){
            return x;
        }

        public int getY(){
            return y;
        }
    }

    public class Figura
    {

        private List<Pixel> pixeles;

        /// <summary>
        /// Constructor de la clase Figura
        /// </summary>
        /// <param name="pixeles">Lista de pixeles que conforman la figura</param>
        ///Suponemos que todos los pixeles de la lista son del mismo color.
        public Figura(List<Pixel> pixeles)
        {
            this.pixeles = pixeles;
        }
        /// <summary>
        /// Obtiene el color de la figura
        /// </summary>
        public Color getColor()
        {
            return pixeles[0].getColor();
        }

        ///<summary>
        ///agrega el pixel dado a esta figura
        ///</summary>
        public void agregaPixel(Pixel pixel)
        {
            pixeles.Add(pixel);
        }

        public string toString()
        {
            return "Figura de color " + getColor() + " con " + pixeles.Count + " pixeles";
        }

        ///<summary>
        ///encuentra el centro de la figura
        ///</summary>
        ///<returns> el Pixel del centro de la figura </returns>
        public Pixel encuentraCentro()
        {
            if(pixeles.Count == 0)
            {
                return null;
            } else
            {
                Pixel primerPixel = pixeles[0];
                Pixel ultimoPixel = pixeles[pixeles.Count -1];  
                Pixel centro =new Pixel((primerPixel.getX() + ultimoPixel.getX())/2, (primerPixel.getY() + ultimoPixel.getY())/2, primerPixel.getColor());
                //Buscamos el centro en la lista de pixeles
                foreach(Pixel pixel in pixeles){
                    if(pixel.getX() == centro.getX() && pixel.getY() == centro.getY()){
                    return pixel;
                    }
                }
            }

            return centro;
            
        }

        ///<summary>
        ///algoritmo que traza lineas desde el centro 
        ///de la figura hasta los pixeles que la conforman, obteniendo los
        ///puntos máximos de la figura, que serán interpretados como los vértices
        ///</summary>
        ///<returns> una lista de pixeles que representan los vértices de la figura </returns>
        public List<Pixel> encuentraVertices()
        {
            List<Pixel> vertices = new List<Pixel>();
            Pixel centro = encuentraCentro();
            
            return vertices;
        }
    
        ///<summary>
        ///algoritmo que determina que figura es de acuerdo a su número de vértices
        ///</summary>
        ///<returns> una cadena con el nombre de la figura:
        ///Triangulo, Cuadrilatero, Circulo u otro
        ///</returns>
        public string determinaFigura()
        {
            int numeroVertices = encuentraVertices().Count;
            if(numeroVertices == 3)
            {
                return "Triangulo";
            } else if(numeroVertices == 4)
            {
                return "Cuadrilatero";
            } else if(numeroVertices == 0)
            {
                return "Circulo";
            } else
            {
                return "Otro";
            }

        }
    }

    public class ProcesadorImagen
    {
        private Bitmap imagen;

        public ProcesadorImagen(Bitmap imagen)
        {
            if (imagen == null)
            {
                throw new ArgumentNullException("imagen");
            }
            this.imagen = imagen;

        }

        /// <summary>
        /// Procesa la imagen y obtiene las figuras presentes en la imagen
        /// Se presupone que cada figura tiene un color diferente
        /// </summary>
        /// <param name="imagen">Imagen a procesar</param>
        /// <returns>Una lista con las figuras presentes en la imagen</returns>
        public List <Figura> RecorreImagen(){
            //el color del fondo es el primer pixel en la imagen
            Pixel fondo = new Pixel(0,0,imagen.GetPixel(0,0));
            List <Figura> figuras = new List<Figura>();

            //recorremos la imagen como un arreglo bidiemnsional
            for (int i = 0; i < imagen.Width; i++)
            {
                for (int j = 0; j < imagen.Height; j++)
                {
                    //si el pixel no es del color del fondo, lo agregamos a una figura
                    if (!imagen.GetPixel(i,j).Equals(fondo.getColor()))
                    {
                        anexaPixel(figuras, new Pixel(i,j,imagen.GetPixel(i,j)));
                    }
                }
            }

            return figuras;

        }
        /// <summary>
        /// anexa el pixel a una figura en la lista dada.
        /// si no existe una figura con si mismo color, la crea y 
        /// la anexa a la lista. 
        /// </summary>
        /// <param name="pixel">Pixel a anexar</param>
        /// <param name="figuras">Lista de figuras</param>
        private void anexaPixel(List<Figura> figuras, Pixel pixel)
        {
            bool agregado = false;

            foreach (Figura figura in figuras)
            {
                if(figura.getColor().Equals(pixel.getColor()))
                {
                    figura.agregaPixel(pixel);
                    agregado = true;
                    return;
                }
            }
            if(!agregado)
            {
                List <Pixel> pixeles = new List<Pixel>();
                pixeles.Add(pixel);
                Figura figura = new Figura(pixeles);
                figuras.Add(figura);
            }
        }      
    }

    public class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Introduce la dirección de la imagen");
            string nombreImagen = Console.ReadLine();
            Bitmap imagen = new Bitmap(nombreImagen);
            ProcesadorImagen procesador = new ProcesadorImagen(imagen);

            Console.WriteLine("La imagen tiene {0} pixeles de ancho y {1} pixeles de largo", imagen.Width, imagen.Height);
            List<Figura> figuras = procesador.RecorreImagen();
            foreach (Figura figura in figuras)
            {
                Console.WriteLine(figura.toString());
                Pixel centro = figura.encuentraCentro();
                Console.WriteLine("Centro: " + centro.getX() + ", " + centro.getY());
            }
        }
    }

} 
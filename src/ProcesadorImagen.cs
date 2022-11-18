
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
        
        /// <summary>
        /// Devuelve el color del pixel
        /// </summary>
        public Color ObtenColor(){
            return color;
        }

        /// <summary>
        /// Devuelve la coordenada x del pixel
        /// </summary>
        public int ObtenX(){
            return x;
        }

        /// <summary>
        /// Devuelve la coordenada y del pixel
        /// </summary>
        public int ObtenY(){
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
        public Color ObtenColor()
        {
            return pixeles[0].ObtenColor();
        }

        ///<summary>
        ///agrega el pixel dado a esta figura
        ///</summary>
        public void AgregaPixel(Pixel pixel)
        {
            pixeles.Add(pixel);
        }

        /// <summary>
        /// Obtiene una lista con los pixeles de la figura
        /// </summary>
        public List <Pixel> ObtenPixeles() {
            return pixeles;
        }
        
        /// <summary>
        ///Representa a la imagen con un string
        /// </summary>
        public string AString()
        {
            return "Figura de color " + ObtenColor() + " con " + pixeles.Count + " pixeles";
        }

        ///<summary>
        ///encuentra el centro de la figura
        ///</summary>
        ///<returns> el Pixel del centro de la figura </returns>
        public Pixel EncuentraCentro()
        {
            if(pixeles.Count == 0)
            {
                return null;
            } else{
                Pixel primerPixel = pixeles[0];
                Pixel ultimoPixel = pixeles[pixeles.Count -1];  
                Pixel centro =new Pixel((primerPixel.ObtenX() + ultimoPixel.ObtenX())/2, (primerPixel.ObtenY() + ultimoPixel.ObtenY())/2, primerPixel.ObtenColor());
                //Buscamos el centro en la lista de pixeles
                foreach(Pixel pixel in pixeles){
                    if(pixel.ObtenX() == centro.ObtenX() && pixel.ObtenY() == centro.ObtenY()){
                    return pixel;
                    }
                }
                return centro;
            }
        }

        ///<summary>
        ///algoritmo que traza lineas desde el centro 
        ///de la figura hasta los pixeles que la conforman, obteniendo los
        ///puntos máximos de la figura, que serán interpretados como los vértices
        ///</summary>
        ///<returns> una lista de pixeles que representan los vértices de la figura </returns>
        public List<Pixel> EncuentraVertices()
        {
            List<Pixel> vertices = new List<Pixel>();
            Pixel centro = EncuentraCentro();
            
            return vertices;
        }
    
        ///<summary>
        ///algoritmo que determina que figura es de acuerdo a su número de vértices
        ///</summary>
        ///<returns> una cadena con el nombre de la figura:
        ///Triangulo, Cuadrilatero, Circulo u otro
        ///</returns>
        public string DeterminaFigura()
        {
            int numeroVertices = EncuentraVertices().Count;
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
        private Color fondo;

        /// <summary>
        /// Constructor de la clase ProcesadorImagen
        /// </summary>
        /// <param name="imagen">Imagen a procesar</param>
        public ProcesadorImagen(Bitmap imagen)
        {
            if (imagen == null)
            {
                throw new ArgumentNullException("imagen");
            }
            this.imagen = imagen;
            this.fondo = imagen.GetPixel(0, 0);

        }

        ///
        ///<summary>
        /// Obtiene los bordes de una figura dada.
        ///</summary>
        ///<param name= "figura"> La figura a analizar</param>
        private List<Pixel> ObtenBordes(Figura figura)
        {
            List <Pixel> pixeles = figura.ObtenPixeles();
            List <Pixel> bordes = new List<Pixel>();

            foreach (Pixel pixel in pixeles)
            {
                if(VecinoEnFondo(pixel.ObtenX(), pixel.ObtenY())){
                    bordes.Add(pixel);
                } 
            }
            return bordes;
        }

        /// <summary>
        /// Checa que si alguno de sus Pixeles vecinos sea del color de fondo
        /// </summary>
        /// <param name="x">Coordenada x del pixel</param>
        /// <param name="y">Coordenada y del pixel</param>
        /// <returns>True si alguno de sus pixeles vecinos es del color de fondo</returns>
        private bool VecinoEnFondo(int x, int y)
        {
            try
            {
                return (EsFondo(x-1,y-1) || EsFondo(x-1,y) || EsFondo(x-1,y+1) ||
                    EsFondo(x,y-1)  || EsFondo(x,y+1) || EsFondo(x+1, y-1) ||
                    EsFondo (x+1, y) || EsFondo(x+1, y+1));
            }
            catch (System.ArgumentOutOfRangeException)
            {
                return true;
            }
        }
        
        ///<summary>
        /// Determina si el pixel dado es del color de fondo 
        ///</summary>
        /// <param name="x">Coordenada x del pixel</param>
        /// <param name="y">Coordenada y del pixel</param>
        /// <returns>True si el pixel es del color de fondo</returns>
        private bool EsFondo(int x, int y){
            return imagen.GetPixel(x,y).Equals(fondo);
        }

        /// <summary>
        /// Procesa la imagen y obtiene las figuras presentes en la imagen
        /// Se presupone que cada figura tiene un color diferente
        /// </summary>
        /// <param name="imagen">Imagen a procesar</param>
        /// <returns>Una lista con las figuras presentes en la imagen</returns>
        public List <Figura> RecorreImagen(){
            //el color del fondo es el primer pixel en la imagen
            Pixel fondoPixel = new Pixel(0,0,imagen.GetPixel(0,0));
            List <Figura> figuras = new List<Figura>();
            for (int i = 0; i < imagen.Width; i++)
            {
                for (int j = 0; j < imagen.Height; j++)
                {
                    if (!imagen.GetPixel(i,j).Equals(fondoPixel.ObtenColor()))
                    {
                        AnexaPixel(figuras, new Pixel(i,j,imagen.GetPixel(i,j)));
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
        private void AnexaPixel(List<Figura> figuras, Pixel pixel)
        {
            bool agregado = false;

            foreach (Figura figura in figuras)
            {
                if(figura.ObtenColor().Equals(pixel.ObtenColor()))
                {
                    figura.AgregaPixel(pixel);
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
            Console.WriteLine("La imagen tiene {0} pixeles de ancho y {1} pixeles de alto", imagen.Width, imagen.Height);
            List<Figura> figuras = procesador.RecorreImagen();
            foreach (Figura figura in figuras)
            {
                Console.WriteLine(figura.AString());
                Pixel centro = figura.EncuentraCentro();
                Console.WriteLine("Centro: " + centro.ObtenX() + ", " + centro.ObtenY());
            }
        }
    }

} 
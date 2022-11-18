
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReconocimientoFiguras {
    public class Pixel {

        private int x;
        private int y;
        private Color color;
        
        /// <summary>
        /// Constructor de la clase Pixel.
        /// </summary>
        /// <param name = "x"> Coordenada x del pixel. </param>
        /// <param name = "y"> Coordenada y del pixel. </param>
        /// <param name = "color"> Color del pixel. </param>
        public Pixel(int x, int y, Color color){
            this.x = x;
            this.y = y;
            this.color = color;
        }
        
        /// <summary>
        /// Devuelve el color del pixel.
        /// </summary>
        public Color ObtenColor(){
            return color;
        }

        /// <summary>
        /// Devuelve la coordenada x del pixel.
        /// </summary>
        public int ObtenX(){
            return x;
        }

        /// <summary>
        /// Devuelve la coordenada y del pixel.
        /// </summary>
        public int ObtenY(){
            return y;
        }
    }

    public class Figura {

        private List<Pixel> pixeles;
        private Bitmap imagen;
        private Color color;

        /// <summary>
        /// Constructor de la clase Figura.
        /// </summary>
        /// <param name = "pixeles"> Lista de pixeles que conforman la figura. </param>
        /// Suponemos que todos los pixeles de la lista son del mismo color.
        public Figura(List<Pixel> pixeles, Bitmap imagen){
            this.pixeles = pixeles;
            this.imagen = imagen;
            this.color = pixeles[0].ObtenColor();
        }

        /// <summary>
        /// Obtiene los pixeles de esta figura.
        /// </summary>
        public List<Pixel> ObtenPixeles(){
            return pixeles;
        }

        /// <summary>
        /// Obtiene el color de la figura.
        /// </summary>
        /// <returns> El color de esta figura. </returns>
        public Color ObtenColor(){
            return color;
        }

        /// <summary>
        /// Agrega el pixel dado a esta figura.
        /// </summary>
        public void AgregaPixel(Pixel pixel){
            pixeles.Add(pixel);
        }
        
        /// <summary>
        /// Representa a la imagen con un string.
        /// </summary>
        /// <returns> La imagen representada como un string. </returns>
        public string AString(){
            return "Figura de color " + ObtenColor() + " con " + pixeles.Count + " pixeles";
        }

        /// <summary>
        /// Encuentra el centro de la figura.
        /// </summary>
        /// <returns> El Pixel del centro de la figura. </returns>
        public Pixel EncuentraCentro()
        {
            if(pixeles.Count == 0)
            {
                return null;
            } else{
                Pixel primerPixel = pixeles[0];
                Pixel ultimoPixel = pixeles[pixeles.Count - 1];  
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

        /// <summary>
        /// Algoritmo que obtiene la cantidad de máximos que hay entre las distancias desde
        /// el centro de la figura hasta los puntos del borde. La cantidad de máximos serán
        /// interpretados como los vértices.
        /// </summary>
        /// <returns> Cantidad de máximos, o bien, el número de vértices de esta figura. </returns>
        public int EncuentraVertices()
        {
            List<Pixel> vertices = new List<Pixel>();
            List<double> distancias = ObtenDistancias();
            int maximos = 0;
            for(int i = 1; i < distancias.Count; i++)
            {
                if((distancias[i] > distancias[i - 1]) && (distancias[i] > distancias[i + 1]))
                {
                    maximos++;
                }
            }
            return maximos;
        }

        /// <summary>
        /// Obtiene las distancias entre los puntos que están en el borde y el centro de la
        /// figura para calcular la cantidad de máximos que hay.
        /// </summary>
        /// <returns> Lista de distancias. </returns>
        private List<double> ObtenDistancias()
        {
            List<double> distancias = new List<int>();
            Pixel centro = EncuentraCentro();
            foreach (Pixel pixel in ObtenBordes())
            {
                distancias.Add(Math.Sqrt(Math.Pow(pixel.ObtenX() - centro.ObtenX(), 2) + Math.Pow(pixel.ObtenY() - centro.ObtenY(), 2)));
            }
            return distancias;
        }

        /// <summary>
        /// Obtiene los bordes de esta figura.
        /// </summary>
        /// <returns> Lista de pixeles que representan los bordes de la figura. </returns>
        private List<Pixel> ObtenBordes()
        {
            List<Pixel> bordes = new List<Pixel>();

            foreach (Pixel pixel in pixeles)
            {
                if(VecinoEnFondo(pixel.ObtenX(), pixel.ObtenY())){
                    bordes.Add(pixel);
                } 
            }
            return bordes;
        }
    
        /// <summary>
        /// Checa que si alguno de sus Pixeles vecinos sea del color de fondo.
        /// </summary>
        /// <param name = "x"> Coordenada x del pixel. </param>
        /// <param name = "y"> Coordenada y del pixel. </param>
        /// <returns> True si alguno de sus pixeles vecinos es del color de fondo. </returns>
        public bool VecinoEnFondo(int x, int y)
        {
            try{
                return (EsFondo(x-1, y-1) || EsFondo(x-1, y) || EsFondo(x-1, y+1) ||
                    EsFondo(x, y-1)  || EsFondo(x, y+1) || EsFondo(x+1, y-1) ||
                    EsFondo (x+1, y) || EsFondo(x+1, y+1));
            }
            catch (System.ArgumentOutOfRangeException){
                return true;
            }
        }
        
        /// <summary>
        /// Determina si el pixel dado es del color de fondo.
        /// </summary>
        /// <param name = "x"> Coordenada x del pixel. </param>
        /// <param name = "y"> Coordenada y del pixel. </param>
        /// <returns> True si el pixel es del color de fondo. </returns>
        private bool EsFondo(int x, int y){
            return imagen.GetPixel(x, y).Equals(imagen.GetPixel(0, 0));
        }

        /// <summary>
        /// Algoritmo que determina que figura es de acuerdo a su número de vértices.
        /// </summary>
        /// <returns>
        /// Una cadena con el nombre de la figura:
        /// Triangulo, Cuadrilatero, Circulo u otro.
        /// </returns>
        public string DeterminaFigura()
        {
            int numeroVertices = EncuentraVertices();
            if(numeroVertices == 3)
            {
                return "Triangulo";
            } else if(numeroVertices == 4)
            {
                return "Cuadrilatero";
            } else if(numeroVertices == 0 || numeroVertices > 12)
            {
                return "Circulo";
            } else
            {
                return "Otro";
            }

        }
    }

    public class ProcesadorImagen {
        private Bitmap imagen;
        private List<Figura> figuras;

        /// <summary>
        /// Constructor de la clase ProcesadorImagen.
        /// </summary>
        /// <param name = "imagen"> Imagen a procesar. </param>
        public ProcesadorImagen(Bitmap imagen){
            if (imagen == null)
            {
                throw new ArgumentNullException("imagen");
            }
            this.imagen = imagen;
            this.figuras = RecorreImagen();
        }

        /// <summary>
        /// Procesa la imagen y obtiene las figuras presentes en la imagen.
        /// Se presupone que cada figura tiene un color diferente.
        /// </summary>
        /// <returns> Una lista con las figuras presentes en la imagen. </returns>
        public List<Figura> RecorreImagen(){
            //El color del fondo es el primer pixel en la imagen.
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
        /// Anexa el pixel a una figura en la lista dada.
        /// Si no existe una figura con si mismo color, la crea y la anexa a la lista. 
        /// </summary>
        /// <param name = "pixel"> Pixel a anexar. </param>
        /// <param name = "figuras"> Lista de figuras. </param>
        public void AnexaPixel(List<Figura> figuras, Pixel pixel)
        {
            bool agregado = false;

            if (figuras.Count != 0)
            {
                foreach (Figura figura in figuras)
                {
                    if(figura.ObtenColor().Equals(pixel.ObtenColor()))
                    {
                        figura.AgregaPixel(pixel);
                        agregado = true;
                        return;
                    }
                }
            }
            if(!agregado)
            {
                List <Pixel> pixeles = new List<Pixel>();
                pixeles.Add(pixel);
                Figura figura = new Figura(pixeles, imagen);
                figuras.Add(figura);
            }
        }      

        /// <summary>
        /// Representa a la imagen como una cadena en el cual muestra qué tipo de figuras
        /// hay con sus respectivos colores.
        /// </summary>
        /// <returns> La cadena con la información de la imagen. </returns>
        public string ToString() {
            foreach (Figura figura in figuras)
            {
                "Color: " + figura.ObtenColor().ToArgb() + " Figura: " + figura.DeterminaFigura() + "\n";
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

    [TestClass]
    public class PruebaPixel {

        [TestMethod]
        public void PruebaConstructorPixel() {
            Pixel pixel = new Pixel(1, 2, Color.Red);
            Assert.AreEqual(1, pixel.ObtenX());
            Assert.AreEqual(2, pixel.ObtenY());
            Assert.AreEqual(Color.Red, pixel.ObtenColor());
        }
    }

    [TestClass]
    public class PruebaFigura {

        [TestMethod]
        public void PruebaConstructorFigura() {
            List<Pixel> pixeles = new List<Pixel>();
            pixeles.Add(new Pixel(1, 2, Color.Red));
            pixeles.Add(new Pixel(2, 3, Color.Red));
            pixeles.Add(new Pixel(3, 4, Color.Red));
            Bitmap imagen = new Bitmap(5, 5);
            Figura figura = new Figura(pixeles, imagen);
            Assert.AreEqual(Color.Red, figura.ObtenColor());
        }

        [TestMethod]
        public void PruebaEncuentraCentro() {
            List<Pixel> pixeles = new List<Pixel>();
            pixeles.Add(new Pixel(1, 2, Color.Red));
            pixeles.Add(new Pixel(2, 3, Color.Red));
            pixeles.Add(new Pixel(3, 4, Color.Red));
            pixeles.Add(new Pixel(4, 5, Color.Red));
            Bitmap imagen = new Bitmap(5, 5);
            Figura figura = new Figura(pixeles, imagen);
            Assert.AreEqual(3, figura.EncuentraCentro().ObtenX());
            Assert.AreEqual(3, figura.EncuentraCentro().ObtenY());
        }

        [TestMethod]
        public void PruebaAgregaPixel() {
            List<Pixel> pixeles = new List<Pixel>();
            pixeles.Add(new Pixel(1, 2, Color.Red));
            Bitmap imagen = new Bitmap(5, 5);
            Figura figura = new Figura(pixeles, imagen);
            figura.AgregaPixel(new Pixel(2, 3, Color.Red));
            Assert.AreEqual(2, figura.ObtenPixeles().Count);
            figura.AgregaPixel(new Pixel(3, 4, Color.Red));
            Assert.AreEqual(3, figura.ObtenPixeles().Count);
            figura.AgregaPixel(new Pixel(4, 5, Color.Red));
            Assert.AreEqual(4, figura.ObtenPixeles().Count);
        }
    }

    [TestClass]
    public class PruebaProcesadorImagen {

        [TestMethod]
        public void PruebaRecorreImagen() {
            Bitmap imagen = new Bitmap(5, 5);
            ProcesadorImagen procesador = new ProcesadorImagen(imagen);
            List<Figura> figuras = procesador.RecorreImagen();
            Assert.AreEqual(1, figuras.Count);
        }

        [TestMethod]
        public void PruebaAnexaPixel() {
            Bitmap imagen = new Bitmap(5, 5);
            ProcesadorImagen procesador = new ProcesadorImagen(imagen);
            List<Figura> figuras = procesador.RecorreImagen();
            procesador.AnexaPixel(figuras, new Pixel(1, 2, Color.Red));
            Assert.AreEqual(2, figuras.Count);
        }
    }
} 
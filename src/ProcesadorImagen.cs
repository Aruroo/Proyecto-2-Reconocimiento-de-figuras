
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System;

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
        private int EncuentraVertices()
        {
           List <double> distancias = ObtenDistancias();

           int maximos = 0;

           for(int i = 2; i < distancias.Count - 2; i++)
           {
            //comparamos el actual con los dos anteriores y los dos siguientes
            if(distancias[i] > distancias[i - 1] && distancias[i] > distancias[i - 2]
                 && distancias[i] > distancias[i + 1] && distancias[i] > distancias[i + 2])
            {
                maximos++;
            }  
           }
           //realizamos las comparaciones que faltan:
           if(distancias.Count >3){
            //para i = 0
            if(distancias[0]> distancias[distancias.Count-1] && distancias[0] > distancias[distancias.Count-2]
                && distancias[0] > distancias[1] && distancias[0] > distancias[2])
            {
                maximos++;
            }
            //para i = 1
            if(distancias[1]> distancias[0] && distancias[1] > distancias[distancias.Count-1]
                && distancias[1] > distancias[2] && distancias[1] > distancias[3])
            {
                maximos++;
            }
            //para i = distancias.Count - 1
            if(distancias[distancias.Count - 1]> distancias[distancias.Count - 2] && distancias[distancias.Count - 1] > distancias[distancias.Count - 3]
                && distancias[distancias.Count - 1] > distancias[0] && distancias[distancias.Count - 1] > distancias[1])
            {
                maximos++;
            }
            //para i = distancias.Count - 2
            if(distancias[distancias.Count - 2]> distancias[distancias.Count - 3] && distancias[distancias.Count - 2] > distancias[distancias.Count - 4]
                && distancias[distancias.Count - 2] > distancias[distancias.Count - 1] && distancias[distancias.Count - 2] > distancias[0])
            {
                maximos++;
            }

           }

           return maximos;

        }

        

        /// <summary>
        /// Obtiene las distancias entre los puntos que están en el borde y el centro de la
        /// figura.
        /// </summary>
        /// <returns> Lista de listas de distancias (para preservar un orden). </returns>
        public List<double> ObtenDistancias()
        {
            List<double> distancias = new List<double>();
            Pixel centro = EncuentraCentro();
            List<Pixel> bordes = ObtenBordes();
            foreach (Pixel pixel in bordes)
            {
                distancias.Add(Math.Sqrt(Math.Pow(pixel.ObtenX() - centro.ObtenX(), 2)
                                + Math.Pow(pixel.ObtenY() - centro.ObtenY(), 2)));
            
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
            bordes.Add(pixeles[0]);
            Pixel siguiente = ObtenSiguienteBorde(bordes, this.pixeles[0]);
            while (siguiente != null){
                bordes.Add(siguiente);
                siguiente = ObtenSiguienteBorde(bordes, siguiente);
            }
        
            return bordes;
        }


        /// <summary>
        /// Obtiene el siguiente pixel en el borde que no se ha agregado a una lista dada.
        /// Suponemos que el pixel es borde.
        /// </summary>
        /// <returns> El siguiente pixel en el borde (Null si no hay). </returns>
        private Pixel ObtenSiguienteBorde(List<Pixel> bordes, Pixel pixel){

            //Probamos primero con el pixel de arriba
            Pixel pixelArriba = new Pixel(pixel.ObtenX(), pixel.ObtenY() - 1, pixel.ObtenColor());
            if(!YaAgregado(bordes, pixelArriba) && EsBorde(pixelArriba.ObtenX(), pixelArriba.ObtenY())){
                return pixelArriba;
            }

             //Probamos con el pixel de arriba a la derecha
            Pixel pixelArribaDerecha = new Pixel(pixel.ObtenX() + 1, pixel.ObtenY() - 1, pixel.ObtenColor());
            if(!YaAgregado(bordes, pixelArribaDerecha) && EsBorde(pixelArribaDerecha.ObtenX(), pixelArribaDerecha.ObtenY())){
                return pixelArribaDerecha;
            }

            //Probamos con el pixel de la derecha
            Pixel pixelDerecha = new Pixel(pixel.ObtenX() + 1, pixel.ObtenY(), pixel.ObtenColor());
            if(!YaAgregado(bordes, pixelDerecha) && EsBorde(pixelDerecha.ObtenX(), pixelDerecha.ObtenY())){
                return pixelDerecha;
            }

            //Probamos con el pixel de abajo a la derecha
            Pixel pixelAbajoDerecha = new Pixel(pixel.ObtenX() + 1, pixel.ObtenY() + 1, pixel.ObtenColor());
            if(!YaAgregado(bordes, pixelAbajoDerecha) && EsBorde(pixelAbajoDerecha.ObtenX(), pixelAbajoDerecha.ObtenY())){
                return pixelAbajoDerecha;
            }

            //Probamos con el pixel de abajo
            Pixel pixelAbajo = new Pixel(pixel.ObtenX(), pixel.ObtenY() + 1, pixel.ObtenColor());
            if(!YaAgregado(bordes, pixelAbajo) && EsBorde(pixelAbajo.ObtenX(), pixelAbajo.ObtenY())){
                return pixelAbajo;
            }

            //Probamos con el pixel de abajo a la izquierda
            Pixel pixelAbajoIzquierda = new Pixel(pixel.ObtenX() - 1, pixel.ObtenY() + 1, pixel.ObtenColor());
            if(!YaAgregado(bordes, pixelAbajoIzquierda) && EsBorde(pixelAbajoIzquierda.ObtenX(), pixelAbajoIzquierda.ObtenY())){
                return pixelAbajoIzquierda;
            }

            //Probamos con el pixel de la izquierda
            Pixel pixelIzquierda = new Pixel(pixel.ObtenX() - 1, pixel.ObtenY(), pixel.ObtenColor());
            if(!YaAgregado(bordes, pixelIzquierda) && EsBorde(pixelIzquierda.ObtenX(), pixelIzquierda.ObtenY())){
                return pixelIzquierda;
            }

            //Probamos con el pixel de arriba a la izquierda
            Pixel pixelArribaIzquierda = new Pixel(pixel.ObtenX() - 1, pixel.ObtenY() - 1, pixel.ObtenColor());
            if(!YaAgregado(bordes, pixelArribaIzquierda) && EsBorde(pixelArribaIzquierda.ObtenX(), pixelArribaIzquierda.ObtenY())){
                return pixelArribaIzquierda;
            }

            return null;
        }


        private static bool YaAgregado(List<Pixel> pixeles, Pixel pixel)
        {
            if (pixeles.Count == 0)
            {
                return false;
            }
            foreach(Pixel p in pixeles)
            {
                if(pixel.ObtenX() == p.ObtenX() && pixel.ObtenY() == p.ObtenY())
                {
                    return true;
                }
            }
            return false;
        }

        private bool EsBorde(int x , int y){
            bool estaEnFigura = false;
            foreach(Pixel p in this.pixeles)
            {
                if(x == p.ObtenX() && y == p.ObtenY())
                {
                    estaEnFigura = true;
                    break;
                }
            }
            return estaEnFigura && VecinoEnFondo(x, y);
        }
    
        /// <summary>
        /// Checa que si alguno de sus Pixeles vecinos sea del color de fondo.
        /// </summary>
        /// <param name = "x"> Coordenada x del pixel. </param>
        /// <param name = "y"> Coordenada y del pixel. </param>
        /// <returns> True si alguno de sus pixeles vecinos es del color de fondo. </returns>
        public bool VecinoEnFondo(int x, int y)
        {
            return (EsFondo(x, y - 1) || EsFondo(x, y + 1) || EsFondo(x - 1, y) || EsFondo(x + 1, y));   
        }
        
        /// <summary>
        /// Determina si el pixel dado es del color de fondo.
        /// </summary>
        /// <param name = "x"> Coordenada x del pixel. </param>
        /// <param name = "y"> Coordenada y del pixel. </param>
        /// <returns> True si el pixel es del color de fondo. </returns>
        private bool EsFondo(int x, int y){
            try
            {
                return  imagen.GetPixel(x, y).Equals(imagen.GetPixel(0, 0));
            }
            catch (System.Exception)
            {
                
                return true;
            }
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
            if (numeroVertices ==2){
                return "Esto no deberia estar pasando";
            }
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
        private void AnexaPixel(List<Figura> figuras, Pixel pixel)
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
    }

    public class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Introduce la dirección de la imagen");
            string nombreImagen = "/home/arturo/Descargas/Ejemplos/example_2.bmp";
            Bitmap imagen = new Bitmap(nombreImagen);
            ProcesadorImagen procesador = new ProcesadorImagen(imagen);
            Console.WriteLine("La imagen tiene {0} pixeles de ancho y {1} pixeles de alto", imagen.Width, imagen.Height);
            List<Figura> figuras = procesador.RecorreImagen();
            Console.WriteLine("La imagen tiene {0} figuras", figuras.Count);
            foreach (Figura figura in figuras)
            {
                Console.WriteLine("Figura: " + figura.DeterminaFigura());
            }

        }
    }


} 
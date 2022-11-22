
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
        private string nombre;

        /// <summary>
        /// Constructor de la clase Figura.
        /// </summary>
        /// <param name = "pixeles"> Lista de pixeles que conforman la figura. </param>
        /// Suponemos que todos los pixeles de la lista son del mismo color.
        public Figura(List<Pixel> pixeles, Bitmap imagen){
            if(pixeles == null || pixeles.Count == 0 || imagen == null){
                throw new ArgumentException("La lista de pixeles no puede ser nula o vacía, la imagen debe estar en un Bitmap.");
            }
            this.pixeles = pixeles;
            this.imagen = imagen;
            this.color = pixeles[0].ObtenColor();
        }
        
       

        public string ObtenNombre(){
            this.nombre = DeterminaFigura();
            return nombre;
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
        /// Obtiene la representación hexadecimal del color de la figura.
        /// </summary>
        /// <returns> La representación hexadecimal del color de la figura. </returns>
        public string ObtenColorHex(){
            return color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
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
        /// Algoritmo que determina que figura es de acuerdo a su número de vértices.
        /// </summary>
        /// <returns>
        /// Una cadena con el nombre de la figura:
        /// Triangulo, Cuadrilatero, Circulo u otro.
        /// </returns>
        private string DeterminaFigura()
        {
            int numeroVertices = EncuentraVertices();
            if(numeroVertices == 3)
            {
                return "T";
            } else if(numeroVertices == 4)
            {
                return "C";
            } else if(numeroVertices == 0 || numeroVertices > 8)
            {
                return "0";
            } else
            {
                return "X";
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
           Pixel centro = EncuentraCentro();

           if(EsBorde(centro.ObtenX(), centro.ObtenY()))
           {
                //si el centro es borde, entonces es un triangulo
               return 3;
           }

           List <double> distancias = ObtenDistancias(centro);
           distancias = SuavizaDistancias(distancias);

           int maximos = 0;

           for(int i = 0; i< distancias.Count; i++){
                if(EsMaximo(distancias, i)){
                    maximos++;
                }
           }

           return maximos;

        }

        ///<summary>
        ///algoritmo que suaviza la lista de distancias
        // para que los bordes de la figura sean mejor definidos.
        ///</summary>
        ///<returns> La lista de distancias suavizadas.
        private List<double> SuavizaDistancias(List<double> lista){
            List<double> distanciasSuavizadas = new List<double>();
            
            for(int i = 2 ; i < lista.Count-3; i++){
                distanciasSuavizadas.Add(Promedia(i-2, i+2, lista));
            }
            return distanciasSuavizadas;

        }

        ///<summary>
        ///Algoritmo que promedia los valores en el rango
        // dado, suponemos que min y max son valores válidos en la lista.
        ///</summary>
        /// <param name = "lista"> Lista de pixeles que conforman la figura. </param>
        /// <param name = "min"> el primer elemento del rango a promediar </param>
        ///<returns> La lista de distancias suavizadas.
        private static double Promedia(int min, int max, List<double> lista){
            if(min>max || lista.Count < max){
                throw new ArgumentException("rango invalido");
            }
            double prom = 0;
            for(int i =  min; i <= max; i++){
                prom += lista[i];
            }
            prom = prom/(max-min);
            return prom;
        }


        
        /// <summary>
        /// Compara el elemento dado con los cuatro anteriores y los cuatro
        /// siguientes, si es mayor que todos, entonces es un máximo.
        /// </summary>
        /// <param name="lista"> La lista de elementos. </param>
        /// <param name="indice"> El índice del elemento a comparar. </param>
        private static bool EsMaximo(List<double> lista, int i){

            if(lista.Count < 9){
                return false;
            }
            
            // Si el elemento es el primero de la lista, lo comparamos con los 
            // cuatro finales y los cuatro iniciales.
            if(i == 0){
                for(int j = 1; j < 5; j++){
                    if(lista[i] < lista[j]){
                        return false;
                    }
                }
                for(int j = lista.Count - 4; j < lista.Count; j++){
                    if(lista[i] < lista[j]){
                        return false;
                    }
                }
                return true;
            }
            if(i == 1){
                for(int j = 0; j < 6; j++){
                    if(lista[i] < lista[j]){
                        return false;
                    }
                }
                for(int j = lista.Count - 3; j < lista.Count; j++){
                    if(lista[i] < lista[j]){
                        return false;
                    }
                }
                return true;
            }
            if(i == 2){
                for(int j = 0; j < 7; j++){
                    if(lista[i] < lista[j]){
                        return false;
                    }
                }
                for(int j = lista.Count - 2; j < lista.Count; j++){
                    if(lista[i] < lista[j]){
                        return false;
                    }
                }
                return true;
            }

            if(i == 3){
                for(int j = 0; j < 8; j++){
                    if(lista[i] < lista[j]){
                        return false;
                    }
                }
                if(lista[i] < lista[lista.Count - 1]){
                    return false;
                }
                
                return true;
            }

            // Si el elemento es el último de la lista, lo comparamos con los
            // cuatro iniciales y los cuatro finales.
            if(i == lista.Count - 1){
                for(int j = 0; j < 4; j++){
                    if(lista[i] < lista[j]){
                        return false;
                    }
                }
                for(int j = lista.Count - 5; j < lista.Count - 1; j++){
                    if(lista[i] < lista[j]){
                        return false;
                    }
                }
                return true;
            }

            if(i == lista.Count - 2){
                for(int j = 0; j < 3; j++){
                    if(lista[i] < lista[j]){
                        return false;
                    }
                }
                for(int j = lista.Count - 6; j < lista.Count; j++){
                    if(lista[i] < lista[j]){
                        return false;
                    }
                }
                return true;
            }

            if(i == lista.Count-3){
                for(int j = 0; j < 2; j++){
                    if(lista[i] < lista[j]){
                        return false;
                    }
                }
                for(int j = lista.Count - 7; j < lista.Count; j++){
                    if(lista[i] < lista[j]){
                        return false;
                    }
                }
                return true;
            }

            if( i == lista.Count - 4){
                if(lista[i] < lista[0]){
                    return false;
                }
                for(int j = lista.Count - 8; j < lista.Count; j++){
                    if(lista[i] < lista[j]){
                        return false;
                    }
                }
                return true;
            }

            //caso general:
            for(int j = i - 4; j <= i + 4; j++){
                if(j != i && lista[j] > lista[i]){
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Obtiene las distancias entre los puntos que están en el borde y el centro de la
        /// figura.
        /// </summary>
        ///<param name = "centro"> el centro de la figura </param>
        /// <returns> Lista de listas de distancias (para preservar un orden). </returns>
        private List<double> ObtenDistancias(Pixel centro)
        {
            List<double> distancias = new List<double>();
            List<Pixel> bordes = ObtenBordes();
            foreach (Pixel pixel in bordes)
            {
                distancias.Add(Math.Sqrt(Math.Pow(pixel.ObtenX() - centro.ObtenX(), 2)
                                + Math.Pow(pixel.ObtenY() - centro.ObtenY(), 2)));
            }
            return distancias;
        }

        /// <summary>
        /// Encuentra el centro de la figura.
        /// </summary>
        /// <returns> El Pixel del centro de la figura. </returns>
        private Pixel EncuentraCentro()
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
        private bool VecinoEnFondo(int x, int y)
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
                throw new ArgumentNullException("imagen invalida");
            }
            this.imagen = imagen;
        }

        /// <summary>
        /// Devuelve la lista de figuras encontradas en la imagen.
        /// </summary>
        /// <returns> Lista de figuras. </returns>
        public List<Figura> ObtenFiguras(){
            this.figuras = RecorreImagen();
            return figuras;
        }

        /// <summary>
        /// Procesa la imagen y obtiene las figuras presentes en la imagen.
        /// Se presupone que cada figura tiene un color diferente.
        /// </summary>
        /// <returns> Una lista con las figuras presentes en la imagen. </returns>
        private List<Figura> RecorreImagen(){
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
            try
            {
                string nombreImagen = Console.ReadLine();
                Bitmap imagen = new Bitmap(nombreImagen);
                ProcesadorImagen procesador = new ProcesadorImagen(imagen);
                List<Figura> figuras = procesador.ObtenFiguras();
                Console.WriteLine("La imagen tiene {0} figuras", figuras.Count);
                foreach (Figura figura in figuras)
                {
                    Console.WriteLine("{1} = {0}" , figura.ObtenNombre() , figura.ObtenColorHex());
                }
                
            }
            catch (System.Exception)
            {
                Console.WriteLine("Error al leer la imagen, por favor intenta de nuevo.");
                Main(args);
            }

        }
    }

} 
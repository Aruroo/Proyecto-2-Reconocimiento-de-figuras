
namespace ReconocimientoFiguras
{
    public class Figura{

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
        public Color getColor(){
            return pixeles[0].getColor();
        }

        ///<summary>
        ///agrega el pixel dado a esta figura
        ///</summary>
        public void agregaPixel(Pixel pixel){
            pixeles.Add(pixel);
        }
        ///<summary>
        ///encuentra el centro de la figura
        ///</summary>
        ///<returns> el Pixel del centro de la figura </returns>
        public Pixel encuentraCentro(){
            Pixel primerPixel = pixeles[0];
            Pixel ultimoPixel = pixeles[pixeles.Lengh-1];
            int medioX = (primerPixel.getX + ultimoPixel.getX)/2
            int medioY = (primerPixel.getY + ultimoPixel.getY)/2
            Pixel pixel = new Pixel (medioX, medioY , primerPixel.getColor())
            return pixeles[pixeles.IndexOf(pixel)];
        }

        

    }
}
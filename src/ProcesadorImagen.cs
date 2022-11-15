
using ReconocimientoFiguras.Pixel;

namespace ReconocimientoFiguras
{

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
        private List <Figura> RecorreImagen(){
            //el color del fondo es el primer pixel en la imagen
            Pixel fondo = new Pixel(0,0,imagen.GetPixel(0,0));
            List <Figura> figuras = new List<Figura>();

            //recorremos la imagen como un arreglo bidiemnsional
            for (int i = 0; i < imagen.Width; i++)
            {
                for (int j = 0; j < imagen.Height; j++)
                {
                    //si el pixel no es del color del fondo
                    if (!imagen.GetPixel(i,j).Equals(fondo.getColor()))
                    {
                        anexaPixel(figuras, imagen.GetPixel(i,j));
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
            boolean agregado = false;

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

} 
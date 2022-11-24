using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using ReconocimientoFiguras;
using Pixels;
using System;

namespace Procesador {
    /// <summary>
    /// Clase que representa el procesamiento de una imagen para detectar figuras.
    /// </summary>
    /// <remarks>
    /// Almacena las figuras resultantes del proceso en una lista de tipo Figura.
    /// </remarks>
    public class ProcesadorImagen {
        private Bitmap imagen;
        private List<Figura> figuras;

        /// <summary>
        /// Constructor de la clase ProcesadorImagen.
        /// </summary>
        /// <param name = "imagen"> Imagen a procesar. </param>
        public ProcesadorImagen(Bitmap imagen) {
            if (imagen == null) {
                throw new ArgumentNullException("imagen invalida");
            }
            this.imagen = imagen;
        }

        /// <summary>
        /// Obtiene la lista de figuras encontradas en la imagen.
        /// </summary>
        /// <returns> Lista de figuras. </returns>
        public List<Figura> ObtenFiguras() {
            this.figuras = RecorreImagen();
            return figuras;
        }

        /// <summary>
        /// Procesa la imagen y obtiene las figuras presentes en la imagen.
        /// Se presupone que cada figura tiene un color diferente.
        /// </summary>
        /// <returns> Una lista con las figuras presentes en la imagen. </returns>
        public List<Figura> RecorreImagen() {
            //El color del fondo es el primer pixel en la imagen.
            Pixel fondoPixel = new Pixel(0, 0, imagen.GetPixel(0, 0));
            List <Figura> figuras = new List<Figura>();
            for (int i = 0; i < imagen.Width; i++) {
                for (int j = 0; j < imagen.Height; j++) {
                    if (!imagen.GetPixel(i,j).Equals(fondoPixel.ObtenColor())) {
                        AnexaPixel(figuras, new Pixel(i,j,imagen.GetPixel(i,j)));
                    }
                }
            }
            return figuras;
        }
        
        /// <summary>
        /// Anexa el pixel a una figura en la lista dada.
        /// Si no existe una figura con su mismo color, la crea y la anexa a la lista. 
        /// </summary>
        /// <param name = "pixel"> Pixel a anexar. </param>
        /// <param name = "figuras"> Lista de figuras. </param>
        public void AnexaPixel(List<Figura> figuras, Pixel pixel) {
            bool agregado = false;
            if (figuras.Count != 0) {
                foreach (Figura figura in figuras) {
                    if(figura.ObtenColor().Equals(pixel.ObtenColor())) {
                        figura.AgregaPixel(pixel);
                        agregado = true;
                        return;
                    }
                }
            }
            
            if(!agregado) {
                List <Pixel> pixeles = new List<Pixel>();
                pixeles.Add(pixel);
                Figura figura = new Figura(pixeles, imagen);
                figuras.Add(figura);
            }
        }      
    }
} 
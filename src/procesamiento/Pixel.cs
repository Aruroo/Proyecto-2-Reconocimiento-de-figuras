using System;
using System.Drawing;


namespace Pixels {
    /// <summary>
    /// Clase que crea objetos de tipo Pixel.
    /// </summary>
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
        public Color ObtenColor() {
            return color;
        }

        /// <summary>
        /// Devuelve la coordenada x del pixel.
        /// </summary>
        public int ObtenX() {
            return x;
        }

        /// <summary>
        /// Devuelve la coordenada y del pixel.
        /// </summary>
        public int ObtenY() {
            return y;
        }
    }
}
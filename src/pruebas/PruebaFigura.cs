using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using Procesador;
using ReconocimientoFiguras;
using Pixels;
using Microsoft.VisualStudio.TestTools.UnitTesting;


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
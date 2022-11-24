using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Procesador;
using ReconocimientoFiguras;
using Pixels;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;

    ///[TestClass]
    public class PruebaProcesadorImagen {

        ///[TestMethod]
        public void PruebaRecorreImagen() {
            Bitmap imagen = new Bitmap(5, 5);
            ProcesadorImagen procesador = new ProcesadorImagen(imagen);
            List<Figura> figuras = procesador.RecorreImagen();
            Assert.AreEqual(1, figuras.Count);
        }

        ///[TestMethod]
        public void PruebaAnexaPixel() {
            Bitmap imagen = new Bitmap(5, 5);
            ProcesadorImagen procesador = new ProcesadorImagen(imagen);
            List<Figura> figuras = procesador.RecorreImagen();
            procesador.AnexaPixel(figuras, new Pixel(1, 2, Color.Red));
            Assert.AreEqual(2, figuras.Count);
        }
    }
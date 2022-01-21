using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaEntidades
{
    public class ImagenProducto
    {
        public int IdProducto { get; set; }
        public string Descripcion { get; set; }

        public PictureBox Imagen { get; set; }

        
    }
}

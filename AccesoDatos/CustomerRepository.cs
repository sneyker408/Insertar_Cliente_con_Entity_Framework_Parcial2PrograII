using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class CustomerRepository
    {
        public NorthwindEntities contexto = new NorthwindEntities();

        public List<Categories> CargarInformacion()
        {
            var categorias = from Category in contexto.Categories
                           select Category;

            return categorias.ToList();

        }

        public int InsertarCategoria(Categories categorias)
        {
            contexto.Categories.Add(categorias);
            return contexto.SaveChanges();
        }
    }

}

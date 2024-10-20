using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoAPIWeb.Models
{
    public class Products
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId ID_Productos { get; set; }

        public string NombreP { get; set; }

        public string Categoria { get; set; }

        public string Proveedor { get; set; }

        public float Precio { get; set; }

        public int Stock { get; set; }

        public string Descripcion { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCoreExemplo.Models
{
    public class Aluno
    {
        public MongoDB.Bson.ObjectId _id { get; set; }

        public string Nome { get; set; }

        public int Idade { get; set; }
    }
}

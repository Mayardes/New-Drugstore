using System;
using System.Collections.Generic;
using System.Text;

namespace Drugstore.Domain.Entities
{
    public class Shopkeeper
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sector { get; set; }
        public int Register { get; set; }
    }
}

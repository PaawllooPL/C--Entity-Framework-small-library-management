using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Model
{
    public class Library
    {
        public Library() 
        { 
        }
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        [Hide]
        public List<Client> Clients { get; set; } = new();
        public int ClientCount { get {  return Clients.Count; } }
    }
}

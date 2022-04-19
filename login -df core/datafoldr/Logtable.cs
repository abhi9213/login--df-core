using System;
using System.Collections.Generic;

#nullable disable

namespace login__df_core.datafoldr
{
    public partial class Logtable
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string Name { get; set; }
    }
}

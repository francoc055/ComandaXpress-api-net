﻿namespace comandaXpress_api_net.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public string Rol { get; set; }
        public DateTime FechaAlta { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BitacoraDireccionesIp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_bit_cat_equipo_telefono_modelo
    {
        public tb_bit_cat_equipo_telefono_modelo()
        {
            this.tb_bit_ip_historico = new HashSet<tb_bit_ip_historico>();
            this.tb_bit_ip_historico1 = new HashSet<tb_bit_ip_historico>();
            this.tb_bit_ip = new HashSet<tb_bit_ip>();
        }
    
        public int pk_cve_equipo_telefono_modelo { get; set; }
        public string des_equipo_telefono_modelo { get; set; }
    
        public virtual ICollection<tb_bit_ip_historico> tb_bit_ip_historico { get; set; }
        public virtual ICollection<tb_bit_ip_historico> tb_bit_ip_historico1 { get; set; }
        public virtual ICollection<tb_bit_ip> tb_bit_ip { get; set; }
    }
}

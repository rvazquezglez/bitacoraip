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
    
    public partial class tb_bit_cat_area
    {
        public tb_bit_cat_area()
        {
            this.tb_bit_usuario = new HashSet<tb_bit_usuario>();
            this.tb_bit_usuario_historico = new HashSet<tb_bit_usuario_historico>();
        }
    
        public int pk_cve_area { get; set; }
        public string des_area { get; set; }
    
        public virtual ICollection<tb_bit_usuario> tb_bit_usuario { get; set; }
        public virtual ICollection<tb_bit_usuario_historico> tb_bit_usuario_historico { get; set; }
    }
}

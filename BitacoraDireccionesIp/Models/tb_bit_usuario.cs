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
    
    public partial class tb_bit_usuario
    {
        public tb_bit_usuario()
        {
            this.tb_bit_ip = new HashSet<tb_bit_ip>();
            this.tb_bit_ip_historico = new HashSet<tb_bit_ip_historico>();
        }
    
        public int pk_cve_usuario { get; set; }
        public string nom_user_name { get; set; }
        public string nom_usuario { get; set; }
        public string ape_paterno { get; set; }
        public string ape_materno { get; set; }
        public int fk_cve_dga { get; set; }
        public int fk_cve_area { get; set; }
        public int fk_cve_empresa { get; set; }
        public int fk_cve_piso { get; set; }
        public int fk_cve_regional { get; set; }
        public string des_observacion { get; set; }
        public System.DateTime fec_alta { get; set; }
        public Nullable<System.DateTime> fec_baja { get; set; }
        public Nullable<System.DateTime> fec_actualiza { get; set; }
    
        public virtual tb_bit_cat_area tb_bit_cat_area { get; set; }
        public virtual tb_bit_cat_dga tb_bit_cat_dga { get; set; }
        public virtual tb_bit_cat_empresa tb_bit_cat_empresa { get; set; }
        public virtual tb_bit_cat_piso tb_bit_cat_piso { get; set; }
        public virtual tb_bit_cat_regional tb_bit_cat_regional { get; set; }
        public virtual ICollection<tb_bit_ip> tb_bit_ip { get; set; }
        public virtual ICollection<tb_bit_ip_historico> tb_bit_ip_historico { get; set; }
    }
}

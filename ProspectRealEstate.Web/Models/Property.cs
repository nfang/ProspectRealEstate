//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProspectRealEstate.Web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Property
    {
        public Property()
        {
            this.Attachments = new HashSet<Attachment>();
        }
    
        public int ID { get; set; }
        public string property_code { get; set; }
        public byte property_type { get; set; }
        public byte num_of_bedroom { get; set; }
        public byte num_of_bathroom { get; set; }
        public byte num_of_carspace { get; set; }
        public Nullable<int> land_area { get; set; }
        public string description { get; set; }
        public bool featured { get; set; }
        public string street { get; set; }
        public short suburb_id { get; set; }
        public byte state_id { get; set; }
        public short country_id { get; set; }
        public string postcode { get; set; }
        public Nullable<int> min_price { get; set; }
        public Nullable<int> max_price { get; set; }
        public System.Data.Spatial.DbGeography latlng { get; set; }
        public Nullable<byte> agent_id { get; set; }
        public System.DateTime added_on { get; set; }
        public byte added_by { get; set; }
        public Nullable<System.DateTime> modified_on { get; set; }
        public Nullable<byte> modified_by { get; set; }
        public string status { get; set; }
        public string rent_or_sale { get; set; }
        public byte lang { get; set; }
    
        public virtual Agent Agent { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual Country Country { get; set; }
        public virtual PropertyType PropertyType { get; set; }
        public virtual State State { get; set; }
        public virtual Suburb Suburb { get; set; }
        public virtual User AddedBy { get; set; }
        public virtual User ModifiedBy { get; set; }
        public virtual Language Language { get; set; }
    }
}

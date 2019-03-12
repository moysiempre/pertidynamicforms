namespace FormsAdminGP.Domain
{
    public class FormDetail : Entity<string>
    {
        public string FormHdId { get; set; }
        public short FieldTypeId { get; set; }
        public string FieldLabel { get; set; }
        public short Order { get; set; }
        public bool IsRequired { get; set; }        
    }
}

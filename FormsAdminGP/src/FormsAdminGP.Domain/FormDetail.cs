namespace FormsAdminGP.Domain
{
    public class FormDetail : Entity<string>
    {
        public string FormHdId { get; set; }
        public string FieldTypeId { get; set; }
        public string FieldLabel { get; set; }
        public byte Order { get; set; }
        public bool IsRequired { get; set; }        
    }
}

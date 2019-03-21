using System.ComponentModel;

namespace FormsAdminGP.Common.Enums
{
    public enum WithEMail : byte
    {
        To = 1,
        CC,
        Bcc
    }

    public enum FieldType : byte
    {
        [Description("Texto alfanumérico")]
        text = 1,      
        [Description("Dropdown List")]
        select,
        [Description("Radio")]
        radio,
        [Description("Checkbox")]
        checkbox,
        [Description("Botón Enviar")]
        submit
    }

    public enum Operation : byte
    {
        Create = 1,
        Read = 0,
        Update = 2,
        Delete = 3
    }
}

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
        textInput = 1,
        [Description("Numérico")]
        numberInput,
        [Description("Radio")]
        radio,
        [Description("Checkbox")]
        checkbox,
        [Description("Botón Enviar")]
        submitInput
    }

    public enum Operation : byte
    {
        Create = 1,
        Read = 0,
        Update = 2,
        Delete = 3
    }
}

using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace RH.API.Enum
{
    public enum Permissoes
    {
        [XmlEnumAttribute("F")]
        [Display(Name="Funcionário")]
        Funcionario,

        [XmlEnumAttribute("G")]
        [Display(Name = "Gerente")]
        Gerente,

        [XmlEnumAttribute("A")]
        [Display(Name = "Administrador")]
        Administrador
    }
}

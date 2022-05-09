using System.ComponentModel;

namespace ITSolution.Framework.Common.BaseClasses.AbstractEntities;

public enum TypeCliente
{
    [Description("Física")]
    Fisica = 0,
    [Description("Jurídica")]
    Juridica = 1
}
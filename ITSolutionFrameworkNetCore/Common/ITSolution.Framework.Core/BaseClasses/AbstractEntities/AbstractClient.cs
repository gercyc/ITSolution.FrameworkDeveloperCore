using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ITSolution.Framework.Core.Common.BaseClasses.AbstractEntities;

[Serializable]
public abstract class AbstractClient
{
    [Required]
    [StringLength(200)]
    [Display(Name = "Razão Social")]
    public string RazaoSocial { get; set; }

    [StringLength(200)]
    [Display(Name = "Nome Fantansia")]
    public string NomeFantasia { get; set; }
        
    [Display(Name = "CPF/CNPJ")]
    [StringLength(18, MinimumLength =0, ErrorMessage = "CNPJ deve conter entre 11 e 14 digítos")]
    public string CpfCnpj { get; set; }

    [StringLength(100)]
    [Display(Name = "E-mail da empresa")]
    public string Email { get; set; }

    [StringLength(20)]
    [Display(Name = "Telefone Principal")]
    public string Telefone { get; set; }

    [StringLength(45)]
    public string DataSituacao { get; set; }

    [StringLength(45)]
    public string SituacaoJuridica { get; set; }

    [StringLength(45)]
    public string Abertura { get; set; }

    [StringLength(100)]
    public string NaturezaJuridica { get; set; }

    [JsonProperty("ultima_atualizacao")]
    public Nullable<DateTime> UltimaAtualizacao { get; set; }

    [StringLength(45)]        
    public string StatusCliente { get; set; }

    [StringLength(45)]
    public string Efr { get; set; }

    [StringLength(45)]
    public string MotivoSituacao { get; set; }

    [StringLength(45)]
    public string SituacaoEspecial { get; set; }

    [StringLength(45)]
    public string DataSituacaoEspecial { get; set; }

    public Nullable<decimal> CapitalSocial { get; set; }

    [Display(Name = "Nome Endereço")]
    [StringLength(200)]
    public string NomeEndereco { get; set; }

    [JsonProperty("numero")]
        
    //Marcação caso o endereço não tenha número = S/N
    [Display(Name = "Número do endereço")]
    [StringLength(200)]
    public string NumeroEndereco { get; set; }

    [Display(Name = "Bairro")]
    [StringLength(200)]
    public string Bairro { get; set; }

    [StringLength(100)]
    public string Complemento { get; set; }

    [Display(Name = "CEP")]
    [StringLength(9, ErrorMessage = "Cep deve conter 8 ou 9 digítos\nEx: 00000-000\n00000000")]
    public string Cep { get; set; }

    [Display(Name = "Estado")]
    [StringLength(100)]
    public string Uf { get; set; }

    [StringLength(100)]
    public string Cidade { get; set; }

    [Display(Name = "Tipo de endereço")]
    [StringLength(50)]
    public string TipoEndereco { get; set; }

    [Display(Name = "Tipo de Cliente")]
    public TypeCliente TipoCliente { get; set; }

    [Display(Name = "Data de Nascimento")]
    [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
    public DateTime? DataNascimento { get; set; }

    [Display(Name = "Documento Identidade (RG)")]
    [StringLength(20)]
    public string RG { get; set; }

    [StringLength(25)]
    [Display(Name = "Celular ")]
    public string Celular { get; set; }

    [StringLength(25)]
    [Display(Name = "Telefone Comercial")]
    public string TelefoneComercial { get; set; }

    [Display(Name = "Data Registro")]
    public Nullable<DateTime> DtRegtroJuntaCom { get; set; }

    [Display(Name = "Data Cadastro")]
    public Nullable<DateTime> DtCadastro { get; set; }

    [StringLength(25)]
    [Display(Name = "Inscrição Estadual")]
    public string InscricaoEstadual { get; set; }

    [StringLength(25)]
    [Display(Name = "Inscrição Municipal")]
    public string InscricaoMunicipal { get; set; }

    [StringLength(50)]
    public string Pais { get; set; }

    protected AbstractClient()
    {
        this.TipoCliente = TypeCliente.Fisica;
    }

    protected AbstractClient(string nome)
    {
        this.RazaoSocial = nome;
    }

    protected AbstractClient(string nome, string rg, string cpfCnpj, DateTime? dtDataNasc, TypeCliente tipoCliente,
        string telefone, string celular, string telComercial)
        : this()
    {
        this.RazaoSocial = nome;
        this.RG = rg;
        this.CpfCnpj = cpfCnpj;
        this.DataNascimento = dtDataNasc;
        this.TipoCliente = tipoCliente;
        this.Telefone = telefone;
        this.Celular = celular;
        this.TelefoneComercial = telComercial;
    }

   
    /// <summary>
    /// Atualiza os dados Cliente com o Cliente informado
    /// </summary>
    /// <param name="cliente"></param>
    public virtual void Update(AbstractClient cliente)
    {
        this.RazaoSocial = cliente.RazaoSocial;
        this.RG = cliente.RG;
        this.CpfCnpj = cliente.CpfCnpj;
        this.DataNascimento = cliente.DataNascimento;
        this.Email = cliente.Email;
        this.Telefone = cliente.Telefone;
        this.Celular = cliente.Celular;
        this.TelefoneComercial = cliente.TelefoneComercial;
        this.TipoCliente = cliente.TipoCliente;
    }

    public override string ToString()
    {
        return RazaoSocial;
    }

}
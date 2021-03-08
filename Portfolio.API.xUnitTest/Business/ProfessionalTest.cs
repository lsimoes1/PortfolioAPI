using Bogus;
using Moq;
using Portfolio.API.Business;
using Portfolio.API.DAO.Interface;
using Portfolio.API.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Portfolio.API.xUnitTest.Business
{
    public class ProfessionalTest
    {
        //private BProfessional academy;
        //private Mock<IProfessionalDAO> bussines;

        //private List<MProfessional> CreateFakeObj()
        //{
        //    List<MProfessional> professionalObj = new Faker<MProfessional>()
        //        .RuleFor(p => p.Company, f => f.Company.CompanyName())
        //    .RuleFor(p => p.Image, f => f.Internet.UrlWithPath())
        //    .RuleFor(p => p.Link, f => f.Internet.UrlWithPath())
        //    .RuleFor(p => p.Offices, f => f.Make(3, () => f.Commerce.Department, f.Date.Recent, f.Date.Past, f.Commerce.Department))
        //    .RuleFor(p => p.Imagem, f => f.Internet.Url())
        //    .RuleFor(p => p.Linguagem, f => f.Random.Word())
        //    .RuleFor(p => p.Tipo, f => f.Random.Int())
        //    .Generate(new Random().Next(1, 10), null);

        //    return professionalObj;
        //}

        //[Fact]
        //public void Teste_Retorno_Aademy_Projects_Not_Null()
        //{
        //    bussines = new Mock<IAcademyDAO>();

        //    var objFake = this.CreateFakeObj();
        //    bussines.Setup(x => x.FindAllAcademyProjects()).Returns(objFake);
        //    academy = new BAcademy(bussines.Object);
        //    string response = academy.getAcademy();

        //    Assert.Equal(JsonConvert.SerializeObject(objFake), response);
        //}

        //[Fact]
        //public void Teste_Retorno_Aademy_Projects_NotFound()
        //{
        //    bussines = new Mock<IAcademyDAO>();

        //    List<MAcademy> listAcademy = new List<MAcademy>();

        //    bussines.Setup(x => x.FindAllAcademyProjects()).Returns(listAcademy);
        //    academy = new BAcademy(bussines.Object);
        //    string response = academy.getAcademy();

        //    Assert.Null(response);
        //}
    }
}

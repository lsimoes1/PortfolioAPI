using Bogus;
using Moq;
using Newtonsoft.Json;
using Portfolio.API.Business;
using Portfolio.API.Business.Interface;
using Portfolio.API.DAO.Interface;
using Portfolio.API.Model;
using Portfolio.API.Model.Response;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace Portfolio.API.xUnitTest.Business
{
    public class AcademyTest
    {
        private BAcademy academy;
        private Mock<IAcademyDAO> bussines;

        private List<MAcademy> CreateFakeObj()
        {
            List<MAcademy> academyobj = new Faker<MAcademy>()
                .RuleFor(p => p.Descricao, p => p.Random.Word())
            .RuleFor(p => p.Nome, p => p.Name.FirstName())
            .RuleFor(p => p.Concluido, p => p.Random.Bool())
            .RuleFor(p => p.DataConclusao, p => p.Date.Past())
            .RuleFor(p => p.Imagem, p => p.Internet.Url())
            .RuleFor(p => p.Linguagem, p => p.Random.Word())
            .RuleFor(p => p.Tipo, p => p.Random.Int())
            .Generate(new Random().Next(1, 10), null);

            return academyobj;
        }

        [Fact]
        public void Teste_Retorno_Aademy_Projects_Not_Null()
        {
            bussines = new Mock<IAcademyDAO>();

            var objFake = this.CreateFakeObj();
            bussines.Setup(x => x.FindAllAcademyProjects()).Returns(objFake);
            academy = new BAcademy(bussines.Object);
            string response = academy.getAcademy();

            Assert.Equal(JsonConvert.SerializeObject(objFake), response);
        }

        [Fact]
        public void Teste_Retorno_Aademy_Projects_NotFound()
        {
            bussines = new Mock<IAcademyDAO>();

            List<MAcademy> listAcademy = new List<MAcademy>();

            bussines.Setup(x => x.FindAllAcademyProjects()).Returns(listAcademy);
            academy = new BAcademy(bussines.Object);
            string response = academy.getAcademy();

            Assert.Null(response);
        }
    }
}

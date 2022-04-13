using Microsoft.AspNetCore.Mvc;
using PocoSwaggerApp.ViewModels;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text.Json;

namespace PocoSwaggerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountThreasholdController : ControllerBase
    {
        static HttpClient client = new HttpClient();
        static DiscountThreshold retdiscountThreshold = new DiscountThreshold();
        ///// <summary>
        ///// Obter todos os usuários.
        ///// </summary>
        ///// <response code="200">A lista de usuários foi obtida com sucesso.</response>
        ///// <response code="500">Ocorreu um erro ao obter a lista de usuários.</response>
        //[HttpGet]
        //[ProducesResponseType(typeof(List<UsuarioGetVm>), 200)]
        //[ProducesResponseType(500)]
        //public IActionResult Get()
        //{
        //    var usuarios = ObterUsuarios();
        //    return Ok(usuarios);
        //}

        ///// <summary>
        ///// Obter um usuário específico por ID.
        ///// </summary>
        ///// <param name="id">ID do usuário.</param>
        ///// <response code="200">O usuário foi obtido com sucesso.</response>
        ///// <response code="404">Não foi encontrado usuário com ID especificado.</response>
        ///// <response code="500">Ocorreu um erro ao obter o usuário.</response>
        //[HttpGet("{id}")]
        //public IActionResult Get(Guid id)
        //{
        //    var usuarios = ObterUsuarios();
        //    var usuario = usuarios.FirstOrDefault(x => x.Id.Equals(id));

        //    if (usuario == null)
        //        return NotFound(new { message = "Usuário não encontrado." });

        //    return Ok(usuario);
        //}

        /// <summary>
        /// Save DiscountThreashold.
        /// </summary>
        /// <param name="DiscountThreashold">Model of DiscountThreashold.</param>
        /// <response code="200">DiscountThreashold sucess.</response>
        /// <response code="400">The model of DiscountThreashold send is wrong.</response>
        /// <response code="500">Show an error to save DiscountThreashold.</response>
        [HttpPost]
        public IActionResult Post(DiscountThreshold discountThreshold)
        {
            //HttpClient client = new HttpClient();
            
            RunAsyncDiscountThreshold().GetAwaiter().GetResult();
            
            static async Task<DiscountThreshold> GetDiscountThresholdAsync(DiscountThreshold discountThreshold)
            {
                
                HttpResponseMessage response = await client.PostAsJsonAsync(
                    "api/DiffDiscount/GetDiscounts", discountThreshold);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    discountThreshold = await response.Content.ReadFromJsonAsync<DiscountThreshold>();
                }

                return discountThreshold;
            }

            static async Task RunAsyncDiscountThreshold()
            {
                client.BaseAddress = new Uri("https://pricingmanagerapi-g1.r2.pcf.dell.com/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    DiscountThreshold discountThreshold = new DiscountThreshold();
                    //var listDiscountThreshold = await GetDiscountThresholdAsync(discountThreshold);
                    retdiscountThreshold = await GetDiscountThresholdAsync(discountThreshold);
                }
                catch (Exception e)
                {
                    string error = e.Message;
                }
            }
            return Ok(retdiscountThreshold);
        }

        ///// <summary>
        ///// Alterar usuário.
        ///// </summary> 
        ///// <param name="id">ID do usuário.</param>
        ///// <param name="usuario">Modelo do usuário.</param>
        ///// <response code="200">O usuário foi alterado com sucesso.</response>
        ///// <response code="400">O modelo do usuário enviado é inválido.</response>
        ///// <response code="404">Não foi encontrado usuário com ID especificado.</response>
        ///// <response code="500">Ocorreu um erro ao alterar o usuário.</response>
        //[HttpPut("{id}")]
        //public IActionResult Put([FromRoute] Guid id, [FromBody] UsuarioPutVm usuario)
        //{
        //    try
        //    {
        //        var usuarios = ObterUsuarios();
        //        var usuarioFakeDb = usuarios.FirstOrDefault(x => x.Id.Equals(id));

        //        if (usuarioFakeDb == null)
        //            return NotFound(new { message = "Usuário não encontrado." });

        //        //Atualiza o usuário na base de dados.

        //        return NoContent();
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode((int)HttpStatusCode.InternalServerError);
        //    }
        //}

        ///// <summary>
        ///// Deletar usuário.
        ///// </summary>
        ///// <param name="id">ID do usuário.</param>
        ///// <response code="200">O usuário foi deletado com sucesso.</response>
        ///// <response code="404">Não foi encontrado usuário com ID especificado.</response>
        ///// <response code="500">Ocorreu um erro ao deletar o usuário.</response>
        //[HttpDelete("{id}")]
        //public IActionResult Delete(Guid id)
        //{
        //    try
        //    {
        //        var usuarios = ObterUsuarios();
        //        var ususario = usuarios.FirstOrDefault(x => x.Id.Equals(id));

        //        if (ususario == null)
        //            return NotFound(new { message = "Usuário não encontrado." });

        //        //Exclui o usuário na base de dados.

        //        return NoContent();
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode((int)HttpStatusCode.InternalServerError);
        //    }
        //}

        //private IEnumerable<UsuarioGetVm> ObterUsuarios()
        //{
        //    //throw new Exception("Não foi possível buscar os usuários na base.");

        //    return new List<UsuarioGetVm>
        //    {
        //        new UsuarioGetVm { Id = Guid.Parse("CAEF4812-598E-4473-8EFC-B338AF69A18F"), Name = "João", Email = "joao@outlook.com" },
        //        new UsuarioGetVm { Id = Guid.Parse("B88DCCAF-A3A2-436F-8B09-27F0E1F73321"), Name = "José", Email = "jose@outlook.com" },
        //        new UsuarioGetVm { Id = Guid.Parse("1E018A4E-FC1B-4E2D-91CA-8076D60CF63F"), Name = "Maria", Email = "maria@outlook.com" }
        //    };
        //}
    }
}

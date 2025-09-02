using Diet.Pro.AI.Infra.Shared.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace Diet.Pro.AI.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhysicalAssessmentController : Controller
    {
        [HttpPost]
        public IActionResult CalcularTmb([FromBody] TmbImputModel req)
        {
            // Validação simples
            if (req.Peso <= 0 || req.Altura <= 0 || req.Idade <= 0)
                return BadRequest("Peso, altura e idade devem ser maiores que zero.");

            // Cálculo da TMB (Mifflin-St Jeor)
            double tmb;
            if (req.Sexo.ToLower() == "masculino")
                tmb = 10 * req.Peso + 6.25 * req.Altura - 5 * req.Idade + 5;
            else
                tmb = 10 * req.Peso + 6.25 * req.Altura - 5 * req.Idade - 161;

            // Fator de atividade física
            var fatores = new Dictionary<string, double>
        {
            { "sedentario", 1.2 },
            { "leve", 1.375 },
            { "moderado", 1.55 },
            { "intenso", 1.725 },
            { "muito_intenso", 1.9 }
        };

            double fator = fatores.GetValueOrDefault(req.NivelAtividade.ToLower(), 1.55);
            double tmbAjustada = tmb * fator;

            return Ok(new
            {
                Tmb = Math.Round(tmb, 2),
                TmbAjustada = Math.Round(tmbAjustada, 2),
                Unidade = "kcal/dia",
                req.NivelAtividade
            });
        }
    }
}

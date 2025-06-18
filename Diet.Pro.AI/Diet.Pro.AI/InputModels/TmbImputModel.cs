namespace Diet.Pro.AI.InputModels
{
    public class TmbImputModel
    {
        public string Sexo { get; set; } = "masculino"; // "masculino" ou "feminino"
        public int Idade { get; set; }
        public float Peso { get; set; }     // em kg
        public float Altura { get; set; }   // em cm
        public string NivelAtividade { get; set; } = "moderado"; // sedentario, leve, moderado, intenso, muito_intenso
    }
}

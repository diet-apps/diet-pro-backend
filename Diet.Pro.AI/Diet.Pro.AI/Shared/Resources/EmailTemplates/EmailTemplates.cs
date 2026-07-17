namespace Diet.Pro.AI.Shared.Resources.EmailTemplates
{
    public static class EmailTemplates
    {
        public const string WelcomeSubject = "?? Bem-vindo ao SmartNutri.Ai";

        public const string WelcomeBody = """
            <h1>Bem-vindo ao SmartNutri.Ai.</h1>

            <p>
                Sua jornada para uma vida mais saudável começa agora.
            </p>

            <p>
                Acompanhe sua alimentação, monitore indicadores importantes,
                descubra suas necessidades nutricionais e tome decisões mais inteligentes
                para o seu bem-estar.
            </p>

            <p>
                Estamos felizes por fazer parte dessa jornada.
            </p>

            <p>
                <strong>Equipe SmartNutri.Ai ??</strong>
            </p>
            """;

        public const string BirthdaySubjectTemplate = "?? Feliz aniversário, {0}!";

        public const string BirthdayBodyTemplate = """
            <h1>Parabéns, {0}! ??</h1>

            <p>
                Hoje é um dia especial, e nós da SmartNutri.Ai queremos celebrar com você.
            </p>

            <p>
                Que seu novo ciclo venha com muita saúde, energia e conquistas.
            </p>

            <p>
                Conte com a gente para seguir firme na sua jornada de bem-estar.
            </p>

            <p>
                <strong>Com carinho, Equipe SmartNutri.Ai ??</strong>
            </p>
            """;

        public static string GetBirthdaySubject(string name)
            => string.Format(BirthdaySubjectTemplate, name);

        public static string GetBirthdayBody(string name)
            => string.Format(BirthdayBodyTemplate, name);
    }
}
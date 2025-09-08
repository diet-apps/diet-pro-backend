using Diet.Pro.AI.Aplication.Interfaces;
using Diet.Pro.AI.Aplication.Services;
using Diet.Pro.AI.Infra.Data.Repositories.Interfaces;
using Diet.Pro.AI.Infra.Data.Repositories;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;

namespace Diet.Pro.AI.Infra.IoC
{
    public static class ConfigureServicesExtensions
    {
        private const string FirebaseProjectId = "FirebaseSettings:ProjectId";
        private const string FirebaseCredentials = "FirebaseSettings:CredentialsJson";

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddScoped<IFirebaseService, FirebaseService>();
            services.AddScoped<IUserFirebaseService, UserFirebaseService>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IFireStoreRepository, FirestoreRepository>();
            return services;
        }

        public static IServiceCollection AddFirestoreDb(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. Pega o ProjectId do appsettings.json
            string projectId = configuration[FirebaseProjectId];
            // 2. Pega a string JSON das credenciais da configuração (User Secrets ou Variáveis de Ambiente)
            string credentialsJson = configuration[FirebaseCredentials];
            if (string.IsNullOrEmpty(credentialsJson))
            {
                throw new InvalidOperationException(
                    "A credencial JSON do Firebase não foi encontrada. " +
                    "Verifique se 'Firebase:CredentialsJson' está configurado nos User Secrets ou variáveis de ambiente.");
            }
            // 3. Cria a credencial a partir da string JSON
            var credential = GoogleCredential.FromJson(credentialsJson);
            // 4. Cria e retorna a instância do FirestoreDb com a credencial explícita
            var firestoreDb = new FirestoreDbBuilder
            {
                ProjectId = projectId,
                Credential = credential
            }.Build();
            services.AddSingleton(firestoreDb);
            return services;
        }
        public static IServiceCollection AddFirebaseAdmin(this IServiceCollection services, IConfiguration configuration)
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromJson(configuration[FirebaseCredentials]),
            });
            return services;
        }
    }
}

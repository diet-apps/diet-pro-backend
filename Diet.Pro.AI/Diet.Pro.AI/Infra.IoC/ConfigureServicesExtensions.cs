﻿using Diet.Pro.AI.Aplication.Interfaces;
using Diet.Pro.AI.Aplication.Services;
using Diet.Pro.AI.Infra.Data.Repositories.Interfaces;
using Diet.Pro.AI.Infra.Data.Repositories;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Newtonsoft.Json;

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
            var projectId = configuration[FirebaseProjectId];
            var credentialsRaw = configuration[FirebaseCredentials];

            if (string.IsNullOrWhiteSpace(projectId))
                throw new InvalidOperationException($"O ProjectId do Firebase está ausente. Verifique a configuração '{FirebaseProjectId}'.");

            if (string.IsNullOrWhiteSpace(credentialsRaw))
                throw new InvalidOperationException($"A credencial JSON do Firebase está ausente. Verifique a configuração '{FirebaseCredentials}'.");

            try
            {
                string credentialsJson = GetCredentialsJson(credentialsRaw);

                //// Heurística simples: se começa com "{" => provavelmente é JSON puro
                //if (credentialsRaw.TrimStart().StartsWith("{"))
                //{
                //    credentialsJson = credentialsRaw;
                //}
                //else
                //{
                //    // Se for uma string escapada, desserializa
                //    credentialsJson = JsonConvert.DeserializeObject<string>(credentialsRaw);
                //}

                // Corrige quebras de linha no private_key
                credentialsJson = credentialsJson.Replace("\\n", "\n");

                var credential = GoogleCredential.FromJson(credentialsJson);

                var firestoreDb = new FirestoreDbBuilder
                {
                    ProjectId = projectId,
                    Credential = credential
                }.Build();

                services.AddSingleton(firestoreDb);

                return services;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Erro ao carregar as credenciais do Firebase. Verifique se a variável 'Firebase__CredentialsJson' está corretamente formatada.",
                    ex);
            }
        }



        public static IServiceCollection AddFirebaseAdmin(this IServiceCollection services, IConfiguration configuration)
        {
            string credentialsJson = GetCredentialsJson(configuration[FirebaseCredentials]);
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromJson(credentialsJson),
            });
            return services;
        }

        private static string GetCredentialsJson(string? credentialsRaw)
        {
            // Heurística simples: se começa com "{" => provavelmente é JSON puro
            if (credentialsRaw!.TrimStart().StartsWith("{"))
            {
                return credentialsRaw;
            }

            // Se for uma string escapada, desserializa
            return JsonConvert.DeserializeObject<string>(credentialsRaw)!;
        }
    }
}

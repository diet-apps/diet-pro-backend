using Diet.Pro.AI.Infra.Data.Repositories.Collections;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Diet.Pro.AI.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestFirebaseController : ControllerBase
    {
        private readonly FirestoreDb _firestoreDb;

        public TestFirebaseController(FirestoreDb firestoreDb)
        {
            _firestoreDb = firestoreDb;
        }

        [HttpGet("list-users")]
        public async Task<IActionResult> ListUsers()
        {
            try
            {
                var usersCollection = _firestoreDb.Collection(FirebaseCollections.Users);

                var snapshot = await usersCollection.Limit(5).GetSnapshotAsync();

                var userIds = snapshot.Documents.Select(d => d.Id);

                return Ok(new
                {
                    success = true,
                    message = "Conectado ao Firestore e dados obtidos com sucesso!",
                    userIds = userIds
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Erro ao conectar ao Firestore",
                    error = ex.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }
    }
}

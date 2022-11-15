using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XF_CrudFirebase.Models;

namespace XF_CrudFirebase.Services
{
    class FirebaseService
    {
        FirebaseClient firebase =
           new FirebaseClient("https://xamarinfirebase-52b1f-default-rtdb.firebaseio.com/");

        public async Task AddContato(int contatoId, string nome, string email) => await firebase
               .Child("Contatos")
                  .PostAsync(new Contato() { ContatoId = contatoId, Nome = nome, Email = email });

        public async Task<List<Contato>> GetContatos()
        {
            return (await firebase
              .Child("Contatos")
              .OnceAsync<Contato>()).Select(item => new Contato
              {
                  Nome = item.Object.Nome,
                  Email = item.Object.Email,
                  ContatoId = item.Object.ContatoId
              }).ToList();
        }
        public async Task<Contato> GetContato(int ContatoId)
        {
            var contatos = await GetContatos();
            await firebase
              .Child("Contatos")
                .OnceAsync<Contato>();
            return contatos.Where(a => a.ContatoId == ContatoId)
                .FirstOrDefault();
        }
        public async Task UpdateContato(int ContatoId, string nome, string email)
        {
            var toUpdateContato = (await firebase
              .Child("Contatos")
                .OnceAsync<Contato>())
                   .Where(a => a.Object.ContatoId == ContatoId).FirstOrDefault();
            await firebase
              .Child("Contatos")
                .Child(toUpdateContato.Key)
                  .PutAsync(new Contato()
                  { ContatoId = ContatoId, Nome = nome, Email = email });
        }
        public async Task DeleteContato(int ContatoId)
        {
            var toDeleteContato = (await firebase
              .Child("Contatos")
              .OnceAsync<Contato>())
                .Where(a => a.Object.ContatoId == ContatoId).FirstOrDefault();
            await firebase.Child("Contatos")
                    .Child(toDeleteContato.Key)
                        .DeleteAsync();
        }
    }
}

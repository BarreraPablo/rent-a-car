using RentACar.Core.Entities;
using RentACar.Core.Exceptions;
using RentACar.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentACar.Core.Services
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork unitOfWork;

        public ClientService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Client> GetClients() => unitOfWork.ClientRepository.GetAll();

        public async Task Create(Client client)
        {
            if (client == null)
            {
                throw new ArgumentNullException();
            }

            int clientAge = client.CalculateAge();

            if (clientAge < 16)
            {
                throw new BussinessException("The client must be over 15 years old");
            }

            var existentClient = await unitOfWork.ClientRepository.GetByDocumentNumber(client.DocumentNumber);

            if (existentClient != null)
            {
                throw new BussinessException("The client has already been registered");
            }

            await unitOfWork.ClientRepository.Add(client);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task Update(Client client)
        {
            if (client == null)
            {
                throw new ArgumentNullException();
            }

            int clientAge = client.CalculateAge();

            if (clientAge < 16)
            {
                throw new BussinessException("The client must be over 15 years old");
            }

            var existentClient = await unitOfWork.ClientRepository.GetByDocumentNumber(client.DocumentNumber);

            if (existentClient != null && existentClient.Id != client.Id)
            {
                throw new BussinessException("The client has already been registered");
            }

            await unitOfWork.ClientRepository.GetAndUpdate(client);
            await unitOfWork.SaveChangesAsync();
        }
    }
}

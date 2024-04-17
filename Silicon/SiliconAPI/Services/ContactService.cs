using SiliconAPI.Entitites;
using SiliconAPI.Models;
using SiliconAPI.Repositories;

namespace SiliconAPI.Services
{
    public class ContactService
    {
        private readonly ContactRepository _repo;

        public ContactService(ContactRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> CreateContactRequestAsync(ContactModel model)
        {
            try
            {

                var entity = new ContactEntity()
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    Service = model.Service,
                    Message = model.Message
                };

                var result = await _repo.CreateAsync(entity);

                if (result)
                {
                    return true;
                }

            }

            catch { }

            return false;
        }
    }
}

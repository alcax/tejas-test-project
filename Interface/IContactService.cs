using SampleProject.Models;
namespace SampleProject.Interface
{
    public interface IContactService
    {
        /// <summary>
        /// Add Contact details 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ActionOutput</returns>
        Task<ActionOutput> AddContact(AddContactModel model);

        /// <summary>
        /// Get Contact list with pagination and filters
        /// </summary>
        /// <param name="model">PagingModel</param>
        /// <returns>PagingResult<ContactListingModel></returns>
        Task<PagingResult<ContactListingModel>> GetContactPagedlist(PagingModel model);

        /// <summary>
        ///Get Contact details by Id
        /// </summary>
        /// <param name="model"></param>
        /// <returns>PagingResult<ContactListingModel></returns>
        Task<ContactDetailsModel> GetContactDetailsById(long id);
    }
}

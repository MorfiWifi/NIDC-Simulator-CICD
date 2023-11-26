using Core.Repository;
using Domain.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Configs
{
    public class FolderService : BaseService<Folder>, IFolderService
    {
        public FolderService(IRepository<Folder> repo) : base(repo)
        {
        }

        public List<Folder> GetByUserIdAndPublicFolders(Guid userId)
        {
            return _repo.Where(x => x.AccountId == userId || x.IsPublic).ToList();

        }

        public List<Folder> GetFoldersByUserId(Guid userId)
        {
            return _repo.Where(x=>x.AccountId == userId).ToList();
        }
    }
    public interface IFolderService:IBaseService<Folder>
    {
        List<Folder> GetFoldersByUserId(Guid userId);
        List<Folder> GetByUserIdAndPublicFolders(Guid userId);
    }
}

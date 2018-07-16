namespace Portfoglio.Models
{
    public class SqlContext
    {
        public readonly IRepositoryAdvanced<Album> AlbumRepository;
        public readonly IRepositoryAdvanced<Picture> PictureRepository;
        public readonly IRepositoryAuth AuthRepository;

        public SqlContext(Context context)
        {
            AlbumRepository = new SqlAlbumRepository(context);
            PictureRepository = new SqlPictureRepository(context);
            AuthRepository = new SqlUserRepository(context);
        }
    }
}
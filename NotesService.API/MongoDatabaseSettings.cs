namespace NotesService.API.Config
{
    public class MongoDatabaseSettings
    {
        
            public string ConnectionString { get; set; } = null!;
            public string DatabaseName { get; set; } = null!;
            public string NotesCollectionName { get; set; } = null!;
        

    }
}

// TODO reuse for Foreign Keys
/*using AutoFixture;
using AutoFixture.Kernel;
using VideoGamesRental.Infrastructure.VideoGames;

namespace VideoGamesRental.Infrastructure.Tests.VideoGames;

public class VideoGameDataModelBuilder(IFixture inFixture) : ISpecimenBuilder
{
    public object Create(object inRequest, ISpecimenContext inContext)
    {
        if (inRequest is not Type type)
            return new NoSpecimen();
        
        if (type != typeof(VideoGameDataModel))
            return new NoSpecimen();
        
        var result = inFixture.Build<VideoGameDataModel>()
            .Create();
        
        result.FKId = result.FK!.Id;
        
        return result;
    }
}*/
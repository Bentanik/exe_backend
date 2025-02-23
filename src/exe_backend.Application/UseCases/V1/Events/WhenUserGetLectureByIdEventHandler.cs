
namespace exe_backend.Application.UseCases.V1.Events;

public sealed class WhenUserGetLectureByIdEventHandler
    (IPublishEndpoint publishEndpoint, IUnitOfWork unitOfWork)
    : IDomainEventHandler<Contract.Services.Course.Event.UserGotLectureByIdEvent>
{

    public async Task Handle(Contract.Services.Course.Event.UserGotLectureByIdEvent notification, CancellationToken cancellationToken)
    {
        Task.Run(() => CheckAndSaveProgressStudyAsync(notification));
    }

    public async Task CheckAndSaveProgressStudyAsync(Contract.Services.Course.Event.UserGotLectureByIdEvent lectureEvent)
    {
        var lecture = await unitOfWork.LectureRepository.FindSingleAsync(lt => lt.Id == lectureEvent.LectureId);

        if(lecture == null) {
        
        } 
    }
}
